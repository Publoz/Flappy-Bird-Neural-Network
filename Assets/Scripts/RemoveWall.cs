using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveWall : MonoBehaviour
{

    private GameControl gc;

    void Start()
    {
        gc = FindObjectOfType<GameControl>();
    }

   void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            // Destroy(other.gameObject); //doesnt work
            other.gameObject.SetActive(false);
            gc.SpawnNeu(other.GetComponent<Membrane>().tag);
        }
       
    }
}
