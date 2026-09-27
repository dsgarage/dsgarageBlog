#!/bin/zsh
# Blender Fes 2026 AW Day1 の 1080p アーカイブを、セッション単位に無劣化分割する(exia 上で実行)。
#
# 使い方(ローカルから):
#   scp stock/BlenderFes2026/scripts/split_video_exia.sh exiamac-mini:/Volumes/Disk4TB/BlenderFes2026AW/
#   ssh exiamac-mini 'zsh /Volumes/Disk4TB/BlenderFes2026AW/split_video_exia.sh'
#
# 切り出し範囲 = 壁時計で「開始 5 分前 〜 終了 5 分後」(= 4200 秒)。
# 配信開始 = 2026-09-26 09:45:01 なので、配信内秒 = 壁時計 - 09:45:01。
# -ss を入力側に置いた -c copy なので、実際の開始はキーフレーム境界まで数秒前後する。
# 出力が既に存在するセッションはスキップする(上書き・削除はしない)。
set -u
ROOT=${ROOT:-/Volumes/Disk4TB/BlenderFes2026AW}
SRC=${1:-$ROOT/BlenderFes2026AW_Day1_1080p.mp4}
OUT=$ROOT/Day1
FFMPEG=/opt/homebrew/bin/ffmpeg
FFPROBE=/opt/homebrew/bin/ffprobe
DUR=4200

# 番号:配信内オフセット秒:ディレクトリ名
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

[[ -f "$SRC" ]] || { echo "入力がありません: $SRC"; exit 1; }
mkdir -p "$OUT"
for s in $SESSIONS; do
  IFS=: read nn off name <<< "$s"
  dst="$OUT/$name.mp4"
  if [[ -e "$dst" ]]; then echo "[skip] $name (既存)"; continue; fi
  echo "[split] $name ss=$off t=$DUR"
  "$FFMPEG" -hide_banner -loglevel error -n -ss "$off" -i "$SRC" -t "$DUR" \
    -map 0 -c copy -avoid_negative_ts make_zero -movflags +faststart "$dst" \
    || { echo "SPLIT FAILED $name"; exit 2; }
done

echo "[verify] duration(秒)"
for f in "$OUT"/*.mp4; do
  echo "$f $("$FFPROBE" -v error -show_entries format=duration -of csv=p=0 "$f")"
done
