using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private static Score instance = null;
    private static int score = 0;
    public Text scoreText;
    private Score() { }

    private void Start()
    {
        instance = this;
        scoreText.text = "Score: " + score;
    }

    public static Score Instance {
        get
        {
            if (instance == null)
            {
                instance = new Score();
            }

            return instance;
        }
    }

    public void addScore(int amount)
    {
        score += amount;
        scoreText.text = "Score: " + score;
    }
}
