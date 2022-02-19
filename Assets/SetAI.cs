using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SetAI : MonoBehaviour
{
    public void goAI()
    {
        PlayerPrefs.SetInt("training", 0);
        PlayerPrefs.SetInt("ai", 1);
        PlayerPrefs.Save();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
