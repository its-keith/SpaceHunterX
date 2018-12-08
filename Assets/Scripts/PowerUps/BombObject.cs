using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The BombObject is a circle with a CircleCollider2D that expands outward from where it spawned
/// </summary>
public class BombObject : MonoBehaviour {
    
    //How fast the circle expands
    float increaseBy = .25f;
    
    // Update is called once per frame
    void Update () {
        if (transform.localScale.x < 10)
        {
            //Make the circle bigger
            gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x + increaseBy,
            gameObject.transform.localScale.y + increaseBy,
            gameObject.transform.localScale.z);
        } else
        {
            //Once the circle is big enough, destroy it
            GameObjectUtil.Destroy(gameObject);
        }
        
	}
}
