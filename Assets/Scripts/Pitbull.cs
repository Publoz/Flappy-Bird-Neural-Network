using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pitbull : MonoBehaviour
{

    private float boundX;

    void Start()
    {
        boundX = FindObjectOfType<GameControl>().getSize().x * -1f;
    }
    // Update is called once per frame
    void Update()
    {
        //Debug.Log("dale");

        transform.Rotate(0, 0, 360 * Time.deltaTime);

        transform.position = Vector3.MoveTowards(transform.position, new Vector3(-30, transform.position.y, 0), 18f * Time.deltaTime);
        //transform.position.Set(this.gameObject.transform.position.x - 0.1f, this.gameObject.transform.position.y, this.gameObject.transform.position.z); ;

        if (transform.position.x + 0.5f < boundX)
        {
            Destroy(gameObject);
        }


    }
}
