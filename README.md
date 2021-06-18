# FukuwaraiAction

福笑いをアクションゲームに。<br>
顔パーツに衝突時の角度で、顔の土台にセット。<br>
3回落ちるとゲームオーバー。

アクションゲームにする事で福笑いにはないハラハラ感を味わえる。<br>
また、福笑いと同じく最後までどんな顔になるか分からないドキドキ感。


## DEMO

[![demo](https://github.com/yuuuuuuya/FukuwaraiAction/wiki/fukuwaraiAction.gif)](https://github.com/yuuuuuuya/FukuwaraiAction/wiki/fukuwaraiAction.gif)


## implementation
- 複数のSceneを同時ロード<br>
前のシーンの情報を残しておきたいPlayerオブジェクトとUIをManagerシーンに配置。<br>
5つあるステージは別シーンにわけ、ステージ切替は"UnloadSceneAsync"で現在のステージシーンのみをアンロード。<br>
Managerシーンは残したままLoadSceneMode.Additiveで次のシーンをロード。<br>
また、デバッグ用に各ステージシーンから始めてもプレイ出来るように実装。<br>

- キャラのアニメーション<br>
移動スピードをパラメーターとしてAnimatorに渡し、<br>
BrendTreeで値が小さければ歩くモーション、値が大きければ走るモーションになるよう実装。

- デバッグ用と実機用で操作を出来る様に<br>
Unityエディタ上ではpcのキーを押下しキャラの操作を、<br>
スマートフォンではUGUIのUIを押下しキャラを操作できるように実装。


## Author

* 伊島悠矢
* b.ald.m.wn@gmail.com