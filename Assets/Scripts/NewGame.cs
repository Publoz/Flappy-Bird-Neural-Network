using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGame : MonoBehaviour
{
    public void OnClick()
    {
        FindObjectOfType<GameControl>().nextLevel();
    }
}
