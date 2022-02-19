using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Close : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject panel;


    public void Click()
    {
        panel.SetActive(false);
    }
}
