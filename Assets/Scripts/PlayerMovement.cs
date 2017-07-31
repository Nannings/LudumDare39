using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float speed;
    private float maxSpeed = 10;
    private float accelaration = 8;
    private float deacceleration = 10f;
    public Vector2 direction;
    private Rigidbody2D myRigidbody2D;

    void Awake()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Use this for initialization
    void Start()
    {
    }

    void FixedUpdate()
    {
        Vector2 dir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (dir.x != 0 || dir.y != 0)
        {
            direction = dir;
            speed = Mathf.Lerp(speed, maxSpeed, accelaration * Time.deltaTime);
        }
        else
        {
            speed = Mathf.Lerp(speed, 0, deacceleration * Time.deltaTime);
        }
        myRigidbody2D.velocity = direction * speed;
    }
}
