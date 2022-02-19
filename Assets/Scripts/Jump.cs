using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Jump : MonoBehaviour
{

    Rigidbody2D rb;
    private bool jumped = false;
    private int count = 0;
    public Sprite av;
    public GameObject bullet;
    private GameControl gc;
    private float nextFire = 0f;
    public float fireRate = 0.5f;

    public float timeStarted;
    private int score;

    public GameObject spaceToJump;

    public bool died = false;

    void Start()
    {
        Physics2D.IgnoreLayerCollision(10, 10);
        gc = FindObjectOfType<GameControl>();
        rb = transform.GetComponent<Rigidbody2D>();

        if(PlayerPrefs.GetInt("hasAviators") == 1)
        {
            SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
            sr.sprite = av;
        }
        float[,] weights = gc.getBreeder().getBest();
        float[,] iWeights = gc.getBreeder().getBestInput();

        float[,] p = gc.getBreeder().getPairBest();
        float [,] ip = gc.getBreeder().getPairInput();
        if (gc.getBreeder().Gen() == 1 && gc.Training())
        {
            GetComponentInChildren<AI>().Init(true,null, null, null, null);
        }
        else if (gc.Training())
        {
            GetComponentInChildren<AI>().Init(false, weights, iWeights, p, ip);
        }
        
        
    }

    public bool getJumped()
    {
        return jumped;
    }

    public void setJumped(bool j)
    {
        jumped = j;
    }

    public void addScore()
    {
        score++;
    }

    public int Score()
    {
        return score;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
       // Debug.Log("collide");
        if (other.gameObject.CompareTag("vaccine"))
        {
         
            gc.gameOver();
            Destroy(other.gameObject);
        }
    }

    public void die()
    {
        died = true;
        //gc.removeRona(this);
        gameObject.SetActive(false);
    }

    public void itsMr305()
    {
        FindObjectOfType<AudioManager>().Play("its");
    }

    public void Spring()
    {
        if (gc.red)
        {
            gc.gameOver();
        }


        if (!jumped)
        {
            timeStarted = Time.timeSinceLevelLoad;
            jumped = true;
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 2;
            spaceToJump.GetComponent<Animation>().Play();
            //  FindObjectOfType<GameControl>().SpawnInital();
            if (!gc.Training())
            {
                FindObjectOfType<GameControl>().init();
                Invoke("itsMr305", 1f);
            }
            // spaceToJump.SetActive(false);
        }
        //Spring();

        count++;
        float jump = 7f; //was 7f without deltaTiem
        rb.velocity = Vector2.up * jump;

        if (count % 10 == 0 && !gc.Training())
        {
            FindObjectOfType<AudioManager>().Play("haha");
        }
    }
   
    void Update()
    {
        if (died)
        {
            return;
        }

        if (!jumped)
        {
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
            gameObject.GetComponent<Transform>().position = new Vector3(gameObject.GetComponent<Transform>().position.x, 0); //on first time, slowly drifts downs
        }

        if (Input.GetKeyDown("space") || Input.touchCount > 0)
        {

            Spring();
           
        }

        if (Input.GetKeyDown("e"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }

        if ((Input.GetKeyDown("p") || Input.GetKeyDown("a")) && Time.time > nextFire)
        {
            Instantiate(bullet, new Vector3(transform.position.x, transform.position.y, 2), Quaternion.identity);
            nextFire = Time.time + fireRate;
            FindObjectOfType<AudioManager>().Play("barrett");
        }
    }
}
