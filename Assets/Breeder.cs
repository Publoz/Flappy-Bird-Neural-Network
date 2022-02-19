using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Breeder : MonoBehaviour
{

    public const int n = 9; //5
    public const int layers = 7; // 3
    public const int iNum = 3;

    private int gen = 1;
    private GameControl gc;

    private float[,] best;
    private float[,] bestInput;

    private float[,] pair;
    private float[,] pairInput;

    public static Breeder instance;

    private bool good = false;
    private bool superior = false;

    private List<Jump> ronas;

    private List<Weight> topWeights = new List<Weight>();

    // Start is called before the first frame update

    void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

        void Start()
    {
        gc = FindObjectOfType<GameControl>();
        
    }

    public void setRonas(List<Jump> r)
    {
        ronas = r;
    }

    public int Gen()
    {
        return gen;
    }

    public void storeBest()
    {
        float best = -1f;
        Jump bj = null;
        foreach(Jump j in ronas)
        {
            //j.GetComponent<AI>().calcDist();

            if(j.GetComponentInChildren<AI>().Fitness() > best)
            {
                best = j.GetComponentInChildren<AI>().Fitness();
                bj = j;
            } 
            //else if ( Mathf.Abs(j.GetComponentInChildren<AI>().Fitness() - best) <= 0.1)
            //{
            //    if(j.GetComponent<AI>().calcDist() < bj.GetComponentInChildren<AI>().calcDist())
            //    {
            //        bj = j;
            //    }
            //}
        }

        this.best = bj.GetComponentInChildren<AI>().getWeights();
        bestInput = bj.GetComponentInChildren<AI>().getInputWeights();

        WriteString(bj.GetComponentInChildren<AI>());
    }

    public bool getGood()
    {
        return good;
    }

    public void loadBest()
    {
        topWeights.Clear();
        gen++;
        Debug.Log(gen);
        float best = -1f;
        Jump bj = null;
        foreach (Jump j in ronas)
        {
            AI current = j.GetComponentInChildren<AI>();
            topWeights.Add(new Weight(current.getWeights(), current.getInputWeights(), current.Fitness()));
            if (j.GetComponentInChildren<AI>().Fitness() > best)
            {
                best = j.GetComponentInChildren<AI>().Fitness();
                bj = j;
            }
        }

        //this.best = bj.GetComponentInChildren<AI>().getWeights();
        //bestInput = bj.GetComponentInChildren<AI>().getInputWeights();
        topWeights.Sort();
        //Debug.Log(topWeights[0].getFitness());
        while(topWeights.Count > 3)
        {
            topWeights.RemoveAt(topWeights.Count - 1);
        }
        superior = false;
        int count = 0;
        foreach(Weight w in topWeights)
        {
            if(w.getFitness() > 1.5f)
            {
                count++;
            }
        }
        if(count == 3)
        {
            good = true;
        } else if(count == 1)
        {
            superior = true;
        }

        int index = 0;
        this.best = topWeights[index].getWeights();
        bestInput = topWeights[index].getInputWeights();

        topWeights.RemoveAt(index);
        index = Random.Range(0, 1);
        pair = topWeights[index].getWeights();
        pairInput = topWeights[index].getInputWeights();

        

    }

    public float[,] getBest()
    {
        return best;
    }

    public bool getSuperior()
    {
        return superior;
    }

    public float[,] getBestInput()
    {
        return bestInput;
    }

    public float[,] getPairBest()
    {
        return pair;
    }

    public float[,] getPairInput()
    {
        return pairInput;
    }

    public Weight readBest()
    {
        Debug.Log("reading");
        string path = Application.persistentDataPath + "/bestWeights.txt";
        StreamReader reader = new StreamReader(path);
        ////Print the text from the file
        //Debug.Log(reader.ReadToEnd());
        //reader.Close();

        string line = "";
        List<float> w = new List<float>();
        List<float> iW = new List<float>();
        bool input = true;
        while(true)
        {
            line = reader.ReadLine();
            if(line == null)
            {
                break;
            }
            if (line.Contains("WEIGHTS"))
            {
                input = false;
                continue;
            }
            else
            {
                if (input)
                {
                    iW.Add(float.Parse(line));
                }
                else
                {
                    w.Add(float.Parse(line));
                }
            }
        }

        float[,] weight = new float[n * layers, n];
        float[,] iWeights = new float[iNum, n];

        int count = 0;
        foreach(float f in w)
        {
            weight[count / n, count % n] = f;
                count++;
        }

        count = 0;
        foreach(float f in iW)
        {
            iWeights[count / n, count % n] = f;
            count++;
        }
        //for (int j = 0; j < n; j++)
        //{
        //    for (int i = 0; i < n * layers; i++)
        //    {
        //        weight[i, j] = w[i + j];
        //    }
        //}
        //for (int j = 0; j < n; j++)
        //{
        //    for (int i = 0; i < iNum; i++)
        //    {
        //        iWeights[i, j] = iW[i + j];
        //    }
        //}

        return new Weight(weight, iWeights, -1);
    }

    public static void WriteString(AI ai)
    {
        Debug.Log("writing");
        string path = Application.persistentDataPath + "/bestWeights.txt";
        // Debug.Log(path);
        //Write some text to the test.txt file

        if (File.Exists(path))
        {
            File.Delete(path);
        }

        StreamWriter writer = new StreamWriter(path, true);

        float[,] iw = ai.getInputWeights();

        //for (int col = 0; col < iw.GetLength(1); col++)
        //    for (int row = 0; row < iw.GetLength(0); row++)
        //        writer.WriteLine(iw[row, col]); 
        foreach(float f in iw)
        {
            writer.WriteLine(f);
        }


        writer.WriteLine("WEIGHTS");

        float[,] w = ai.getWeights();

        foreach(float f in w)
        {
            writer.WriteLine(f);
        }
        //for (int col = 0; col < w.GetLength(1); col++)
        //    for (int row = 0; row < w.GetLength(0); row++)
        //        writer.WriteLine(w[row, col]);

        writer.Close();
        //StreamReader reader = new StreamReader(path);
        ////Print the text from the file
        //Debug.Log(reader.ReadToEnd());
        //reader.Close();
    }
}
