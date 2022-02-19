using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buy : MonoBehaviour
{
    private GetAviators t;


   public void OnBuy()
    {
        PlayerPrefs.SetInt("hasAviators", 1);
       // PlayerPrefs.SetInt("aviators", PlayerPrefs.GetInt("aviators") - 305);
        AudioManager a = FindObjectOfType<AudioManager>();
        a.Play("dale");
        a.Play("its");
        a.Play("mr");
        t = FindObjectOfType<GetAviators>();
        t.buy();

        FindObjectOfType<LoadButton>().reload();
    }
}
