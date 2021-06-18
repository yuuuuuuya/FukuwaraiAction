using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacePartsPanel : MonoBehaviour
{
    [SerializeField] GameObject leftEye;
    [SerializeField] GameObject rightEye;
    [SerializeField] GameObject nose;
    [SerializeField] GameObject mouth;

    public void UpdateFacePartsIcons()
    {
        // 取得したら顔パーツを非表示
        if (FaceParts.hasAttachedLeftEye) leftEye.SetActive(false);
        if (FaceParts.hasAttachedRightEye) rightEye.SetActive(false);
        if (FaceParts.hasAttachedNose) nose.SetActive(false);
        if (FaceParts.hasAttachedMouth) mouth.SetActive(false);
    }
}
