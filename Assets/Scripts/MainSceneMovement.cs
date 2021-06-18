using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneMovement : MonoBehaviour
{
    [SerializeField] string sceneName;

    void OnTriggerEnter(Collider other)
    {
        // 顔パーツシーンで、球体にplayerが衝突→Mainシーンへ遷移
        if (other.tag == "Player")
        {
            SceneManager.UnloadSceneAsync(sceneName);
            SceneManager.LoadScene("MainScene", LoadSceneMode.Additive);
        }
    }
}
