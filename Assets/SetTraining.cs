using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetTraining : MonoBehaviour
{
    public void Train()
    {
        PlayerPrefs.SetInt("training", 1);
        PlayerPrefs.SetInt("ai", 0);
        PlayerPrefs.Save();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
