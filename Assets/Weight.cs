using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;


[System.Serializable]
public class Weight : IComparable
{
    private float[,] weights;
    private float[,] iWeights;
    private float fitness;
    
    public Weight(float[,] w, float[,] iw, float f)
    {
        weights = w;
        iWeights = iw;
        fitness = f;
    }

    public int CompareTo(object obj)
    {
        if (obj == null) return 1;

        Weight otherTemperature = obj as Weight;
        if (otherTemperature != null)
        {
            if(otherTemperature.getFitness() < fitness)
            {
                return -1;
            }
            else if(otherTemperature.getFitness() > fitness)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
            
        else
            throw new ArgumentException("Object is not a Temperature");
    }

        public float[,] getWeights()
    {
        return weights;
    }

    public float getFitness()
    {
        return fitness;
    }

    public float[,] getInputWeights()
    {
        return iWeights;
    }
}
