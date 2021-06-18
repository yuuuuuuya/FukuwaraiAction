using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerSceneLoader : MonoBehaviour
{
    public static bool isloaded = false;
    
    [SerializeField] string sceneName;

    void Awake()
    {
        // デバッグ用
        // 各Stageシーンでゲーム開始→Managerシーンをロード
        if (isloaded) return;
        SceneManager.LoadScene("MnagerScene", LoadSceneMode.Additive);
        isloaded = true;
    }

    void Update()
    {
        //playerが落下→lifeが0:TitleScene、lifeが1以上:mainSceneをロード
        Scene MnagerScene = SceneManager.GetSceneByName("MnagerScene");

        foreach (GameObject rootGameObject in MnagerScene.GetRootGameObjects())
        {
            if (rootGameObject.name != "Player") continue;
            if (rootGameObject.transform.position.y <= -10)
            {
                Player Player = rootGameObject.GetComponent<Player>();
                Player.life--;

                if (Player.life == 0)
                {
                    isloaded = false;
                    SceneManager.LoadScene("TitleScene");
                }
                else if (Player.life >= 1)
                {
                    SceneManager.UnloadSceneAsync(sceneName);
                    SceneManager.LoadScene("MainScene", LoadSceneMode.Additive);
                    rootGameObject.transform.position = new Vector3(0, 5, 0);
                }
                else return;
            }
        }

        // ゴールしたか確認
        if(Player.isGoal) isloaded = false;
    }
}