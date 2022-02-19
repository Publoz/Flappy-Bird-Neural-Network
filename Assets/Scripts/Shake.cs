using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class Shake : MonoBehaviour
{
   public void ShakeCamera()
    {
        CameraShaker.Instance.ShakeOnce(4f, 4f, .1f, .3f);
    }
}
