using UnityEngine.UI;
using UnityEngine;

public class Score : MonoBehaviour
{

    public Text t;
    private bool training = false;
    private GameControl gc;
    void Start()
    {
        gc = FindObjectOfType<GameControl>();
        if (gc.Training())
        {
            training = true;
        }
    }

    void Update()
    {
        if (!training)
        {
            t.text = FindObjectOfType<GameControl>().score.ToString();
        }
        else
        {
            t.text = gc.trainingScore().ToString();
        }
        
    }
}
