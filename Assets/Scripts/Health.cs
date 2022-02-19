using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    // Start is called before the first frame update

    private Slider slider;

    void Start()
    {
        slider = GetComponent<Slider>();
    }

    public void setHealthBar(float val)
    {
        slider.value = val;
        if(val == 0.2f)
        {
            GameObject.FindGameObjectWithTag("fill").GetComponent<Image>().color = new Color(1, 0.6f, 0);
        } else if(val == 0.1f)
        {
            GameObject.FindGameObjectWithTag("fill").GetComponent<Image>().color = new Color(1, 0, 0);
        }
    }
}
