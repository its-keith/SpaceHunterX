using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;

    private float vertSpeed;
    private float horizSpeed;

    private Rigidbody2D rb;

    public KeyCode up;
    public KeyCode down;
    public KeyCode left;
    public KeyCode right;

    public string axisVertical;
    public string axisHorizontal;

    Vector3 movementPlayer;
    Vector2 screenPosition;

    public GameObject smoke;
    float smokeCooldown;
    Vector3 smokePos;

    //public Vector2 screenSize;

    void Start()
    {
       //screenSize = new Vector2(Camera.main.aspect * Camera.main.orthographicSize, Camera.main.orthographicSize);
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        //float hAxis = Input.GetAxis("Horizontal");
        //float vAxis = Input.GetAxis("Vertical");
        

        if (Input.GetKey(up) || Input.GetAxis(axisVertical) < 0)
        {
            vertSpeed = speed;
        } else if (Input.GetKey(down) || Input.GetAxis(axisVertical) > 0)
        {
            vertSpeed = -speed;
        } else if (!Input.GetKey(up) && !Input.GetKey(down))
        {
            vertSpeed = 0;
        }

        if (Input.GetKey(right) || Input.GetAxis(axisHorizontal) > 0)
        {
            horizSpeed = speed;
        } else if (Input.GetKey(left) || Input.GetAxis(axisHorizontal) < 0)
        {
            horizSpeed = -speed;
        } else if (!Input.GetKey(right) && !Input.GetKey(left))
        {
            horizSpeed = 0;
        }

        movementPlayer = new Vector3(horizSpeed, vertSpeed, 0) * Time.deltaTime;
        rb.MovePosition(transform.position + movementPlayer);
        //rb.AddForce(movementPlayer);
    }
    void Update() {
        // X axis

        if (transform.position.x <= -885f)
        {
            transform.position = new Vector2(-885f, transform.position.y);
        }
        else if (transform.position.x >= 885f)
        {
            transform.position = new Vector2(885f, transform.position.y);
        }

        // Y axis
        if (transform.position.y <= -915f)
        {
            transform.position = new Vector2(transform.position.x, -915f);
        }
        else if (transform.position.y >= 915f)
        {
            transform.position = new Vector2(transform.position.x, 915f);
        }

        //Spawn smoke exhaust on the player if they are moving
        smokeCooldown -= Time.deltaTime;
        if (smokeCooldown <= 0)
        {
            smokeCooldown = .1f;
            smokePos = new Vector3(transform.position.x, transform.position.y - 30, transform.position.z);
            GameObjectUtil.Instantiate(smoke, smokePos, null);
        }
    }
}
