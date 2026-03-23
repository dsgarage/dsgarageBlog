// style.css と統一したカラーパレット
export const theme = {
  // 背景
  bg: "#1a1a2e",
  bgLight: "#16213e",

  // テキスト
  text: "#ffffff",
  textMuted: "rgba(255, 255, 255, 0.7)",
  textDim: "rgba(255, 255, 255, 0.5)",

  // カラー（style.css --primary, --accent 準拠）
  primary: "#2563eb",
  primaryDark: "#1e40af",
  accent: "#e94560",
  accentAlt: "#f59e0b",

  // フォント
  fontFamily: "'Noto Sans JP', sans-serif",

  // サイズ
  canvas: { width: 1080, height: 1080 },
  fps: 30,
  durationInFrames: 900, // 30秒
} as const;

// シーン配分（フレーム数）
export const scenes = {
  hook: { from: 0, duration: 300 },       // 0-9秒 (10秒)
  keyPoints: { from: 300, duration: 420 }, // 10-23秒 (14秒)
  cta: { from: 720, duration: 180 },      // 24-29秒 (6秒)
} as const;

// VideoProps の型定義
export interface VideoProps {
  title: string;
  subtitle: string;
  keyPoints: string[];
  author: string;
  date: string;
}
