using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserObject : MonoBehaviour {

    private float y;
    private float pos;
    public GameObject burst;

    bool delay;

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
		
        if(y < 20)
        {
            y += .75f;
            transform.localScale = new Vector3(1, y, 1);
        }
        
        if(transform.parent.GetComponent<Laser>().cooldown <= 0)
        {
            pos += 50f;
            transform.position = new Vector3(transform.position.x, pos, transform.position.z);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        GameObjectUtil.Instantiate(burst, collision.transform.position, null);
    }
}
