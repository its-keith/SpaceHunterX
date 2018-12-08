using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Score
{
    public int score { get; set; }
    public string playerOne { get; set; }
    public string playerTwo { get; set; }

    public Score(int score, string playerOne, string playerTwo)
    {
        this.score = score;
        this.playerOne = playerOne;
        this.playerTwo = playerTwo;
    }
}

public class Leaderboard : MonoBehaviour {

    public static int leaderboardSize = 6;

    public Score[] score = new Score[leaderboardSize];
    
	// Use this for initialization
	void Start () {
        InitBoard();
        DisplayLeaderboard(score);
    }
	
	// Update is called once per frame
	void Update () {
        
        if (Input.GetButton("P1_Button0") || Input.GetButton("P2_Button0")) {
            SceneManager.LoadScene("MainMenu");
        }

	}

    void DisplayLeaderboard(Score[] scores)
    {
        List<GameObject> newScores = new List<GameObject>();

        for (int i = 0; i < leaderboardSize; i++)
        {
            newScores.Add(GameObject.Find("Canvas/Scores/" + i));
        }
        
        //Update them so the text corresponds to the score
        for (int i = 0; i < leaderboardSize; i++)
        {
            newScores[i].GetComponent<Text>().text = PlayerPrefs.GetInt("score" + i).ToString();
            newScores[i].transform.GetChild(0).GetComponent<Text>().text = PlayerPrefs.GetString("playerOne"+i);
            newScores[i].transform.GetChild(1).GetComponent<Text>().text = PlayerPrefs.GetString("playerTwo" + i);
        }
    }

    public void ResetLeaderboard(Score[] scores)
    {
        for (int i = 0; i < score.Length; i++)
        {
            PlayerPrefs.SetInt("score" + i, scores[i].score);
            PlayerPrefs.SetString("playerOne" + i, scores[i].playerOne);
            PlayerPrefs.SetString("playerTwo" + i, scores[i].playerTwo);
        }
    }

    public void InitBoard()
    {
        if (PlayerPrefs.GetInt("SetBoard") == 0)
        {
            //Set the default leaderboard scores
            score[0] = new Score(10000, "Keith", "Arnold");
            score[1] = new Score(9000, "Jimmy", "");
            score[2] = new Score(7500, "Apple", "Kenny");
            score[3] = new Score(6000, "Dhvani", "");
            score[4] = new Score(5000, "Binila", "");
            score[5] = new Score(5, "Joe", "");
            ResetLeaderboard(score);
            PlayerPrefs.SetInt("SetBoard", 1);
        }
    }
}