using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FaceParts : MonoBehaviour
{
    [SerializeField] string sceneName;
    AudioSource audioSource;

    void Start()
    {
        //各シーンで顔パーツを取得済みなら顔パーツobjectを非表示
        Scene managerScene = SceneManager.GetSceneByName("MnagerScene");
        Player player;

        foreach (GameObject rootGameObject in managerScene.GetRootGameObjects())
        {
            player = rootGameObject.GetComponent<Player>();
            if (player == null) continue;
            switch (sceneName)
            {
                case "LeftEyeScene":
                    if (player.hasAttachedLeftEye) gameObject.SetActive(false);
                    break;
                case "RightEyeScene":
                    if (player.hasAttachedRightEye) gameObject.SetActive(false);
                    break;
                case "NoseScene":
                    if (player.hasAttachedNose) gameObject.SetActive(false);
                    break;
                case "MouthScene":
                    if (player.hasAttachedMouth) gameObject.SetActive(false);
                    break;
                default:
                    break;
            }
        }

        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        transform.Rotate(0.0f, 0.0f, 5.0f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") audioSource.Play();
    }
}
