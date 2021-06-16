using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tittle : MonoBehaviour
{
    public void OnStartButtonClicked()
    {
        // 顔パーツ取得済みかのプロパティをリセット
        FaceParts.hasAttachedLeftEye = false;
        FaceParts.hasAttachedMouth = false;
        FaceParts.hasAttachedNose = false;
        FaceParts.hasAttachedRightEye = false;

        //lifeプロパティをリセット
        Player.life = 3;

        // Mainシーンをロード
        SceneManager.LoadScene("MainScene");
    }
}
