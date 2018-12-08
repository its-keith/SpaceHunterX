using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SelectOnInput : MonoBehaviour {

    public EventSystem eventSystem;
    public GameObject[] selectedObject;
    int objectIndex = 0;
    float delayCooldown;

    public GameObject[] panels;

    private bool buttonSelected;
    
	// Use this for initialization
	void Start ()
    {
        eventSystem.SetSelectedGameObject(selectedObject[0]);
    }
	
	// Update is called once per frame
	void Update ()
    {
        //Get the index of the currently selected game object
        //for (int i = 0; i < selectedObject.Length; i++)
        //{
        //    if (selectedObject[i] == eventSystem.currentSelectedGameObject)
        //    {
        //        objectIndex = i;
        //    }
        //}

		if (Input.GetAxis("Joystick1Vertical") !=0 && delayCooldown <= 0
            && eventSystem.currentSelectedGameObject != selectedObject[6]
            && eventSystem.currentSelectedGameObject != selectedObject[7])
        {
            if (Input.GetAxis("Joystick1Vertical") < 0)
            {
                if (objectIndex == 0)
                {
                    objectIndex = 5;
                } else
                {
                    objectIndex -= 1;
                    
                }
            } else if (Input.GetAxis("Joystick1Vertical") > 0)
            {
                if (objectIndex == 5)
                {
                    objectIndex = 0;
                }
                else
                {
                    objectIndex += 1;
                }
            }
            eventSystem.SetSelectedGameObject(selectedObject[objectIndex]);
            delayCooldown = .3f;
        }

        if ((Input.GetButton("P1_Button0") || Input.GetButton("P2_Button0")) && delayCooldown <= 0)
        {
            if (eventSystem.currentSelectedGameObject == selectedObject[0])
            {
                //Single Player
                PlayerPrefs.SetInt("Players", 1);
                SceneManager.LoadScene("Level1");
            }
            else if (eventSystem.currentSelectedGameObject == selectedObject[1])
            {
                //Two Player
                PlayerPrefs.SetInt("Players", 2);
                SceneManager.LoadScene("Level1");
            }
            else if (eventSystem.currentSelectedGameObject == selectedObject[2])
            {
                //Leaderboard
                SceneManager.LoadScene("Leaderboard");
            }
            //else if (eventSystem.currentSelectedGameObject==selectedObject[3])
            //{
            //    //Settings
            //    panels[0].SetActive(false);
            //    panels[1].SetActive(true);
            //    eventSystem.SetSelectedGameObject(selectedObject[9]);
            //}
            //else if (eventSystem.currentSelectedGameObject == selectedObject[9])
            //{
            //    //Settings Back
            //    panels[0].SetActive(true);
            //    panels[1].SetActive(false);
            //    eventSystem.SetSelectedGameObject(selectedObject[3]);
            //}
            else if (eventSystem.currentSelectedGameObject == selectedObject[3])
            {
                //About
                panels[0].SetActive(false);
                panels[2].SetActive(true);
                eventSystem.SetSelectedGameObject(selectedObject[7]);
            }
            else if (eventSystem.currentSelectedGameObject == selectedObject[7])
            {
                //About Back
                panels[0].SetActive(true);
                panels[2].SetActive(false);
                eventSystem.SetSelectedGameObject(selectedObject[3]);
            }
            else if (eventSystem.currentSelectedGameObject == selectedObject[4])
            {
                //Credits
                panels[0].SetActive(false);
                panels[1].SetActive(true);
                eventSystem.SetSelectedGameObject(selectedObject[6]);
                
            }
            else if (eventSystem.currentSelectedGameObject == selectedObject[6])
            {
                //Credits back
                panels[0].SetActive(true);
                panels[1].SetActive(false);
                eventSystem.SetSelectedGameObject(selectedObject[5]);

            }
            else if (eventSystem.currentSelectedGameObject == selectedObject[5])
            {
                //Quit
                Application.Quit();
            }
          
            delayCooldown = .3f;
        }

        if (delayCooldown > 0)
        {
            delayCooldown -= Time.deltaTime;
        }
        
	}

    private void OnDisable()
    {
        buttonSelected = false;
    }
}
