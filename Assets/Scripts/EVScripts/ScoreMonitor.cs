using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreMonitor : MonoBehaviour
{
    public IntVariable marioScore;
    public Text text;
    public void UpdateScore()
    {
        Debug.Log("Updating score");
        Debug.Log(marioScore.Value.ToString());
        text.text = "Score: " + marioScore.Value.ToString();
    }

    public void Start()
    {
        UpdateScore();
    }
}
