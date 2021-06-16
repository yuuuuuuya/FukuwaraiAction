using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacePartsPanel : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] GameObject leftEye;
    [SerializeField] GameObject rightEye;
    [SerializeField] GameObject nose;
    [SerializeField] GameObject mouth;

    public void UpdateFacePartsIcons()
    {
        if (player.hasAttachedLeftEye) leftEye.SetActive(false);
        if (player.hasAttachedRightEye) rightEye.SetActive(false);
        if (player.hasAttachedNose) nose.SetActive(false);
        if (player.hasAttachedMouth) mouth.SetActive(false);
    }
}
