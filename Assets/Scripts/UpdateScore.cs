using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateScore : MonoBehaviour
{
    // Start is called before the first frame update

    public GameControl gc;
    private bool hit = false;

    void Start()
    {
        gc = FindObjectOfType<GameControl>();
    }
   
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")){
            
            if(hit == false && gc.Training())
            {
                Invoke("RemoveCollider", 1f);
            }

            hit = true;
            if (!gc.Training())
            {
                gc.addScore();
                Destroy(this.gameObject);
            }
            else
            {
                other.gameObject.GetComponent<Jump>().addScore();
            }
            
        }
        
    }

    public void RemoveCollider()
    {
        Destroy(this.gameObject);
    }
}
