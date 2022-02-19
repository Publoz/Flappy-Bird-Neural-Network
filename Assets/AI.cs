using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    private Jump rona;

    private float fitness;

    private float distOnDeath;


    private float[,] neurons;
    private float[,] weights;

    private float[] inputs;
    private float[,] iWeights;
    private int output;

    public const int n = 9; //5
    public const int layers = 7; // 3
    public const int iNum = 3;

    private const int mutationChance = 14;
    private const float mutationAmount = 100f;

    private const float ranNum = 1000f;

    private bool jumped = false;

    private GameControl gc;


    public void Init(bool init, float[,] w, float[,] iW, float[,] s, float[,] iS)
    {
        gc = FindObjectOfType<GameControl>();
        rona = gameObject.transform.root.gameObject.GetComponent<Jump>();

        neurons = new float[layers, n];
        weights = new float[n * layers, n];
        iWeights = new float[iNum, n];
        inputs = new float[iNum];

        bool bad = false;
        if(FindObjectOfType<Breeder>().getGood() == false)
        {
            bad = true;
          //  Debug.Log("bad");
        }
        bool sup = false;
        if (FindObjectOfType<Breeder>().getSuperior() == true)
        {
            sup = true;
            bad = false;
            //Debug.Log("Super");
        }
            setWeights(init, w, iW, s, iS, sup, bad);
    }

    public float Fitness()
    {
        return fitness;
    }

    public void setWeights(Weight weight)
    {

        float[,] w = weight.getWeights();
        float[,] iW = weight.getInputWeights();

        gc = FindObjectOfType<GameControl>();
        rona = gameObject.transform.root.gameObject.GetComponent<Jump>();

        neurons = new float[layers, n];
        weights = new float[n * layers, n];
        iWeights = new float[iNum, n];
        inputs = new float[iNum];

        for (int i = 0; i < n * layers; i++)
        {
            for (int j = 0; j < n; j++)
            {
                weights[i, j] = w[i, j];
            }
        }
        for (int i = 0; i < iNum; i++)
        {
            for (int j = 0; j < n; j++)
            {
                iWeights[i, j] = iW[i, j];
            }
        }
    }

    public void setWeights(bool init, float[,] w, float[,] iW, float[,] s, float[,] iS, bool sup, bool bad)
    {
        if (init || bad)
        {
            for (int i = 0; i < n * layers; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    weights[i, j] = Random.RandomRange(-ranNum, ranNum+1);
                }
            }

            for (int i = 0; i < iNum; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    iWeights[i, j] = Random.RandomRange(-ranNum, ranNum+1);
                }
            }
        }
        else
        {
            for (int i = 0; i < n * layers; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if((int)Random.Range(0, 100) <= mutationChance)
                    {

                        //weights[i, j] = (w[i, j] + s[i, j] / 2.0f);
                        //Debug.Log("mutate");
                        if ((int)Random.Range(0, 2) == 0)
                        {
                            weights[i, j] = w[i, j] + Random.Range(0, mutationAmount);
                        }
                        else
                        {
                            weights[i, j] = w[i, j] - Random.Range(0, mutationAmount);
                        }
                    }
                    else
                    {
                        if((int)Random.Range(0,2) == 0 || sup)
                        {
                            //Debug.Log("init");
                            weights[i, j] = w[i, j];
                        }
                        else
                        {
                            //Debug.Log("Other");
                            weights[i, j] = s[i, j];
                        }
                        
                    }
                   
                }
            }

            for (int i = 0; i < iNum; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if ((int)Random.Range(0, 100) <= mutationChance)
                    {
                        if ((int)Random.Range(0, 2) == 0)
                        {
                            iWeights[i, j] = iW[i, j] + Random.Range(0, mutationAmount);
                        }
                        else
                        {
                            iWeights[i, j] = iW[i, j] - Random.Range(0, mutationAmount);
                        }
                        // iWeights[i, j] = (iW[i, j] + iS[i, j] / 2.0f);
                    } else
                    {
                        if ((int)Random.Range(0, 2) == 0 || sup)
                        {
                            iWeights[i, j] = iW[i, j];
                        }
                        else
                        {
                            iWeights[i, j] = iS[i, j];
                        }
                    }
                    
                }
            }
        }
       
    }

    public float[,] getWeights()
    {
        return weights;
    }

    public float[,] getInputWeights()
    {
        return iWeights;
    }


    //Calculate value for certain neuron in a layer
    //3 different possibilites
    //layer 1 we need input layer
    //output layer we need to use threshold
    //otherwise normal
    public void calcValue(int layer, int neuron)
    {
        Debug.Assert(layer > 0);

        float sum = 0f;
        if(layer == layers)
        {
            for(int i = 0; i < n; i++)
            {
                sum += neurons[layer-1, i] * weights[((layer - 1) * n) + i, neuron];
            }
            output = threshold(sum);

        } else if(layer == 1)
        {
            for(int i = 0; i < iNum; i++)
            {
                sum += inputs[i] * iWeights[i, neuron];
            }
            neurons[layer, neuron] = sigmoid(sum);
        } else if (layer > 1)
        {
            for (int i = 0; i < n; i++)
            {
                sum += neurons[layer - 1, i] * weights[((layer - 1) * n) + i, neuron];
            }
            neurons[layer, neuron] = sigmoid(sum);
        }

    }

    public int threshold(float val)
    {
        if(val >= 0)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    public float sigmoid(float val)
    {
        return 1 / (1 + Mathf.Exp(-val));
    }

    public void calcAll()
    {
        //calc layers until output
        //start at 1 because we go layers - 1 to get the previous layer
        for (int i = 1; i < layers; i++)
        {
            for(int j = 0; j < n; j++)
            {
                calcValue(i, j);
            }   
        }

        //getting output layer
        for(int i = 0; i < n; i++)
        {
            calcValue(layers, i);
        }
    }

    public float calcDist()
    {
        distOnDeath = Mathf.Abs(gameObject.transform.root.transform.position.y - inputs[2]);
        return distOnDeath;
    }

    // Update is called once per frame
    void Update()
    {

        if (rona.getJumped() && gc.alive)
        {
           // Debug.Log("update");
           // jumped = true;
            inputs[0] = (rona.transform.position.y + 5) ; // y
            inputs[1] = gc.getDistAway(gameObject.transform.root.gameObject.GetComponent<Jump>()); //dist away
            inputs[2] = gc.getMiddle() + 5; //middle of mem

           //Debug.Log(iWeights[iNum-1, n-1]);

            calcAll();
           // Debug.Log(output);
            if (output == 1)
            {
                rona.Spring();
            }
            fitness = Time.timeSinceLevelLoad - rona.timeStarted;
        }

        
        

    }
}
