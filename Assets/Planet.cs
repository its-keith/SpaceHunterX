using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour {
    
	// Update is called once per frame
	void Update () {
		if (transform.position.y > 500)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - .2f, transform.position.z);
        }
	}
}
