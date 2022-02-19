using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
//using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class LensDistort : MonoBehaviour
{

    private float intensity;
    private LensDistortion ld;
    private Volume v;
    private bool up = false;
    private float rate = 0.02f;

    //https://answers.unity.com/questions/1355103/modifying-the-new-post-processing-stack-through-co.html
    void Start()
    {
        v = GetComponent<Volume>();
        v.sharedProfile.TryGet<LensDistortion>(out ld);
       //ld = gameObject.GetComponent<LensDistortion>();
        intensity = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!up)
        {
            if (intensity > 0f)
            {
                intensity -= rate * 2;
            }
            else
            {
                intensity -= rate;
            }
            if (intensity <= -0.9)
            {
                up = true;
            }
        }
        else
        {
            if(intensity < 0f)
            {
                intensity += rate * 2;
            }
            else
            {
                intensity += rate;
            }
            
            if(intensity >= 0.8)
            {
                up = false;
            }
        }
        ld.intensity.Override(intensity);
    }
}
