# Hatsumi「TimTea Final Part Rigging/Blendshapes」要点

- 依頼制作の記録連載の最終回。ナレーションなしで、揺れ物ボーン・ウェイト・表情シェイプキーまでを進める
- 衣装のひも・ベルトや髪の房に、親子で連なる揺れ物ボーンを追加（名前は番号と .L / .R 付き、全体で 339 本）
- 体は hips、upper_arm.L、thumb.proximal.L など、ボーン名の頂点グループをウェイトペイントでボーンごとに塗る
- 髪・衣装は Armature モディファイアー（Vertex Groups オン、Preserve Volume オフ）。房は帯状に少し重ねて塗る
- 目のシェイプキーはスカルプト（Smooth・Grab）で作り、Blend from Shape（Basis）で片側だけ元に戻す
- 口・ほお・鼻は ARKit の 52 種と同じ名前でプロポーショナル編集。最後に happy・sad・shock の表情キーも足す
