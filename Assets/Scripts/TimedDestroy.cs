using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedDestroy : MonoBehaviour {

    /// <summary>
    /// Destroy a gameObject after a specified amount of time
    /// </summary>

    public float time;

	// Use this for initialization
	void Start () {
        Destroy(gameObject, time);
	}
}
