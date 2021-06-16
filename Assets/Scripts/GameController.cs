using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] LifePanel lifePanel;
    [SerializeField] FacePartsPanel facePartsPanel;

    void Awake()
    {
        //デバッグ用
        if (ManagerSceneLoader.isloaded) return;
        SceneManager.LoadScene("MainScene");
    }

    void Update()
    {
        lifePanel.UpdateLifeIcons(player.life);
        facePartsPanel.UpdateFacePartsIcons();
    }

    public void OnRestartButtonClicked()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
