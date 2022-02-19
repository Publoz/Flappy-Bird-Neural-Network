using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LoadButton : MonoBehaviour
{

    public GameObject button;
    // Start is called before the first frame update

    

    void Start()
    {
        int val = PlayerPrefs.GetInt("aviators");
        if(val >= 305)
        {
            button.SetActive(true);
           
        }
        else
        {
            
            button.SetActive(false);
        }
        
    }

    public void reload()
    {
        int val = PlayerPrefs.GetInt("aviators");
        if (val >= 305)
        {
            button.SetActive(true);

        }
        else
        {

            button.SetActive(false);
        }
    }
    
}
