#!/bin/bash
set -e

BASE_DIR="/Users/daisuketsukada/Documents/dsgarageBlog/TechBlog/stock/GDC2026"
TEMPLATE_DIR="/Users/daisuketsukada/ReVIEWStarterTemplate"
CONVERTER="$BASE_DIR/md2review.py"

# Volume definitions
# Vol1: デザイン＆テクノロジー
VOL1_SESSIONS=(
  "SilentHillF_Yang:01-silenthill"
  "UX_Mobile_Porting_PrinceOfPersia:02-princeofpersia"
  "IntelliScene_MultiAgent_GameSceneLayout:03-intelliscene"
  "IntentDriven_GameSceneEditor:04-intentdriven"
  "StateOfLevelDesign2026:05-leveldesign"
  "CreatingPlayerExpertise:06-playerexpertise"
)

# Vol2: クリエイティブ＆ビジネス
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

  local vol_dir="$BASE_DIR/ebook/$vol_name"

  echo "=== Setting up $vol_name ==="

  # Copy template
  if [ -d "$vol_dir" ]; then
    rm -rf "$vol_dir"
  fi
  cp -r "$TEMPLATE_DIR" "$vol_dir"

  # Clean template contents
  rm -f "$vol_dir/contents/"*.re
  rm -rf "$vol_dir/images/"*

  # Convert each session
  local catalog_chaps=""
  for entry in "${sessions[@]}"; do
    local session_dir="${entry%%:*}"
    local re_name="${entry##*:}"
    local src_dir="$BASE_DIR/$session_dir"

    # Find the markdown file
    local md_file=$(ls "$src_dir"/*.md 2>/dev/null | grep -v README | head -1)
    if [ -z "$md_file" ]; then
      echo "  WARNING: No markdown found in $src_dir"
      continue
    fi

    echo "  Converting: $session_dir -> $re_name.re"

    # Convert markdown to Re:VIEW
    python3 "$CONVERTER" "$md_file" "$vol_dir/contents/$re_name.re" ""

    # Copy images
    if [ -d "$src_dir/images" ]; then
      mkdir -p "$vol_dir/images/$re_name"
      cp "$src_dir/images/"*.{jpg,jpeg,png,gif,svg,webp,avif} "$vol_dir/images/$re_name/" 2>/dev/null || true
      # Convert avif to jpg for Re:VIEW compatibility
      for avif in "$vol_dir/images/$re_name/"*.avif; do
        if [ -f "$avif" ]; then
          local jpg="${avif%.avif}.jpg"
          sips -s format jpeg "$avif" --out "$jpg" 2>/dev/null && rm "$avif"
        fi
      done
    fi

    catalog_chaps="$catalog_chaps  - $re_name.re\n"
  done

  # Write catalog.yml
  cat > "$vol_dir/catalog.yml" << CATEOF
## GDC 2026 電子書籍 $vol_name
PREDEF:
  - 00-preface.re
CHAPS:
$(echo -e "$catalog_chaps" | sed '/^$/d')
APPENDIX:
POSTDEF:
  - 99-postface.re
CATEOF

  # Create preface
  cat > "$vol_dir/contents/00-preface.re" << 'PREEOF'
= はじめに

本書は、2026年3月にサンフランシスコで開催された GDC（Game Developers Conference）2026 の注目セッションをレポートした電子書籍です。

各セッションのスピーカーによるトランスクリプト（スピーチ文字起こし）を中心に構成し、スライド画像とともにセッションの臨場感をお届けします。英語の引用は原文を残しつつ日本語意訳を添えています。

GDC は世界最大のゲーム開発者会議であり、毎年3万人以上のゲーム開発者が集まります。技術、デザイン、ビジネス、ナラティブなど多岐にわたるトラックで数百のセッションが開催され、業界の最前線が共有される場です。
PREEOF

  # Create postface
  cat > "$vol_dir/contents/99-postface.re" << 'POSTEOF'
= あとがき

本書をお読みいただきありがとうございます。

GDC 2026 のセッションレポートが、みなさまのゲーム開発に少しでもお役に立てれば幸いです。

//blankline

発行：dsgarage Games
POSTEOF

  # Update config.yml
  sed -i '' "s/^booktitle: .*/booktitle: |-\n  $vol_title/" "$vol_dir/config.yml"
  sed -i '' "s/^subtitle: .*/subtitle: |-\n  $vol_subtitle/" "$vol_dir/config.yml"
  sed -i '' "s/^bookname: .*/bookname: $vol_name/" "$vol_dir/config.yml"
  sed -i '' "s/^date: .*/date: 2026-03-11/" "$vol_dir/config.yml"
  sed -i '' "s/^rights: .*/rights: (C) 2026 dsgarage Games/" "$vol_dir/config.yml"

  echo "  Done: $vol_dir"
}

# Create ebook directory
mkdir -p "$BASE_DIR/ebook"

# Setup Volume 1
setup_volume "gdc2026-vol1" \
  "GDC 2026 セッションレポート Vol.1" \
  "デザイン＆テクノロジー編" \
  "${VOL1_SESSIONS[@]}"

# Setup Volume 2
setup_volume "gdc2026-vol2" \
  "GDC 2026 セッションレポート Vol.2" \
  "クリエイティブ＆ビジネス編" \
  "${VOL2_SESSIONS[@]}"

echo ""
echo "=== Complete ==="
echo "Vol.1: $BASE_DIR/ebook/gdc2026-vol1/"
echo "Vol.2: $BASE_DIR/ebook/gdc2026-vol2/"
echo ""
echo "Build PDF:  cd <vol_dir> && rake pdf"
echo "Build ePub: cd <vol_dir> && rake epub"
