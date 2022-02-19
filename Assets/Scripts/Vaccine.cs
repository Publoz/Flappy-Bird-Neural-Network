using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vaccine : MonoBehaviour
{
    private float speed;

    void Start()
    {
        speed = FindObjectOfType<GameControl>().speed;
        speed *= 1.5f; //counteract everything moving left
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
        if (transform.position.x < -7)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<GameControl>().gameOver();
            Destroy(gameObject);
        }
    }
}
