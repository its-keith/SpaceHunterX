using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoring : MonoBehaviour {

    public Text[] scores;
    static int score;

	// Use this for initialization
	void Start () {
		if (PlayerPrefs.GetInt("Score").Equals(null))
        {
            PlayerPrefs.SetInt("Score", score);
        }
	}
	
	// Update is called once per frame
	void Update () {
        PlayerPrefs.SetInt("Score", score);
        for (int i = 0; i < scores.Length; i++)
        {
            scores[i].text = PlayerPrefs.GetInt("Score").ToString();
        }
    }

    //Add points to the score value
    public static void UpdateScore(int points)
    {
        score += points;
    }

    //Set score to 0
    public static void ResetScore()
    {
        score = 0;
    }
}
