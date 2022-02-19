using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Membrane : MonoBehaviour
{
    private GameControl gc;
    private float speed;
    public string tag;

    private float boundX;
    private SpriteRenderer sr;

    private GameObject other;

    void Start()
    {
        gc = FindObjectOfType<GameControl>();
        speed = gc.speed;
        sr = GetComponent<SpriteRenderer>();
        boundX = gc.getSize().x * -1f;
    }
    public void SetTag(string tag)
    {
        this.tag = tag;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            //Debug.Log("Trigger");
            if (!gc.Training())
            {
                gc.gameOver();
                return;
            }
            // 
            other.gameObject.GetComponent<Jump>().die();
        }
    }

    void Update()
    {

        if (gc.isAlive() && !gc.red)
        {
            // float speed = 0.035f;
           // Debug.Log(speed);
            transform.Translate(Vector2.left * speed * Time.deltaTime); 
        }

       if(transform.position.x + sr.size.x/2.0f < boundX)
        {
           // Debug.Log("removing wall");
            gc.SpawnNeu(tag);
            Destroy(gameObject);
        }
        
        
    }

    public void setOther(GameObject o)
    {
        other = o;
    }

    public GameObject getOther()
    {
        return other;
    }
    //void OnBecameInvisible()
    //{
    //    Debug.Log("Invisiible");
    //    if(transform.position.x <= 0)
    //    {
    //        gameObject.SetActive(false);
    //        gc.SpawnNeu(tag);
    //    }
        
    //}
}
