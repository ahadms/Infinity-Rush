using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text scoreText;
    public DeathMenu deathmenu;
    private float score = 0.0f;

    private int difficultyLevel = 1;
    private int MaxDifficultyLevel = 10;
    private int ScoreToNextLevel = 10;
    private bool isDead = false;


    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            return;
        }

        if (score >= ScoreToNextLevel)
            LevelUp();

        score += Time.deltaTime;
        scoreText.text = ((int)score).ToString();
    }

    void LevelUp()
    {
        if(difficultyLevel == MaxDifficultyLevel)
        {
            return;
        }
        ScoreToNextLevel *= 2;
        difficultyLevel++;
        GetComponent<PlayerMotor>().SetSpeed(difficultyLevel);
    }

    public void OnDeath()
    {
        isDead = true;
        deathmenu.ToggleEndMenu(score);
       
    }
}
