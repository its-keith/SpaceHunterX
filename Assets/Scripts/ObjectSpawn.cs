using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class ObjectSpawn : MonoBehaviour {

    public GameObject [] prefabs;
    public float delay = 2.0f;
    public bool active = true;
    public Vector2 delayRange = new Vector2(1, 2);

    void Start () {
        ResetDelay();
        StartCoroutine(EnemySpawn());
    }

    IEnumerator EnemySpawn()
    {
        yield return new WaitForSeconds(delay);

        if (active)
        {
            var newTransform = transform;
            //Instantiate(prefabs[Random.Range(0, prefabs.Length)], newTransform.position, Quaternion.identity);
            GameObjectUtil.Instantiate(prefabs[Random.Range(0, prefabs.Length)], newTransform.position, null);
        }
        StartCoroutine(EnemySpawn());
    }

    void ResetDelay()
    {
        delay = Random.Range(delayRange.x, delayRange.y);
    }
    
}
