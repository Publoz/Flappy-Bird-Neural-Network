using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{

    private GameControl gc;
    private float speed;
    // Start is called before the first frame update
    void Start()
    {
        gc = FindObjectOfType<GameControl>();
        speed = gc.speed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x - 0.3f, transform.position.y - 0.15f, 0), 0.2f);
        if(transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }
}
