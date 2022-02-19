
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;
using EZCameraShake;

public class GameControl : MonoBehaviour
{
    private const float horzGap = 8f;
    private float vertGap = 3.8f; //3.6

    private bool training = false;

    private bool ai = false;


    private bool started = false;
    private bool playing = false;
    private GameObject AIBreeder;
    private List<Jump> ronas;
    public GameObject ronaPrefab;
    public GameObject fitnessScores;
    public GameObject saveButton;

    public GameObject wall;
    public GameObject scoreCollider;

    public GameObject spawnLoc;

    public GameObject explosion;

    public GameObject pitbull;

    public GameObject lighting;
    public GameObject postProcessing;


    public GameObject spotlight;

    public GameObject wink;

    public GameObject newHs;

    public GameObject confetti;
    public GameObject rainParticles;

    public GameObject gameOverScreen;

    public GameObject aviator;

    public GameObject darken;

    public GameObject fireball;

    public GameObject background;
    private Sprite oldBackground;
    public GameObject camera;

    public GameObject[] redgreen = new GameObject[2];

    private Membrane closest;

    //private int fireballLevel = 20; //20
    //private int redGreenLevel = 80; //80
    //private int rainOverMeLevel = 40; //40
    //private int bossLevel = 100; //60
    //private int feelGoodLevel = 10;

    Dictionary<string, int> levels = new Dictionary<string, int>();


    // public 

    public GameObject mem;

    //public GameObject secondBack;

    private int totalAviators;
    // private int aviators = 0;
    public GameObject boss;

    [HideInInspector]
    public Jump rona;

    [HideInInspector]
    public bool red = false;

    public int score = 0;
    public int highscore = 0;

    public bool alive = true;

    public bool passed = false;

    public float speed = 0.0305f; //0.035f is Default

    private int lastSpawn = 10;

    private Vector3[] colors;
   // private int index = 0;

    //private int screenHeight;

    public GameObject fastForward;
    public GameObject play;

    private Vector2 size = new Vector2();

    // public GameObject camera;


    void Update()
    {

        if ((!started && !playing))
        {
            return;
        }
        if ((Input.GetKeyDown("space") || started) && training)
        {
            init();
            started = false;
            playing = true;
        }

        List<float> scores = new List<float>();
        bool finished = true;
        if (training)
        { 
            for(int i = 0; i < ronas.Count; i++)
            {
                if (!ronas[i].died)
                {
                    scores.Add(ronas[i].GetComponentInChildren<AI>().Fitness());
                }
                if (!ronas[i].died)
                {
                    finished = false;
                }
            }
            fitnessScores.SetActive(true);
            Text t = fitnessScores.GetComponent<Text>();

            

            t.text = "Gen: " + AIBreeder.GetComponent<Breeder>().Gen() + "\n";
            t.text += "Alive: " + scores.Count + "\n";
            foreach(float f in scores)
            {
                t.text += f + "\n";
            }
            


            if (finished)
            {
                finished = false;
               // Debug.Log("DONE");
                alive = false;
                Invoke("nextGen", 1f);
                //AIBreeder.GetComponent<Breeder>().storeBest(ronas);
            }
           
         // Debug.Log(ronas[0].GetComponentInChildren<AI>().fitness);
        }    
    }

    public void nextGen()
    {
        AIBreeder.GetComponent<Breeder>().loadBest();
        nextLevel();
    }

   public List<Jump> Ronas()
    {
        return ronas;
    }

    

    public void setSize(float x, float y)
    {
        size.x = x;
        size.y = y;
    }


    public Vector2 getSize()
    {
        return size;
    }

    public Breeder getBreeder()
    {
        return AIBreeder.GetComponent<Breeder>();
    }

    public void startAuto()
    {
        // Debug.Log("Once plz: " + AIBreeder.GetComponent<Breeder>().Gen().ToString());
        if (training)
        {
            started = true;
            foreach (Jump j in ronas)
            {
                j.Spring();
            }
        }
        else
        {
            rona.Spring();
        }
        
    }

    void Awake()
    {

        if(PlayerPrefs.GetInt("training") == 1)
        {
            training = true;
            
        }

        AIBreeder = FindObjectOfType<Breeder>().gameObject;

        if (training)
        {
            saveButton.SetActive(true);
            ronas = new List<Jump>();
            AIBreeder = FindObjectOfType<Breeder>().gameObject;
            Invoke("startAuto", 0.2f);
            int num = 60;

            if(AIBreeder.GetComponent<Breeder>().Gen() == 1)
            {
                num = 230;
            }
            for(int i = 0; i < num; i++)
            {
                GameObject nR = Instantiate(ronaPrefab, new Vector3(-1.75f, 0), Quaternion.identity);
                ronas.Add(nR.GetComponent<Jump>());
                //Instantiate()
                // Rona and AI need to be child / parent
                // go to next gen when they're all dead - no pop up
                // ideally remove the space to start
            }
            AIBreeder.GetComponent<Breeder>().setRonas(ronas);
        }
        else if(PlayerPrefs.GetInt("ai") == 1)
        {
            ai = true;
            rona = Instantiate(ronaPrefab, new Vector3(-1.75f, 0), Quaternion.identity).GetComponent<Jump>();
            Weight winner = AIBreeder.GetComponent<Breeder>().readBest();
            rona.GetComponentInChildren<AI>().setWeights(winner);
        }
        else
        {
            rona = Instantiate(ronaPrefab, new Vector3(-1.75f, 0), Quaternion.identity).GetComponent<Jump>();
            rona.GetComponentInChildren<AI>().gameObject.SetActive(false);
            
        }

        if(ai || training)
        {
            Invoke("startAuto", 0.2f);
        }

        oldBackground = background.GetComponent<SpriteRenderer>().sprite;

        if (PlayerPrefs.HasKey("highscore"))
        {
            highscore = PlayerPrefs.GetInt("highscore");
        }
        // PlayerPrefs.SetInt("highscore", 1); //TODO REMOVE
       //highscore = 2; //TODO
        

        if (PlayerPrefs.HasKey("aviators"))
        {
           // Debug.Log("FOUND aviators");
            totalAviators = PlayerPrefs.GetInt("aviators");
        } else
        {
            totalAviators = 0;
            PlayerPrefs.SetInt("aviators", 0);
        }
        //aviators = 0;
        rona = FindObjectOfType<Jump>();
       // Debug.Log("yo");

        colors = new Vector3[7];
        colors[0] = new Vector3(148, 0, 211);
        colors[1] = new Vector3(75, 0, 130);
        colors[2] = new Vector3(0, 0, 255);
        colors[3] = new Vector3(0, 255, 0);
        colors[4] = new Vector3(255, 255, 0);
        colors[5] = new Vector3(255, 127, 0);
        colors[6] = new Vector3(255, 0, 0);

       // screenHeight = 10;
        Time.timeScale = 1f;

        levels.Add("fireball", 0);
        levels.Add("boss", 0);
        levels.Add("redGreen", 0);
        levels.Add("rain", 0);
        levels.Add("feelGood", 0);


        //int[] nums = new int[levels.Keys.Count];
        var nums = new List<int>();
        for(int i = 0; i < levels.Keys.Count; i++)
        {
            nums.Add((i + 1) * 20);
        }

        int index = Random.Range(0, nums.Count);
        levels["fireball"] = nums[index];
        nums.RemoveAt(index);

         index = Random.Range(0, nums.Count);
        levels["rain"] = nums[index];
        nums.RemoveAt(index);

         index = Random.Range(0, nums.Count);
        levels["feelGood"] = nums[index];
        nums.RemoveAt(index);

         index = Random.Range(0, nums.Count);
        levels["redGreen"] = nums[index];
        nums.RemoveAt(index);

         index = Random.Range(0, nums.Count);
        levels["boss"] = nums[index];
        nums.RemoveAt(index);

    }

    public bool Training()
    {
        return training;
    }

   

    public void setVertGap(float gap)
    {
        vertGap = gap;
    }

    public void gameOver()
    {
        if (!alive)
        {
            return;
        }

        FindObjectOfType<AudioManager>().stopOtherSongs();
        

        darken.SetActive(false);
        alive = false;
        Debug.Log("GameOver");
        Time.timeScale = 1f;
        GameObject player = GameObject.FindGameObjectWithTag("Player");


        Instantiate(explosion, player.transform.position, Quaternion.identity);

        PlayerPrefs.SetInt("aviators", totalAviators);

      //  Debug.Log(PlayerPrefs.GetInt("aviators"));
        

        if (score > highscore)
        {
            PlayerPrefs.SetInt("highscore", score);
        }
        //PlayerPrefs.Save();
        player.SetActive(false);
        if (!training)
        {
            Invoke("addGameOverScreen", 1.4f);
            PlayerPrefs.Save();
        }
       
        //Invoke("nextLevel", 2f);

    }

    public int trainingScore()
    {
        foreach(Jump j in ronas)
        {
            if(j.died == false)
            {
                return j.Score();
            }
        }
        return -1;
    }

    public void addAviator()
    {
        totalAviators += 1;
    }

    public void addGameOverScreen()
    {
        gameOverScreen.SetActive(true);
    }

    public bool isAlive()
    {
        return alive;
    }

    public void nextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public int getScore()
    {
        return score;
    }

    private void disableNewHs()
    {
        newHs.SetActive(false);
        confetti.GetComponent<ParticleSystem>().Stop(false);
     //   darken.SetActive(false);
        spotlight.SetActive(false);
        lighting.GetComponent<Light2D>().intensity += 0.3f;

    }

    public void addScore()
    {
        score++;

        if (!training)
        {
            if (score > highscore && !passed)
            {
                FindObjectOfType<AudioManager>().Play("bada");
                passed = true;
                Instantiate(wink, new Vector3(0, -4, 0), Quaternion.identity);
                newHs.SetActive(true);
                confetti.SetActive(true);
                // confetti.GetComponent<ParticleSystem>().shape.scale.Set((size.x * 2.0f, 1, 1));
                var sh = confetti.GetComponent<ParticleSystem>().shape;
                sh.scale = new Vector3(size.x * 2.0f, 1, 1);
                confetti.GetComponent<ParticleSystem>().emissionRate = sh.scale.x * 6;
                // darken.SetActive(true);
                spotlight.SetActive(true);
                lighting.GetComponent<Light2D>().intensity -= 0.3f;
                Invoke("disableNewHs", 2f);
            }
            else if (score % 10 == 0)
            {
                FindObjectOfType<AudioManager>().Play("dale");
                spawnFlyingPitbull();

            }
            else if (score % 5 == 0)
            {
                this.GetComponentInParent<AudioSource>().Play();
                // Instantiate(pitbull, new Vector3(12, Random.Range(-4, 4)), Quaternion.identity);
                spawnFlyingPitbull();
            }


            if (score == levels["rain"]) //Rain over me
            {
                FindObjectOfType<AudioManager>().Play("rain", true); //was 20f
                rainParticles.SetActive(true);
                darken.SetActive(true);

                InvokeRepeating("discoFX", 11.4f, 0.15f);
            }
            else if (score == levels["rain"] + 10)
            {
                rainParticles.GetComponent<ParticleSystem>().Stop(false);
                darken.SetActive(false);
                background.GetComponent<SpriteRenderer>().sprite = oldBackground;
                postProcessing.SetActive(false);
                CancelInvoke();
            }
            else if (score == levels["fireball"]) // Armageddon
            {
                FindObjectOfType<AudioManager>().Play("fireball", true);
                //other
                InvokeRepeating("armageddon", 8.47f, 0.8f);
            }
            else if (score == levels["fireball"] + 10)
            {
                CancelInvoke();
            }
            else if (score == levels["redGreen"]) //Red light green light
            {
                InvokeRepeating("RedGreen", 0.3f, 1.5f);
                // lighting.GetComponent<Light2D>().intensity = 0.8f;
            }
            else if (score == levels["redGreen"] + 10)
            {
                CancelInvoke();
                //lighting.GetComponent<Light2D>().color = new Color(255, 255, 255);
                redgreen[0].SetActive(false);
                redgreen[1].SetActive(false);
                rona.GetComponent<Rigidbody2D>().gravityScale = 2;
            }
            else if (score == levels["boss"])
            {
                Instantiate(boss, new Vector3(size.x - 1, 0), Quaternion.identity);
                vertGap += 0.6f;
            }
            else if (score == levels["feelGood"])
            {
                FindObjectOfType<AudioManager>().Play("feelGood", true);
                InvokeRepeating("feelGood", 8.65f, 0.8f);
            }
            else if (score == levels["feelGood"] + 10)
            {
                CancelInvoke();
                Time.timeScale = 1f;
                play.SetActive(false);
                fastForward.SetActive(false);
            }
        }
       

    }

    private void spawnFlyingPitbull()
    {
        Instantiate(pitbull, new Vector3(size.x+5, Random.Range(-size.y + 1, size.y-1)), Quaternion.identity);
    }

    private void feelGood()
    {
        if(Time.timeScale == 1f)
        {
            Time.timeScale = 0.6f;
            // fastForward.SetActive(true);
            // fastForward.transform.localRotation()
            play.SetActive(false);
            fastForward.SetActive(true);
            // fastForward.transform.Rotate(new Vector3())
            fastForward.GetComponent<SpriteRenderer>().flipX = true;
        } else if(Time.timeScale == 0.6f)
        {
            Time.timeScale = 1.4f;
            fastForward.SetActive(true);
            fastForward.GetComponent<SpriteRenderer>().flipX = false;
            play.SetActive(false);
        } else
        {
            Time.timeScale = 1f;
            play.SetActive(true);
            fastForward.SetActive(false);
        }
    }

    private void RedGreen()
    {
        if(Random.Range(1, 10) == 5)
        {
            FindObjectOfType<AudioManager>().Play("red");
            // lighting.GetComponent<Light2D>().color = new Color(255, 0, 0);
            redgreen[0].SetActive(true);
            redgreen[1].SetActive(false);
            red = true;
            rona.GetComponent<Rigidbody2D>().gravityScale = 0;
            rona.GetComponent<Rigidbody2D>().velocity = Vector3.zero;


        }
        else
        {
            FindObjectOfType<AudioManager>().Play("green");
            //lighting.GetComponent<Light2D>().color = new Color(0, 255, 0);
            redgreen[1].SetActive(true);
            redgreen[0].SetActive(false);
            red = false;
            rona.GetComponent<Rigidbody2D>().gravityScale = 2;
        }
    }

    public float getDistAway(Jump rona)
    {
       // Jump rona = FindObjectOfType<Jump>();
        Membrane[] arr = FindObjectsOfType<Membrane>();
        float dist = 999f;
        float y = 999f;
        int count = 0;
        foreach (Membrane m in arr)
        {
            count++;
            // Debug.Log(m.transform.localPosition.x);

            if (m.transform.position.x + 0.3f < rona.transform.position.x || m.transform.root.gameObject.GetComponent<UpdateScore>() != null)
            {

                continue;
            }
            else
            {
                if ((m.transform.position.x - rona.transform.position.x) <= dist+0.1f)
                {
                    if(m.tag.Equals("bottom"))
                    {
                        closest = m;
                        y = m.transform.position.y;
                        dist = (m.transform.position.x - rona.transform.position.x);
                    }
                   
                    
                }
            }
        }
        
       
        return dist;
    }

    public float getMiddle()
    {
        
        Debug.Assert(closest.tag.Equals("bottom"));
       // Debug.Log(closest.transform.position.y);

        return ((closest.transform.position.y + closest.transform.root.GetComponent<SpriteRenderer>().size.y / 2.0f) + (vertGap / 2.0f)); //+ 
              //  (closest.getOther().transform.position.y - closest.transform.root.GetComponent<SpriteRenderer>().size.y /2.0f)) / 2.0f;

    }


    private void armageddon()
    {
        Instantiate(fireball, new Vector3(Random.Range(3, 24), 8), Quaternion.identity);
        camera.GetComponent<Shake>().ShakeCamera();
    }

    private void discoFX()
    {
        postProcessing.SetActive(true);
        background.GetComponent<SpriteRenderer>().sprite = null;

        int index = Random.Range(0, 7);
        camera.GetComponent<Camera>().backgroundColor = new Color(colors[index].x / 255f, colors[index].y / 255f, colors[index].z / 255f);
        darken.GetComponent<Image>().color = new Color(colors[index].x / 255f, colors[index].y / 255f, colors[index].z / 255f); 
        
    }

    public void init()
    {

        if (training)
        {
            float spawnInit = size.x + 1f;

            for (int i = 0; i < 4; i++)
            {
                int type = (int)Random.Range(0, 3);
                if (type == 0)
                {
                    GameObject o = Instantiate(mem, new Vector3(spawnInit + i * horzGap, -3.8f), Quaternion.identity);
                    o.GetComponent<Membrane>().SetTag("bottom");
                    o.GetComponent<SpriteRenderer>().size = new Vector2(0.9f, 2.5f);



                    GameObject oo = Instantiate(mem, new Vector3(spawnInit + i * horzGap, 3), Quaternion.identity);
                    oo.GetComponent<Membrane>().SetTag("top");
                    oo.GetComponent<SpriteRenderer>().size = new Vector2(0.9f, 4f);

                    o.GetComponent<Membrane>().setOther(oo);
                    oo.GetComponent<Membrane>().setOther(o);

                    Instantiate(scoreCollider, new Vector3((spawnInit + i * horzGap) + 0.5f, 0), Quaternion.identity);
                } else if(type == 1)
                {
                    GameObject o = Instantiate(mem, new Vector3(spawnInit + i * horzGap, -4.6f), Quaternion.identity);
                    o.GetComponent<Membrane>().SetTag("bottom");
                    o.GetComponent<SpriteRenderer>().size = new Vector2(0.9f, 0.79f);

                    GameObject oo = Instantiate(mem, new Vector3(spawnInit + i * horzGap, 2.29f), Quaternion.identity);
                    oo.GetComponent<Membrane>().SetTag("top");
                    oo.GetComponent<SpriteRenderer>().size = new Vector2(0.9f, 5.4f);

                    o.GetComponent<Membrane>().setOther(oo);
                    oo.GetComponent<Membrane>().setOther(o);

                    Instantiate(scoreCollider, new Vector3((spawnInit + i * horzGap) + 0.5f, 0), Quaternion.identity);
                }
                else
                {
                    GameObject o = Instantiate(mem, new Vector3(spawnInit + i * horzGap, -2.12f), Quaternion.identity);
                    o.GetComponent<Membrane>().SetTag("bottom");
                    o.GetComponent<SpriteRenderer>().size = new Vector2(0.9f, 5.76f);

                    GameObject oo = Instantiate(mem, new Vector3(spawnInit + i * horzGap, 4.78f), Quaternion.identity);
                    oo.GetComponent<Membrane>().SetTag("top");
                    oo.GetComponent<SpriteRenderer>().size = new Vector2(0.9f, 0.43f);


                    o.GetComponent<Membrane>().setOther(oo);
                    oo.GetComponent<Membrane>().setOther(o);
                    Instantiate(scoreCollider, new Vector3((spawnInit + i * horzGap) + 0.5f, 0), Quaternion.identity);
                }

                
            }
        }
        else
        {


            float spawnInit = size.x + 1f;

            for (int i = 0; i < 4; i++)
            {
                GameObject o = Instantiate(mem, new Vector3(spawnInit + i * horzGap, -3.8f), Quaternion.identity);
                o.GetComponent<Membrane>().SetTag("bottom");
                o.GetComponent<SpriteRenderer>().size = new Vector2(0.9f, 2.5f);

                GameObject oo = Instantiate(mem, new Vector3(spawnInit + i * horzGap, 3), Quaternion.identity);
                oo.GetComponent<Membrane>().SetTag("top");
                oo.GetComponent<SpriteRenderer>().size = new Vector2(0.9f, 4f);

                o.GetComponent<Membrane>().setOther(oo);
                oo.GetComponent<Membrane>().setOther(o);

                Instantiate(scoreCollider, new Vector3((spawnInit + i * horzGap) + 0.5f, 0), Quaternion.identity);
            }

        }

    }

    public void SpawnNeu(string tag)
    {

        if (lastSpawn + 10 > Time.frameCount)
        {
            return;
        }

        lastSpawn = Time.frameCount;

        float middleLoc = (Random.Range(2, (size.y * 2.0f)-2));
        Vector3 spawnPos = new Vector3();
        Membrane[] arr = FindObjectsOfType<Membrane>();

        string targ;
        if (tag.Equals("first")) //OBSOLOTE
        {
            targ = "second";
        }
        else
        {
            targ = "first";
        }

        float x = -999f;
        foreach (Membrane m in arr)
        {
            if (m.transform.position.x > x)
            {
                //spawnPos.x = m.transform.position.x + horzGap;
                // break;
                x = m.transform.position.x;
            }
        }

        spawnPos.x = x + horzGap;

        //Top
        float height = Mathf.Abs( middleLoc - (vertGap / 2.0f));
        spawnPos.y = - height/2.0f + (size.y * 2.0f) / 2.0f;
        
        GameObject top = Instantiate(mem, spawnPos, Quaternion.identity);
        //top size
        top.GetComponent<SpriteRenderer>().size = new Vector2(0.9f, height);
        top.GetComponent<Membrane>().SetTag("top");

        //Bottom
        float bHeight = ((size.y * 2.0f) - vertGap) - height;
        Debug.Assert(bHeight < (size.y * 2.0f));
        
        spawnPos.y = spawnPos.y - height/2.0f - vertGap - bHeight/2.0f;
        GameObject bot = Instantiate(mem, spawnPos, Quaternion.identity);
        bot.GetComponent<SpriteRenderer>().size = new Vector2(0.9f, bHeight);
        bot.GetComponent<Membrane>().SetTag("bottom");

        top.GetComponent<Membrane>().setOther(bot);
        bot.GetComponent<Membrane>().setOther(top);

        spawnPos.x += 0.5f;
        spawnPos.y = 0;
        GameObject c = Instantiate(scoreCollider, spawnPos, Quaternion.identity); //collider
      
       
        if(Random.Range(1,9) == 5) //Aviator
        {
            Instantiate(aviator, new Vector3(spawnPos.x + Random.Range(2, 6), Random.Range(-4, 4)), Quaternion.identity);
        }

    }



}
//First set of walls
////  GameObject o = Instantiate(mem, new Vector3(12, -3.8f), Quaternion.identity);
//  o.GetComponent<Membrane>().SetTag("first");
//  o.GetComponent<SpriteRenderer>().size = new Vector2(0.9f, 2.5f);

//  o = Instantiate(mem, new Vector3(12, 3), Quaternion.identity);
//  o.GetComponent<Membrane>().SetTag("first");
//  o.GetComponent<SpriteRenderer>().size = new Vector2(0.9f, 4f); 


//  GameObject c = Instantiate(scoreCollider, new Vector3(12.5f, 0) , Quaternion.identity);

//  //Second
//  o = Instantiate(mem, new Vector3(12 + horzGap, -3.8f), Quaternion.identity);
//  o.GetComponent<Membrane>().SetTag("second");
//  o.GetComponent<SpriteRenderer>().size = new Vector2(0.9f, 2.5f);

//  o = Instantiate(mem, new Vector3(12 + horzGap, 3), Quaternion.identity);
//  o.GetComponent<Membrane>().SetTag("second");
//  o.GetComponent<SpriteRenderer>().size = new Vector2(0.9f, 4f);
//  Instantiate(scoreCollider, new Vector3(12.5f + horzGap, 0), Quaternion.identity);
// c.transform.localScale = new Vector3(1, 10);