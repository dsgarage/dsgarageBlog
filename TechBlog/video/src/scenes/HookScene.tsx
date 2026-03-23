import { AbsoluteFill, interpolate, useCurrentFrame } from "remotion";
import { UTTLogo } from "../components/UTTLogo";
import { AnimatedText } from "../components/AnimatedText";
import { theme } from "../styles/theme";

interface HookSceneProps {
  title: string;
  subtitle: string;
}

export const HookScene: React.FC<HookSceneProps> = ({ title, subtitle }) => {
  const frame = useCurrentFrame();

  // シーン終了時のフェードアウト（最後15フレーム）
  const fadeOut = interpolate(frame, [270, 299], [1, 0], {
    extrapolateLeft: "clamp",
    extrapolateRight: "clamp",
  });

  // タイトル下のアクセントライン
  const lineWidth = interpolate(frame, [90, 130], [0, 600], {
    extrapolateLeft: "clamp",
    extrapolateRight: "clamp",
  });

  return (
    <AbsoluteFill
      style={{
        backgroundColor: theme.bg,
        justifyContent: "center",
        alignItems: "center",
        padding: 80,
        opacity: fadeOut,
      }}
    >
      {/* UTT ロゴ (フレーム0からフェードイン) */}
      <UTTLogo size={100} fadeInFrom={0} />

      {/* タイトル (フレーム60からフェードアップ) */}
      <div style={{ marginTop: 60, textAlign: "center" }}>
        <AnimatedText
          text={title}
          delay={60}
          animation="fadeUp"
          style={{
            fontSize: 56,
            fontWeight: 700,
            color: theme.text,
            lineHeight: 1.3,
          }}
        />
      </div>

      {/* アクセントライン */}
      <div
        style={{
          width: lineWidth,
          height: 4,
          backgroundColor: theme.accent,
          marginTop: 24,
          borderRadius: 2,
        }}
      />

      {/* サブタイトル (フレーム100からフェードイン) */}
      <div style={{ marginTop: 28 }}>
        <AnimatedText
          text={subtitle}
          delay={100}
          animation="fadeIn"
          style={{
            fontSize: 28,
            fontWeight: 400,
            color: theme.textMuted,
            textAlign: "center",
          }}
        />
      </div>
    </AbsoluteFill>
  );
};
