import { AbsoluteFill, Sequence } from "remotion";
import { loadFont } from "@remotion/google-fonts/NotoSansJP";
import { HookScene } from "./scenes/HookScene";
import { KeyPointsScene } from "./scenes/KeyPointsScene";
import { CTAScene } from "./scenes/CTAScene";
import { theme, scenes, type VideoProps } from "./styles/theme";

const { fontFamily } = loadFont();

export const Video: React.FC<VideoProps> = ({
  title,
  subtitle,
  keyPoints,
  author,
  date,
}) => {
  return (
    <AbsoluteFill
      style={{
        backgroundColor: theme.bg,
        fontFamily,
      }}
    >
      {/* Scene 1: Hook — ロゴ + タイトル (10秒) */}
      <Sequence from={scenes.hook.from} durationInFrames={scenes.hook.duration}>
        <HookScene title={title} subtitle={subtitle} />
      </Sequence>

      {/* Scene 2: KeyPoints — 要点3つ (14秒) */}
      <Sequence
        from={scenes.keyPoints.from}
        durationInFrames={scenes.keyPoints.duration}
      >
        <KeyPointsScene keyPoints={keyPoints} />
      </Sequence>

      {/* Scene 3: CTA — 導線 (6秒) */}
      <Sequence from={scenes.cta.from} durationInFrames={scenes.cta.duration}>
        <CTAScene author={author} date={date} />
      </Sequence>
    </AbsoluteFill>
  );
};
