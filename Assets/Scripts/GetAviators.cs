using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetAviators : MonoBehaviour
{


    public Text t;
    // Start is called before the first frame update
    void Start()
    {

        if (PlayerPrefs.HasKey("aviators"))
        {
            t.text = "Aviators " + PlayerPrefs.GetInt("aviators");
        }
      
    }

    public void buy()
    {
        t.text = "Aviators " + PlayerPrefs.GetInt("aviators");
    }

    
}
