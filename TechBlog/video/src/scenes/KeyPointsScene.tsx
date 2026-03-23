import { AbsoluteFill, interpolate, useCurrentFrame } from "remotion";
import { AnimatedText } from "../components/AnimatedText";
import { theme } from "../styles/theme";

interface KeyPointsSceneProps {
  keyPoints: string[];
}

export const KeyPointsScene: React.FC<KeyPointsSceneProps> = ({
  keyPoints,
}) => {
  const frame = useCurrentFrame();

  // シーン開始時のフェードイン（最初15フレーム）
  const fadeIn = interpolate(frame, [0, 15], [0, 1], {
    extrapolateLeft: "clamp",
    extrapolateRight: "clamp",
  });

  // シーン終了時のフェードアウト（最後15フレーム）
  const fadeOut = interpolate(frame, [400, 419], [1, 0], {
    extrapolateLeft: "clamp",
    extrapolateRight: "clamp",
  });

  // 各ポイントの表示タイミング（均等配分）
  const pointInterval = 120; // 4秒間隔

  // 番号インジケーター
  const activeIndex = Math.min(
    Math.floor(frame / pointInterval),
    keyPoints.length - 1,
  );

  return (
    <AbsoluteFill
      style={{
        backgroundColor: theme.bg,
        padding: 80,
        opacity: fadeIn * fadeOut,
      }}
    >
      {/* セクションヘッダ */}
      <AnimatedText
        text="POINT"
        delay={0}
        animation="fadeIn"
        style={{
          fontSize: 20,
          fontWeight: 700,
          color: theme.accent,
          letterSpacing: "0.3em",
          marginBottom: 48,
        }}
      />

      {/* 要点リスト */}
      <div
        style={{
          display: "flex",
          flexDirection: "column",
          gap: 48,
          flex: 1,
          justifyContent: "center",
        }}
      >
        {keyPoints.slice(0, 3).map((point, i) => {
          const pointDelay = 20 + i * pointInterval;
          const isActive = i <= activeIndex;

          // Primary 縦バーの表示アニメーション
          const barHeight = interpolate(
            frame,
            [pointDelay, pointDelay + 20],
            [0, 1],
            { extrapolateLeft: "clamp", extrapolateRight: "clamp" },
          );

          return (
            <div
              key={i}
              style={{
                display: "flex",
                alignItems: "flex-start",
                gap: 28,
                opacity: isActive ? 1 : 0.3,
                transition: "opacity 0.3s",
              }}
            >
              {/* Primary 縦バー */}
              <div
                style={{
                  width: 6,
                  height: 60,
                  backgroundColor: theme.primary,
                  borderRadius: 3,
                  transform: `scaleY(${barHeight})`,
                  transformOrigin: "top",
                  flexShrink: 0,
                  marginTop: 4,
                }}
              />

              {/* 番号 + テキスト */}
              <div>
                <AnimatedText
                  text={`${i + 1}`}
                  delay={pointDelay}
                  animation="fadeIn"
                  style={{
                    fontSize: 24,
                    fontWeight: 700,
                    color: theme.primary,
                    marginBottom: 8,
                  }}
                />
                <AnimatedText
                  text={point}
                  delay={pointDelay + 10}
                  animation="slideLeft"
                  style={{
                    fontSize: 36,
                    fontWeight: 600,
                    color: theme.text,
                    lineHeight: 1.5,
                  }}
                />
              </div>
            </div>
          );
        })}
      </div>

      {/* 下部のドットインジケーター */}
      <div
        style={{
          display: "flex",
          justifyContent: "center",
          gap: 16,
          marginTop: 40,
        }}
      >
        {keyPoints.slice(0, 3).map((_, i) => (
          <div
            key={i}
            style={{
              width: 12,
              height: 12,
              borderRadius: "50%",
              backgroundColor:
                i === activeIndex ? theme.primary : "rgba(255,255,255,0.2)",
            }}
          />
        ))}
      </div>
    </AbsoluteFill>
  );
};
