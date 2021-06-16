using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tittle : MonoBehaviour
{
    public void OnStartButtonClicked()
    {
        SceneManager.LoadScene("MainScene");
    }
}
