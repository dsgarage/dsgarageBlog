import { Composition } from "remotion";
import { Video } from "./Video";
import { theme, type VideoProps } from "./styles/theme";

const defaultProps: VideoProps = {
  title: "UnityでなぜMCPが必要なのか",
  subtitle: "AIコーディング時代のUnity開発インフラ",
  keyPoints: [
    "AIはスクリプト生成が得意だがScene操作ができない",
    "MCPがAIとUnity Editorを直接接続する",
    "UniMCP4CCで開発速度が劇的に向上する",
  ],
  author: "dsgarage",
  date: "2025-12-26",
};

export const Root: React.FC = () => {
  return (
    <Composition
      id="BlogVideo"
      component={Video}
      durationInFrames={theme.durationInFrames}
      fps={theme.fps}
      width={theme.canvas.width}
      height={theme.canvas.height}
      defaultProps={defaultProps}
    />
  );
};
