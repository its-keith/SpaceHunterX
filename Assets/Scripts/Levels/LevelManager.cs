using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

    public AudioClip level;
    public AudioClip levelCleared;
    public AudioClip gameOver;
    public Text notification;
    public GameObject[] player;
    public Sprite[] panels;
    public string levelName;

    public bool startSpawning;

    private bool movePlayers;

    private int counter;

    bool endOfLevel;

    private void Start()
    {
        if (PlayerPrefs.GetInt("Players") == 1)
        {
            player = player.Where(w => w != player[1]).ToArray();
        }

        //Spawn the players below the screen
        if (player.Length == 1)
        {
            player[0].GetComponent<PlayerMovement>().enabled = false;
            player[0].GetComponent<PlayerShooting>().enabled = false;
            Vector3 startPos = new Vector3(0, -1200, -1);
            player[0].transform.position = startPos;
            Instantiate(player[0]);
            player[0] = GameObject.Find("Player1(Clone)");
        }
        else
        {
            for (int i = 0; i < 2; i++)
            {
                player[i].GetComponent<PlayerMovement>().enabled = false;
                player[i].GetComponent<PlayerShooting>().enabled = false;
            }
            Vector3 startPos = new Vector3(-100, -1200, -1);
            player[0].transform.position = startPos;
            Instantiate(player[0]);
            player[0] = GameObject.Find("Player1(Clone)");
            startPos = new Vector3(100, -1200, -1);
            player[1].transform.position = startPos;
            Instantiate(player[1]);
            player[1] = GameObject.Find("Player2(Clone)");
        }

        //Move the players onto the screen
        movePlayers = true;
    }

    private void Update()
    {
        MovePanels();
        CheckForEndGame();
        if (movePlayers)
        {
            for (int i = 0; i < player.Length; i++)
            {
                //Move them up onto the screen
                if (player[i] != null)
                {
                    player[i].transform.position = new Vector3(player[i].transform.position.x, player[i].transform.position.y + 10, -1);
                }
            }
            if (player[0].transform.position.y >= -700)
            {
                //Stop moving once they are on screen
                movePlayers = false;

                //Level Notification
                Notification(levelName, level);

                //Let the players move/shoot
                for (int i = 0; i < player.Length; i++)
                {
                    player[i].GetComponent<PlayerMovement>().enabled = true;
                    player[i].GetComponent<PlayerShooting>().enabled = true;
                }

                //Start spawning enemies
                startSpawning = true;
            }
        }
        if (endOfLevel)
        {
            MovePlayersUp();
        }
    }

    protected void Notification(string text, AudioClip audio)
    {
        Text txt = Instantiate(notification);
        txt.transform.SetParent(GameObject.Find("Canvas").transform, true);
        txt.text = text;
        gameObject.GetComponent<AudioSource>().PlayOneShot(audio);
    }

    void MovePanels()
    {
        Image leftPanel = GameObject.FindGameObjectWithTag("LeftPanel").GetComponent<Image>();
        Image rightPanel = GameObject.FindGameObjectWithTag("RightPanel").GetComponent<Image>();

        if (!endOfLevel)
        {
            //Move panels onto the screen
            if (counter < 33)
            {
                leftPanel.transform.Translate(25f, 0, 0);
                rightPanel.transform.Translate(-25f, 0, 0);
                counter++;
            }

            if (!GameObject.FindGameObjectWithTag("Player1"))
            {
                leftPanel.sprite = panels[0];
                GameObject.Find("Canvas/LeftPanel/Player1_PUs").SetActive(false);
                GameObject.Find("Canvas/LeftPanel/ShieldLevel").SetActive(false);
                GameObject.Find("Canvas/LeftPanel/Score1").SetActive(false);
            }
            if (!GameObject.FindGameObjectWithTag("Player2"))
            {
                rightPanel.sprite = panels[1];
                GameObject.Find("Canvas/RightPanel/Player2_PUs").SetActive(false);
                GameObject.Find("Canvas/RightPanel/ShieldLevel").SetActive(false);
                GameObject.Find("Canvas/RightPanel/Score2").SetActive(false);
            }
        }
        else
        {
            if (counter > 0)
            {
                leftPanel.transform.Translate(-25f, 0, 0);
                rightPanel.transform.Translate(25f, 0, 0);
                counter--;
            }
        }
    }

    void MovePlayersUp()
    {
        for (int i = 0; i < player.Length; i++)
        {
            if (player[i] != null)
            {
                //Move them up onto the screen
                player[i].transform.position = new Vector3(player[i].transform.position.x, player[i].transform.position.y + 50, -1);
            }
        }
    }
    IEnumerator NextLevel(string level, string nextLevel)
    {
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < player.Length; i++)
        {
            if (player[i] != null)
            {
                player[i].GetComponent<PlayerMovement>().enabled = false;
                player[i].GetComponent<PlayerShooting>().enabled = false;
            }
        }
        yield return new WaitForSeconds(1f);

        Notification(level, levelCleared);

        if (nextLevel == "Win")
        {
            StartCoroutine(GameOver());
        } else
        {
            yield return new WaitForSeconds(1.5f);
            endOfLevel = true;
            yield return new WaitForSeconds(1.5f);
            SceneManager.LoadScene(nextLevel);
        }
        
    }

    public void LoadNextLevel(string level, string nextLevel)
    {
        StartCoroutine(NextLevel(level, nextLevel));
    }
    
    void CheckForEndGame()
    {
        //If neither player is in the hierarchy
        if (GameObject.Find("Player1(Clone)") == null && GameObject.Find("Player2(Clone)") == null)
        {
            //End the game
            if (!movePlayers)
            {
                StartCoroutine(GameOver());

                movePlayers = true;
            }
        }
    }

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(1.5f);
        Text txt = Instantiate(notification);
        txt.transform.SetParent(GameObject.Find("Canvas").transform, true);
        txt.text = "Game Over!";
        gameObject.GetComponent<AudioSource>().PlayOneShot(gameOver);
        yield return new WaitForSeconds(1.5f);

        bool highScore = false;
        //Check to see if highscore was beaten
        for (int i = 0; i < Leaderboard.leaderboardSize; i++)
        {
            if (PlayerPrefs.GetInt("Score") > PlayerPrefs.GetInt("score" + i))
            {
                PlayerPrefs.SetInt("score" + i, 0);
                SceneManager.LoadScene("AddName");
                highScore = true;
                Scoring.ResetScore();
                break;
            }
        }

        if (!highScore)
        {
            Scoring.ResetScore();
            SceneManager.LoadScene("MainMenu");
        }

    }
}
