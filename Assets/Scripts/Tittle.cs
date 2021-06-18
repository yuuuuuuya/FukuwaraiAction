using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tittle : MonoBehaviour
{
    public void OnStartButtonClicked()
    {
        // 各プロパティをリセット
        FaceParts.hasAttachedLeftEye = false;
        FaceParts.hasAttachedMouth = false;
        FaceParts.hasAttachedNose = false;
        FaceParts.hasAttachedRightEye = false;

        Player.life = 3;

        Player.isGoal = false;

        // Mainシーンをロード
        SceneManager.LoadScene("MainScene");
    }
}
