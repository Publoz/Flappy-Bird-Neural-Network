using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGen : MonoBehaviour
{


    public GameObject scores;

    void Start()
    {
        if (!FindObjectOfType<GameControl>().Training())
        {
            gameObject.SetActive(false);
            scores.SetActive(false);
        }  
    }

    public void Save()
    {
        FindObjectOfType<Breeder>().storeBest();
    }
}
