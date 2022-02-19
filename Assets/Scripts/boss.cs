using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss : MonoBehaviour
{

    private int health = 10;
    public GameObject membrane;
    public GameObject hitMarker;
    public GameObject vaccine;
    private AudioManager am;
    bool up = true;
    private float speed = 2f;
    private float fireRate = 2.5f;
    private float nextFire = 0f;
    private Health healthBar;
    private GameObject healthy;
    private bool canFire = false;

    void Start()
    {
        am = FindObjectOfType<AudioManager>();
        am.Play("entrance");
        //healthy.SetActive(true);
        healthy = GameObject.FindGameObjectWithTag("healthBar");
        TurnOn(healthy);
        healthBar = healthy.GetComponentInChildren<Health>();
        //Debug.Assert(GameObject.FindGameObjectWithTag("healthBar") != null);
        // healthBar.gameObject.SetActive(true);
        Invoke("enableFire", 2f);
    }

    private void enableFire()
    {
        canFire = true;
    }

    private void TurnOn(GameObject healthy)
    {
        for (int i = 0; i < healthy.transform.childCount; i++)
        {
            healthy.transform.GetChild(i).gameObject.SetActive(true);
        }
    }


    // Update is called once per frame
    void Update()
    {

        if (up)
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.down * speed * Time.deltaTime);
        }


        if (up && transform.position.y >= 4)
        {
            up = false;
        } else if (!up && transform.position.y <= -4f){
            up = true;
        }

        if(canFire && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(vaccine, transform.position, Quaternion.identity);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("bullet"))
        {
            health -= 1;
            am.Play("oof");
            am.Play("hit");
            Destroy(other.gameObject);
            healthBar.setHealthBar(health * 0.1f);
            if(health >= 1)
            {
                Instantiate(hitMarker, transform.position, Quaternion.identity);
            }
            
            Invoke("removeHit", 0.1f);
            //Debug.Log("Boss hit");
            if (health <= 0)
            {
                healthBar.gameObject.SetActive(false);
                am.Play("yeah");
                FindObjectOfType<GameControl>().setVertGap(3.8f);
                Destroy(gameObject);
            }
        }
    }

    private void removeHit()
    {
        Destroy(GameObject.FindGameObjectWithTag("hit"));
    }
}
