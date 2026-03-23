import { interpolate, spring, useCurrentFrame, useVideoConfig } from "remotion";

interface AnimatedTextProps {
  text: string;
  /** アニメーション開始フレーム（Sequence 内の相対フレーム） */
  delay?: number;
  /** アニメーション種別 */
  animation?: "fadeUp" | "slideLeft" | "fadeIn";
  style?: React.CSSProperties;
}

export const AnimatedText: React.FC<AnimatedTextProps> = ({
  text,
  delay = 0,
  animation = "fadeUp",
  style = {},
}) => {
  const frame = useCurrentFrame();
  const { fps } = useVideoConfig();

  const progress = spring({
    frame: frame - delay,
    fps,
    config: { damping: 20, stiffness: 80, mass: 0.8 },
  });

  const animationStyles: React.CSSProperties = (() => {
    switch (animation) {
      case "fadeUp":
        return {
          opacity: progress,
          transform: `translateY(${interpolate(progress, [0, 1], [40, 0])}px)`,
        };
      case "slideLeft":
        return {
          opacity: progress,
          transform: `translateX(${interpolate(progress, [0, 1], [60, 0])}px)`,
        };
      case "fadeIn":
        return {
          opacity: progress,
        };
    }
  })();

  return <div style={{ ...animationStyles, ...style }}>{text}</div>;
};
