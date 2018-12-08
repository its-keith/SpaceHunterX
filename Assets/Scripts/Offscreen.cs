using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Offscreen : MonoBehaviour {

    //Destroy on object when it goes offscreen

    //How far offscreen an object will be deleted
    public float offset = 100f;

    private void Update()
    {
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        if (screenPosition.y > Screen.height + offset || screenPosition.y < 0 - offset)
        {
            GameObjectUtil.Destroy(gameObject);
        } else if (screenPosition.x > Screen.width + offset || screenPosition.x < 0 - offset)
        {
            GameObjectUtil.Destroy(gameObject);
        }
    }

}

