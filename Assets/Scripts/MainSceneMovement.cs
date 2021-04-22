using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneMovement : MonoBehaviour
{
    [SerializeField] string sceneName;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            SceneManager.UnloadSceneAsync(sceneName);
            SceneManager.LoadScene("MainScene", LoadSceneMode.Additive);
        }
    }
}
