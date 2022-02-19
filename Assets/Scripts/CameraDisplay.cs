using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDisplay : MonoBehaviour
{


    //public SpriteRenderer back;
    private Vector2 size = new Vector2(10f, 10f);

    // Start is called before the first frame update
    void Awake()
    {
        float screenRatio = (float)Screen.width / (float)Screen.height;
        float targetRatio = 1f;

        if(screenRatio >= targetRatio)
        {
            Camera.main.orthographicSize = size.y / 2;

        }
        else
        {
            float difference = targetRatio / screenRatio;
            Camera.main.orthographicSize = size.y / 2 * difference;
        }

        FindObjectOfType<GameControl>().setSize(Camera.main.orthographicSize * screenRatio, Camera.main.orthographicSize);
    }

    // Update is called once per frame
   
}
