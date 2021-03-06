﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotifyPowerUp : MonoBehaviour {

    public Animator animator;
    private Text text;
   
	// Use this for initialization
	void Start () {
        AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
        Destroy(gameObject, clipInfo[0].clip.length);

        text = GetComponent<Text>();
	}

    public void SetText(string newText)
    {
        text.text = newText;
    }
}
