using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiftSecondBack : MonoBehaviour
{
    // Start is called before the first frame update

    private float speed;
    private GameControl gc;
    public GameObject self;

    void Start()
    {
        gc = FindObjectOfType<GameControl>();
        speed = gc.speed / 2.0f;
        
    }


    void Update()
    {
        if (gc.isAlive())
        {
            // float speed = 0.035f;
            // Debug.Log(speed);
            transform.Translate(Vector2.left * speed * Time.deltaTime);
            if(transform.position.x < -10)
            {
                
                Instantiate(self, new Vector3(15, 0), Quaternion.identity);
                gameObject.SetActive(false);
            }
        }
    }

    
}
