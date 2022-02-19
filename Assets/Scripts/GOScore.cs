using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GOScore : MonoBehaviour
{

    private bool on = true;

    private string content;

    void Start()
    {
        GameControl gc = FindObjectOfType<GameControl>();
        if (gc.score > gc.highscore)
        {
           // Debug.Log(gc.score.ToString());
           // Debug.Log(gc.highscore.ToString());
            content = "New Highscore " + gc.score.ToString() + "!";
        }
        else
        {
            content = "You got " + gc.score.ToString();
        }
       
        gameObject.GetComponent<Text>().text = content;

        if(gc.score == 1)
        {
            FindObjectOfType<AudioManager>().Play("uno");
        }
       
        InvokeRepeating("flip", 0f, 0.3f);
    }

    private void flip()
    {
        if (on)
        {
            on = false;
            gameObject.GetComponent<Text>().text = "";
        }
        else
        {
            on = true;
            gameObject.GetComponent<Text>().text = content;
        }
    }

}
