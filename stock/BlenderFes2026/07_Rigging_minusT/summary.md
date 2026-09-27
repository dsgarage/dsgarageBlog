# S7 ボーン1本から始めるキャラクターリギング（minusT）要点

- 講演は全編韓国語。アドオンを使わず、Blender 5.2 の標準機能だけで 1 体のキャラクターを Origin ボーン 1 本からリギングした
- Euler と Quaternion は用途で使い分ける。自由に回るボーンは Quaternion、軸が決まった指や何回転もするボーンは Euler
- 骨格は Origin（全体）→ torso（胴体）→ spine / hips の階層。左半分を作り Auto-Name と Symmetrize で右側を生成する
- ウェイトは Auto Normalize をオンにし、Gradient と Blur で調整。指は数値で 1.0 と 0.5 を Assign する
- 塗っても直らない変形は、関節のループ配置（トポロジー）が原因のことがある
- 腕と脚は IK（Chain Length 2、Pole Target）。腕が一直線だと曲がる向きが決まらないので肘を少しずらしておく
- 足は Leg.IK の下にかかと用の逆向きボーンを置き、足裏を床に残したままかかと上げとつま先を操作できるようにする
- 指は Copy Rotation（Local Space、X のみ、Mix: Add）で 1 本回せば全体が曲がるようにする
- 表情は Shape Key を Driver（Averaged Value と Generator 係数 50）でボーンに接続し、Limit Location で範囲を制限する
- 衣装は Data Transfer、Surface Deform のケージ、頂点ペアレントを部位で使い分け、手首のねじれは Bendy Bones で対処する
