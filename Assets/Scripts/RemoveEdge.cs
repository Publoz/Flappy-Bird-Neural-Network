using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveEdge : MonoBehaviour
{

    public GameObject edge;
    
   void OnTriggerEnter2D(Collider2D other)
    {
        other.gameObject.SetActive(false);
        Instantiate(edge, new Vector3(15, 0.5f), Quaternion.identity); // top
    }
}
