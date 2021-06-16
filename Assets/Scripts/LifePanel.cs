using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifePanel : MonoBehaviour
{
    [SerializeField] GameObject[] lifeIcons;

    public void UpdateLifeIcons(int life)
    {
        // 残ライフをUIで表示
        for (int i = 0; i < lifeIcons.Length; i++)
        {
            if (i < life) lifeIcons[i].SetActive(true);
            else lifeIcons[i].SetActive(false);
        }
    }
}
