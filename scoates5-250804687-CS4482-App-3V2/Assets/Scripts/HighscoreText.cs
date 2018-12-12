using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreText : MonoBehaviour
{

    Text Score;

    void OnEnable()
    {
        Score = GetComponent<Text>();
        Score.text = "High Score: " + PlayerPrefs.GetInt("HighScore").ToString();
    }
}