using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FacePartsSceneMovement : MonoBehaviour
{
    Material facePartsSpherMaterial;

    [SerializeField] string sceneName;

    void Start()
    {
        //顔パーツを取得済みのFacePartsSpherは色を変更
        facePartsSpherMaterial = GetComponent<Renderer>().material;
        switch (sceneName)
        {
            case "LeftEyeScene":
                if (FaceParts.hasAttachedLeftEye) facePartsSpherMaterial.color = Color.red;
                break;
            case "RightEyeScene":
                if (FaceParts.hasAttachedRightEye) facePartsSpherMaterial.color = Color.red;
                break;
            case "NoseScene":
                if (FaceParts.hasAttachedNose) facePartsSpherMaterial.color = Color.red;
                break;
            case "MouthScene":
                if (FaceParts.hasAttachedMouth) facePartsSpherMaterial.color = Color.red;
                break;
            default:
                break;
        }
    }
    void OnTriggerEnter()
    {
        // シーンの切り替え
        SceneManager.UnloadSceneAsync("MainScene");
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }
}
