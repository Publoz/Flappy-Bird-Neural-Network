using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed;
    //private Vector3 target;

    void Start()
    {
        speed = FindObjectOfType<GameControl>().speed;
      //  target = new Vector3(transform.position.x + 50, transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
       transform.Translate(Vector2.right * speed * Time.deltaTime);
       
       // transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
       // transform.Rotate(0, 0, 360 * Time.deltaTime * 2.0f);

        if (transform.position.x > 12)
        {
            Destroy(gameObject);
        }
    }

  
}
