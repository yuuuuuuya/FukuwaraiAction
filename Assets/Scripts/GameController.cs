using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] LifePanel lifePanel;
    [SerializeField] FacePartsPanel facePartsPanel;

    void Awake()
    {
        // デバッグ用
        // Managerシーンでゲーム開始→Mainシーンをロード
        if (ManagerSceneLoader.isloaded) return;
        SceneManager.LoadScene("MainScene");
    }

    void Update()
    {
        // 残らいふを随時チェックし、ライフパネルを更新
        lifePanel.UpdateLifeIcons(Player.life);
        facePartsPanel.UpdateFacePartsIcons();
    }

    public void OnRestartButtonClicked()
    {
        // リスタートボタンクリック→タイトルシーンへ
        SceneManager.LoadScene("TitleScene");
    }
}
