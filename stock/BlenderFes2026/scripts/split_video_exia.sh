#!/bin/zsh
# Blender Fes 2026 AW の 1080p アーカイブを、セッション単位に無劣化分割する(exia 上で実行)。
#
# 使い方(ローカルから):
#   scp stock/BlenderFes2026/scripts/split_video_exia.sh exiamac-mini:/Volumes/Disk4TB/BlenderFes2026AW/
#   ssh exiamac-mini 'zsh /Volumes/Disk4TB/BlenderFes2026AW/split_video_exia.sh'          # Day1
#   ssh exiamac-mini 'DAY=2 zsh /Volumes/Disk4TB/BlenderFes2026AW/split_video_exia.sh'    # Day2
#
# DAY(既定 1)で入力 BlenderFes2026AW_Day${DAY}_1080p.mp4、出力 Day${DAY}/、SESSIONS を切り替える。
# 切り出し範囲 = 壁時計で「開始 5 分前 〜 終了 5 分後」(= 4200 秒)。
# 配信開始 = Day1 2026-09-26 09:45:01 / Day2 2026-09-27 09:45:01 なので、配信内秒 = 壁時計 - 09:45:01。
# -ss を入力側に置いた -c copy なので、実際の開始はキーフレーム境界まで数秒前後する。
# 出力が既に存在するセッションはスキップする(上書き・削除はしない)。
set -u
ROOT=${ROOT:-/Volumes/Disk4TB/BlenderFes2026AW}
DAY=${DAY:-1}
SRC=${1:-$ROOT/BlenderFes2026AW_Day${DAY}_1080p.mp4}
OUT=$ROOT/Day${DAY}
FFMPEG=/opt/homebrew/bin/ffmpeg
FFPROBE=/opt/homebrew/bin/ffprobe
DUR=4200

# 番号:配信内オフセット秒:ディレクトリ名[:長さ秒]  長さ省略時は DUR(4200)。
# Day1: 予定終了を超えて続いた S2/S4 は 4800 秒、S8 は配信終端まで(4802 秒)に延長。
# Day2: S8 は配信終端(36001 秒)までの 3902 秒。
if [[ "$DAY" == 2 ]]; then
SESSIONS=(
  "09:599:09_ShortFilm_FUKUPOLY"
  "10:5099:10_MotionGraphicsNodes_cerbalance"
  "11:9599:11_RealtimeContent_raw"
  "12:14099:12_3DPrint_Shiotsuki_Hagiwara"
  "13:18599:13_PhotorealCG_Iori"
  "14:23099:14_VRChatWorld_Fujito"
  "15:27599:15_VibeModeling_KOBATAKA_posiposi"
  "16:32099:16_SuzanneAwards:3902"
)
else
SESSIONS=(
  "01:599:01_KaguyahimeCGBackground_QoonPlant"
  "02:5099:02_CharacterShading_Eight:4800"
  "03:9599:03_MoruReal2_JennyKaori"
  "04:14099:04_AnimeLook_SuminekoBebe:4800"
  "05:18599:05_KireAction_Tatsumura"
  "06:23099:06_Effects_Gyunyubin"
  "07:27599:07_Rigging_minusT"
  "08:32099:08_Addons_3Dnin:4802"
)
fi

[[ -f "$SRC" ]] || { echo "入力がありません: $SRC"; exit 1; }
mkdir -p "$OUT"
for s in $SESSIONS; do
  IFS=: read nn off name len <<< "$s"
  len=${len:-$DUR}
  dst="$OUT/$name.mp4"
  if [[ -e "$dst" ]]; then echo "[skip] $name (既存)"; continue; fi
  echo "[split] $name ss=$off t=$len"
  "$FFMPEG" -hide_banner -loglevel error -n -ss "$off" -i "$SRC" -t "$len" \
    -map 0 -c copy -avoid_negative_ts make_zero -movflags +faststart "$dst" \
    || { echo "SPLIT FAILED $name"; exit 2; }
done

echo "[verify] duration(秒)"
for f in "$OUT"/*.mp4; do
  [[ "$f" == *.short*.mp4 ]] && continue   # 延長前に退避した旧版は対象外
  echo "$f $("$FFPROBE" -v error -show_entries format=duration -of csv=p=0 "$f")"
done
