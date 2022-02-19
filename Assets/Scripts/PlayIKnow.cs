using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayIKnow : MonoBehaviour
{

    public void Here()
    {
        Invoke("Play", 1.5f);
    }

    public void Play()
    {
        FindObjectOfType<AudioManager>().Play("iknow");
    }

    public void Cancel()
    {
        CancelInvoke();
        FindObjectOfType<AudioManager>().Stop("iknow");
    }
}
