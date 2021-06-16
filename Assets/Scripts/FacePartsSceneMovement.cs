using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FacePartsSceneMovement : MonoBehaviour
{
    [SerializeField] string sceneName;

    void Start()
    {
        //顔パーツを取得済みのFacePartsSpherは色を変更
        Material material = GetComponent<Renderer>().material;
        Scene managerScene = SceneManager.GetSceneByName("MnagerScene");
        Player player;
        foreach (GameObject rootGameObject in managerScene.GetRootGameObjects())
        {
            player = rootGameObject.GetComponent<Player>();
            if (player == null) continue;
            switch (sceneName)
            {
                case "LeftEyeScene":
                    if (player.hasAttachedLeftEye) material.color = Color.red;
                    break;
                case "RightEyeScene":
                    if (player.hasAttachedRightEye) material.color = Color.red;
                    break;
                case "NoseScene":
                    if (player.hasAttachedNose) material.color = Color.red;
                    break;
                case "MouthScene":
                    if (player.hasAttachedMouth) material.color = Color.red;
                    break;
                default:
                    break;
            }
        }
    }

    void OnTriggerEnter()
    {
        SceneManager.UnloadSceneAsync("MainScene");
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }
}
