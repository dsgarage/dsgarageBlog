#!/bin/zsh
# セッション動画からスライド候補フレームを抽出する(exia 上で実行)。
#
# 使い方(ローカルから):
#   scp stock/BlenderFes2026/scripts/extract_frames_exia.sh exiamac-mini:/Volumes/Disk4TB/BlenderFes2026AW/
#   ssh exiamac-mini 'zsh /Volumes/Disk4TB/BlenderFes2026AW/extract_frames_exia.sh'        # 全セッション
#   ssh exiamac-mini 'zsh /Volumes/Disk4TB/BlenderFes2026AW/extract_frames_exia.sh 03'     # 1 セッションだけ
# 取り込み(ローカルで):
#   for d in stock/BlenderFes2026/0?_*/; do n=$(basename $d)
#     rsync -a exiamac-mini:/Volumes/Disk4TB/BlenderFes2026AW/Day1/frames/$n/ $d/source/images/; done
#
# 方式:
#   - キーフレームだけデコード(-skip_frame nokey)し、select の scene スコアで切り替わりを検出する
#   - 直前に採用したフレームから MIN_GAP 秒未満のフレームは捨てる
#   - まず枚数だけ数え(書き出しなし)、MAX_FRAMES を超えたら閾値を上げてから書き出す
#   - 幅 1280 の JPEG(-q:v 4)。frames.tsv に 切り出し内秒 / 配信内秒 / 壁時計 を記録する
#   - 既に frames.tsv があるセッションはスキップする(削除・上書きはしない)
set -u
ROOT=${ROOT:-/Volumes/Disk4TB/BlenderFes2026AW}
IN=$ROOT/Day1
OUT=${OUT:-$IN/frames}
FFMPEG=/opt/homebrew/bin/ffmpeg
MIN_GAP=${MIN_GAP:-15}
MAX_FRAMES=${MAX_FRAMES:-300}
THRESHOLDS=(0.3 0.4 0.5 0.6 0.7 0.8)
[[ -n "${THRESHOLDS_OVERRIDE:-}" ]] && THRESHOLDS=(${=THRESHOLDS_OVERRIDE})   # 例: THRESHOLDS_OVERRIDE="0.15 0.2 0.3"
STREAM_START_SEC=35101   # 09:45:01 を 0 時からの秒にしたもの

SESSIONS=(
  "01:599:01_KaguyahimeCGBackground_QoonPlant"
  "02:5099:02_CharacterShading_Eight"
  "03:9599:03_MoruReal2_JennyKaori"
  "04:14099:04_AnimeLook_SuminekoBebe"
  "05:18599:05_KireAction_Tatsumura"
  "06:23099:06_Effects_Gyunyubin"
  "07:27599:07_Rigging_minusT"
  "08:32099:08_Addons_3Dnin"
)
ONLY=${1:-}

vf_select() { echo "select='gt(scene,$1)*(isnan(prev_selected_t)+gte(t-prev_selected_t,$MIN_GAP))'"; }

for s in $SESSIONS; do
  IFS=: read nn off name <<< "$s"
  [[ -n "$ONLY" && "$ONLY" != "$nn" ]] && continue
  src="$IN/$name.mp4"; dst="$OUT/$name"
  [[ -f "$src" ]] || { echo "[miss] $src"; continue; }
  if [[ -f "$dst/frames.tsv" ]]; then echo "[skip] $name (frames.tsv 既存)"; continue; fi

  # 1) 枚数だけ数えて閾値を決める
  th=""
  for t in $THRESHOLDS; do
    cnt=$("$FFMPEG" -hide_banner -nostats -skip_frame nokey -i "$src" -an \
            -vf "$(vf_select $t),showinfo" -f null - 2>&1 | grep -c 'Parsed_showinfo.*pts_time:')
    echo "[count] $name scene>$t -> $cnt"
    th=$t
    (( cnt <= MAX_FRAMES )) && break
  done

  # 2) 書き出し
  mkdir -p "$dst"
  "$FFMPEG" -hide_banner -nostats -n -skip_frame nokey -i "$src" -an \
    -vf "$(vf_select $th),scale=1280:-2,showinfo" -fps_mode vfr -q:v 4 \
    "$dst/frame_%04d.jpg" 2> "$dst/showinfo.log" || { echo "EXTRACT FAILED $name"; continue; }

  # 3) showinfo の n(0 始まり)= ファイル番号 - 1 として frames.tsv を作る
  {
    printf 'file\tclip_sec\tstream_sec\twallclock\n'
    grep 'Parsed_showinfo.*pts_time:' "$dst/showinfo.log" \
      | sed -E 's/.* n: *([0-9]+) .*pts_time:([0-9.]+).*/\1 \2/' \
      | awk -v off="$off" -v base="$STREAM_START_SEC" '{
          st = off + $2; w = int(base + st);
          printf "frame_%04d.jpg\t%.3f\t%.3f\t2026-09-26 %02d:%02d:%02d\n", $1+1, $2, st, int(w/3600), int(w%3600/60), w%60 }'
  } > "$dst/frames.tsv"
  echo "[done] $name threshold=$th frames=$(ls "$dst"/frame_*.jpg | wc -l | tr -d ' ') size=$(du -sh "$dst" | cut -f1)"
done
