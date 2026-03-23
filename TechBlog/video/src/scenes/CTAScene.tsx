import { AbsoluteFill, interpolate, useCurrentFrame } from "remotion";
import { UTTLogo } from "../components/UTTLogo";
import { AnimatedText } from "../components/AnimatedText";
import { theme } from "../styles/theme";

interface CTASceneProps {
  author: string;
  date: string;
}

export const CTAScene: React.FC<CTASceneProps> = ({ author, date }) => {
  const frame = useCurrentFrame();

  // フェードイン
  const fadeIn = interpolate(frame, [0, 15], [0, 1], {
    extrapolateLeft: "clamp",
    extrapolateRight: "clamp",
  });

  // 背景グラデーション（微妙にシフト）
  const gradientShift = interpolate(frame, [0, 180], [0, 20], {
    extrapolateRight: "clamp",
  });

  return (
    <AbsoluteFill
      style={{
        background: `linear-gradient(${135 + gradientShift}deg, ${theme.bg} 0%, ${theme.bgLight} 100%)`,
        justifyContent: "center",
        alignItems: "center",
        padding: 80,
        opacity: fadeIn,
      }}
    >
      {/* CTA テキスト */}
      <AnimatedText
        text="詳しくはブログで!"
        delay={10}
        animation="fadeUp"
        style={{
          fontSize: 52,
          fontWeight: 700,
          color: theme.text,
          marginBottom: 20,
        }}
      />

      {/* アクセントライン */}
      <div
        style={{
          width: interpolate(frame, [30, 60], [0, 400], {
            extrapolateLeft: "clamp",
            extrapolateRight: "clamp",
          }),
          height: 4,
          backgroundColor: theme.accent,
          borderRadius: 2,
          marginBottom: 60,
        }}
      />

      {/* UTT ロゴ再表示 */}
      <UTTLogo size={80} fadeInFrom={40} />

      {/* 著者 / 日付 */}
      <div style={{ marginTop: 48, textAlign: "center" }}>
        <AnimatedText
          text={`${author} | ${date}`}
          delay={60}
          animation="fadeIn"
          style={{
            fontSize: 22,
            fontWeight: 400,
            color: theme.textDim,
          }}
        />
      </div>
    </AbsoluteFill>
  );
};
