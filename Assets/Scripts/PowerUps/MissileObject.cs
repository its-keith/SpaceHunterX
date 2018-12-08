using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileObject : MonoBehaviour {

    public GameObject target;
    private Rigidbody2D rb;

    public float speed = 1000f;
    public float rotateSpeed = 500f;

    public GameObject explosion;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (target != null)
        {
            Vector2 direction = (Vector2)target.transform.position - rb.position;

            direction.Normalize();

            float rotateAmount = Vector3.Cross(direction, transform.up).z;

            rb.angularVelocity = -rotateAmount * rotateSpeed;

            rb.velocity = transform.up * speed;
        } else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == target.name)
        {
            speed = 0f;
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

}
