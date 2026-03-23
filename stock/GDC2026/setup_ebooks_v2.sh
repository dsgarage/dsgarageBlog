#!/bin/bash
set -e

BASE_DIR="/Users/daisuketsukada/Documents/dsgarageBlog/TechBlog/stock/GDC2026"
EBOOK_DIR="$BASE_DIR/ebook"
CONVERTER="$BASE_DIR/md2review.py"

# Volume definitions
VOL1_SESSIONS=(
  "SilentHillF_Yang:01-silenthill"
  "UX_Mobile_Porting_PrinceOfPersia:02-princeofpersia"
  "IntelliScene_MultiAgent_GameSceneLayout:03-intelliscene"
  "IntentDriven_GameSceneEditor:04-intentdriven"
  "StateOfLevelDesign2026:05-leveldesign"
  "CreatingPlayerExpertise:06-playerexpertise"
)

VOL2_SESSIONS=(
  "KateEdwards:01-kateedwards"
  "Community_Safety_What_Players_Want:02-communitysafety"
  "Secrets_TabletopGamesIndustry:03-tabletop"
  "DOES_IT_MAKE_YOU_FEEL_LIKE_A_WANDERING_RONIN_CREATIVE_DIRECTION_FOR_GHOST_OF_YOTEI:04-ghostofyotei"
  "RulesOfTheGame2026:05-rulesofthegame"
  "ClashRoyale_Growth:06-clashroyale"
  "GooseGoldenEgg_Zukowski:07-goosegoldenegg"
)

setup_volume() {
  local vol_name=$1
  local vol_title=$2
  local vol_subtitle=$3
  shift 3
  local sessions=("$@")

  local vol_dir="$EBOOK_DIR/$vol_name"

  echo "=== Setting up $vol_name ==="

  # review init で新規作成
  if [ -d "$vol_dir" ]; then
    rm -rf "$vol_dir"
  fi
  cd "$EBOOK_DIR"
  review init "$vol_name"
  cd "$vol_dir"

  # デフォルトの .re ファイルを削除
  rm -f "$vol_dir/$vol_name.re"

  # config.yml を更新
  sed -i '' "s/^bookname: .*/bookname: $vol_name/" config.yml
  sed -i '' "s/^booktitle: .*/booktitle: $vol_title/" config.yml
  # subtitle を追加
  sed -i '' "/^booktitle:/a\\
subtitle: $vol_subtitle" config.yml
  sed -i '' "s/^aut: .*/aut: [\"dsgarage Games\"]/" config.yml
  sed -i '' "s/^language: .*/language: ja/" config.yml

  # 各セッションを変換
  local catalog_chaps=""
  for entry in "${sessions[@]}"; do
    local session_dir="${entry%%:*}"
    local re_name="${entry##*:}"
    local src_dir="$BASE_DIR/$session_dir"

    local md_file=$(ls "$src_dir"/*.md 2>/dev/null | grep -v README | grep -v SESSION_INFO | head -1)
    if [ -z "$md_file" ]; then
      echo "  WARNING: No markdown found in $src_dir"
      continue
    fi

    echo "  Converting: $session_dir -> $re_name.re"
    python3 "$CONVERTER" "$md_file" "$vol_dir/$re_name.re" ""

    # 画像コピー（images, slides, source/images を探索）
    mkdir -p "$vol_dir/images/$re_name"
    for subdir in images slides source/images; do
      if [ -d "$src_dir/$subdir" ]; then
        find "$src_dir/$subdir" -maxdepth 1 -type f \( -name "*.jpg" -o -name "*.jpeg" -o -name "*.png" -o -name "*.gif" -o -name "*.svg" -o -name "*.avif" \) -exec cp -n {} "$vol_dir/images/$re_name/" \;
      fi
    done
    # avif -> jpg
    for avif in "$vol_dir/images/$re_name/"*.avif; do
      if [ -f "$avif" ]; then
        sips -s format jpeg "$avif" --out "${avif%.avif}.jpg" 2>/dev/null && rm "$avif"
      fi
    done

    catalog_chaps="$catalog_chaps  - $re_name.re\n"
  done

  # まえがき
  cat > "$vol_dir/00-preface.re" << 'PREEOF'
= はじめに

本書は、2026年3月にサンフランシスコで開催された GDC（Game Developers Conference）2026 の注目セッションをレポートした電子書籍です。

各セッションのスピーカーによるトランスクリプト（スピーチ文字起こし）を中心に構成し、スライド画像とともにセッションの臨場感をお届けします。英語の引用は原文を残しつつ日本語意訳を添えています。
PREEOF

  # あとがき
  cat > "$vol_dir/99-postface.re" << 'POSTEOF'
= あとがき

本書をお読みいただきありがとうございます。

GDC 2026 のセッションレポートが、みなさまのゲーム開発に少しでもお役に立てれば幸いです。

//blankline

発行：dsgarage Games
POSTEOF

  # catalog.yml
  cat > "$vol_dir/catalog.yml" << CATEOF
PREDEF:
  - 00-preface.re
CHAPS:
$(echo -e "$catalog_chaps" | sed '/^$/d')
APPENDIX:
POSTDEF:
  - 99-postface.re
CATEOF

  echo "  Done: $vol_dir"
}

# Volume 1
setup_volume "gdc2026-vol1" \
  "GDC 2026 セッションレポート Vol.1 デザイン＆テクノロジー編" \
  "デザイン＆テクノロジー編" \
  "${VOL1_SESSIONS[@]}"

# Volume 2
setup_volume "gdc2026-vol2" \
  "GDC 2026 セッションレポート Vol.2 クリエイティブ＆ビジネス編" \
  "クリエイティブ＆ビジネス編" \
  "${VOL2_SESSIONS[@]}"

echo ""
echo "=== Complete ==="
echo "Vol.1: $EBOOK_DIR/gdc2026-vol1/"
echo "Vol.2: $EBOOK_DIR/gdc2026-vol2/"
echo ""
echo "Build: cd <vol_dir> && rake pdf"
