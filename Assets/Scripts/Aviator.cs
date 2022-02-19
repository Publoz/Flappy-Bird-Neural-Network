using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aviator : MonoBehaviour
{

    private float speed;
    private GameControl gc;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<GameControl>().addAviator();
            FindObjectOfType<AudioManager>().Play("chahoo");
            Destroy(gameObject);
        }
    }

    void Start()
    {
        gc = FindObjectOfType<GameControl>();
        speed = gc.speed;
        

    }

    void Update()
    {
        if (gc.alive && !gc.red)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);

        }
       
    }
}
