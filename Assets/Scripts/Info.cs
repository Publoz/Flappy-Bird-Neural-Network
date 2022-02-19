using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Info : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject info;
    
    public void Click()
    {
        info.SetActive(true);
    }
}
