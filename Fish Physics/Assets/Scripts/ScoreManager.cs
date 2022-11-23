using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TMP_Text textscore;
    public TMP_Text gameOverText;
    public float score;

    public void gameOver()
    {
        gameOverText.text = "Game Over!";
    }

    // Start is called before the first frame update
    void Start()
    {
        score = 0f;
        textscore.text = "Fish Food: " + score.ToString();
    }
    // Update is called once per frame
    void Update()
    {
        textscore.text = "Fish Food: " + score.ToString();
    }

    
}