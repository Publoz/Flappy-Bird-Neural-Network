using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edge : MonoBehaviour
{
    public GameControl gc;
    public GameObject edge;

    private float speed;
    private SpriteRenderer sr;

    private float boundX;

    void Start()
    {
        gc = FindObjectOfType<GameControl>();
        speed = gc.speed;
        sr = GetComponent<SpriteRenderer>();

        boundX = gc.getSize().x * -1f;
    }

    void Update()
    {
       // float speed = 0.035f;

        if (gc.isAlive() && !gc.red)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
        
        if(transform.position.x + sr.size.x/2.0f < boundX)
        {
           // Debug.Log("Remove edge");
            GameObject make = Instantiate(edge, new Vector3(14.9f, transform.position.y), Quaternion.identity);
            if (transform.position.y >= 0)
            {
                make.GetComponent<SpriteRenderer>().flipY = true;
            }

            Destroy(gameObject);
        }

    }

    //void OnBecameInvisible()
    //{
       
    //    Debug.Log("Remove wall");
    //    GameObject make = Instantiate(edge, new Vector3(14.9f, transform.position.y), Quaternion.identity);
    //    if (transform.position.y >= 0)
    //    {
    //        make.GetComponent<SpriteRenderer>().flipY = true;
    //    }

    //    Destroy(gameObject);
    //}

   
    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log(other.gameObject.tag);
        if (other.gameObject.CompareTag("Player"))
        {
            if (gc.Training())
            {
                other.gameObject.GetComponent<Jump>().die();
                return;
            }
            gc.gameOver();
        }
    }
}
