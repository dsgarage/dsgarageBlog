import { interpolate, useCurrentFrame } from "remotion";
import { theme } from "../styles/theme";

interface UTTLogoProps {
  /** ロゴサイズ（フォントサイズ） */
  size?: number;
  /** フェードイン開始フレーム */
  fadeInFrom?: number;
}

export const UTTLogo: React.FC<UTTLogoProps> = ({
  size = 120,
  fadeInFrom = 0,
}) => {
  const frame = useCurrentFrame();
  const opacity = interpolate(frame, [fadeInFrom, fadeInFrom + 20], [0, 1], {
    extrapolateLeft: "clamp",
    extrapolateRight: "clamp",
  });
  const scale = interpolate(
    frame,
    [fadeInFrom, fadeInFrom + 25],
    [0.8, 1],
    { extrapolateLeft: "clamp", extrapolateRight: "clamp" },
  );

  return (
    <div
      style={{
        display: "flex",
        flexDirection: "column",
        alignItems: "center",
        opacity,
        transform: `scale(${scale})`,
      }}
    >
      <div
        style={{
          fontSize: size,
          fontWeight: 900,
          color: theme.accent,
          letterSpacing: "0.08em",
          lineHeight: 1,
        }}
      >
        UTT
      </div>
      <div
        style={{
          fontSize: size * 0.18,
          fontWeight: 400,
          color: theme.textMuted,
          letterSpacing: "0.15em",
          marginTop: size * 0.08,
        }}
      >
        UnityTeacherTsukada
      </div>
    </div>
  );
};
