
using UnityEngine;
using UnityEngine.UI;

public class GetHighScore : MonoBehaviour
{

    public Text t;


    private float r = 1f;
    private float b = 0f;
    private float g = 0f;

    private int change = 1;

    private float num = 0.01f;

   
    // 0 = red increase
    //1 = green increase
    //2 = blue increase
    //3 = red decrease
    //4 = green decrease
    //5 = blue decrease

    // Start is called before the first frame update

    void Awake()
    {
      //  PlayerPrefs.DeleteKey("highscore");
       // PlayerPrefs.DeleteKey("aviators"); //TODO REMOVE;
      //  PlayerPrefs.DeleteKey("hasAviators");

       // PlayerPrefs.SetInt("hasAviators", 0);

     //   PlayerPrefs.SetInt("aviators", 305);

      //  PlayerPrefs.SetInt("highscore", 12);
        if (PlayerPrefs.HasKey("highscore"))
        {

            t.text = "Highscore: " + PlayerPrefs.GetInt("highscore").ToString();

        }

    }
    

    void Update()
    {
        
        t.color = new Color(r, g, b);
       // Debug.Log(r + " " + g + " " + b);

        //RAINBOW CHANGING CODE
        if (change == 1)
        {
            g+=num;
            if (g >= 1f)
            {
                g = 1f;
                change = 3;
            }
        } else if (change == 3)
        {
            r-= num;
            if (r <= 0f)
            {
                r = 0f;
                change = 2;
            }
        } else if (change == 2)
        {
            b+= num;
            if (b >= 1f)
            {
                b = 1f;
                change = 4;
            }
        } else if (change == 4)
        {
            g-= num;
            if (g <= 0f)
            {
                g = 0f;
                change = 0;
            }
        } else if (change == 0)
        {
            r+= num;
            if(r >= 1f)
            {
                r = 1f;
                change = 5;
            }
        } else if(change == 5)
        {
            b-= num;
            if(b <= 0f)
            {
                b = 0f;
                change = 1;
            }
        }

    }

   
}
