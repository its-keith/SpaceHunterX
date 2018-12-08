using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AddName : MonoBehaviour {
    
    bool twoPlayer;

    string[] alphabet = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

    //Index of the alphabet for each player
    int index1 = 0;
    int index2 = 0;

    float delay1;
    float delay2;

    string name1;
    string name2;

    float nameTimer = 20f;
    int nameLength = 3;

	// Use this for initialization
	void Start () {
		
        //Set to false if it was a single player game
        if (PlayerPrefs.GetInt("Players") == 2) {
            twoPlayer = true;
        } else
        {
            GameObject.Find("Canvas/Player Two").SetActive(false);
            GameObject.Find("Canvas/P2_Letter").SetActive(false);
            GameObject.Find("Canvas/P2_Name").SetActive(false);
            twoPlayer = false;
        }
	}
	
	// Update is called once per frame
	void Update () {
        PlayerOne();
        if (twoPlayer)
        {
            PlayerTwo();
        }
        

        nameTimer -= Time.deltaTime;
        GameObject.Find("Timer").GetComponent<Text>().text = Mathf.Ceil(nameTimer).ToString();


        if (nameTimer <= 0)
        {
            UpdateLeaderboard();
        }

	}

    void PlayerOne()
    {
        //Update the letter chosen when moving the joystick
        if (Input.GetAxis("Joystick1Vertical") < 0 && delay1 <= 0)
        {
            if (index1 == 25)
            {
                index1 = 0;
            }
            else
            {
                index1++;
            }
            delay1 = .3f;
        }
        else if (Input.GetAxis("Joystick1Vertical") > 0 && delay1 <= 0)
        {
            if (index1 == 0)
            {
                index1 = 25;
            }
            else
            {
                index1--;
            }
            delay1 = .3f;
        }
        name1 = GameObject.Find("P1_Name").GetComponent<Text>().text;
        //Submit the letter when button is pressed
        if (Input.GetButton("P1_Button0") && delay1 <= 0 && name1.Length <= nameLength)
        {
            name1 += alphabet[index1];
            GameObject.Find("P1_Name").GetComponent<Text>().text = name1;
            delay1 = .3f;
        }

        GameObject.Find("P1_Letter").GetComponent<Text>().text = alphabet[index1];

        if (delay1 > 0)
        {
            delay1 -= Time.deltaTime;
        }
    }

    void PlayerTwo()
    {
        //Update the letter chosen when moving the joystick
        if (Input.GetAxis("Joystick2Vertical") < 0 && delay2 <= 0)
        {
            if (index2 == 25)
            {
                index2 = 0;
            }
            else
            {
                index2++;
            }
            delay2 = .3f;
        }
        else if (Input.GetAxis("Joystick2Vertical") > 0 && delay2 <= 0)
        {
            if (index2 == 0)
            {
                index2 = 25;
            }
            else
            {
                index2--;
            }
            delay2 = .3f;
        }
        name2 = GameObject.Find("P2_Name").GetComponent<Text>().text;
        //Submit the letter when button is pressed
        if (Input.GetButton("P2_Button0") && delay2 <= 0 && name2.Length <= nameLength)
        {
            name2 += alphabet[index2];
            GameObject.Find("P2_Name").GetComponent<Text>().text = name2;
            delay2 = .3f;
        }

        GameObject.Find("P2_Letter").GetComponent<Text>().text = alphabet[index2];

        if (delay2 > 0)
        {
            delay2 -= Time.deltaTime;
        }
    }

    void UpdateLeaderboard()
    {
        if (name1 == "")
        {
            name1 = "Player1";
        }
        if (name2 == "")
        {
            name2 = "Player2";
        }
        
        for (int i = 0; i < Leaderboard.leaderboardSize; i++)
        {
            if (PlayerPrefs.GetInt("score" + i) == 0)
            {
                PlayerPrefs.SetInt("score" + i, PlayerPrefs.GetInt("Score"));
                PlayerPrefs.SetString("playerOne" + i, name1);
                PlayerPrefs.SetString("playerTwo" + i, name2);
                break;
            }
        }
        PlayerPrefs.SetInt("Score", 0);
        SceneManager.LoadScene("Leaderboard");
    }
}
