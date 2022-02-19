using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wink : MonoBehaviour
{

    private Animator a;
    private bool looped = false;

    void Start()
    {
        a = gameObject.GetComponent<Animator>();
    }
    
    void Update()
    {
      

        if (a.GetCurrentAnimatorStateInfo(0).normalizedTime > 2 & !a.IsInTransition(0))//(a.GetCurrentAnimatorStateInfo(0).IsName("wink"))
        {
            //Debug.Log("Looped");
            if(looped == false)
            {
                looped = true;
            }
            else
            {
                //Debug.Log("destroy");
                Destroy(gameObject);
            }
            
        }
    }
}
