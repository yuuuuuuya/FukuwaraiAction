using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FaceParts : MonoBehaviour
{
    AudioSource audioSource;

    [SerializeField] string sceneName;
    
    public static bool hasAttachedRightEye { get; set; } = false; // true→取得済み
    public static bool hasAttachedLeftEye { get; set; } = false; // true→取得済み
    public static bool hasAttachedNose { get; set; } = false; // true→取得済み
    public static bool hasAttachedMouth { get; set; } = false; // true→取得済み

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        //顔パーツを取得済みなら、各シーンで顔パーツobjectを非表示
        switch (sceneName)
        {
            case "LeftEyeScene":
                if (hasAttachedLeftEye) gameObject.SetActive(false);
                break;
            case "RightEyeScene":
                if (hasAttachedRightEye) gameObject.SetActive(false);
                break;
            case "NoseScene":
                if (hasAttachedNose) gameObject.SetActive(false);
                break;
            case "MouthScene":
                if (hasAttachedMouth) gameObject.SetActive(false);
                break;
            default:
                break;
        }
    }

    void Update()
    {
        // 顔パーツを動かす
        transform.Rotate(0.0f, 0.0f, 5.0f);
    }

    void OnTriggerEnter(Collider other)
    {
        // playerが衝突→音鳴らす
        if (other.tag == "Player") audioSource.Play();
    }
}
