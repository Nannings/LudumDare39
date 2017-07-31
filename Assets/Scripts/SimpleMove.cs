using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMove : MonoBehaviour
{
    public float moveSpeed;
    public Vector3 moveDirection;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (moveSpeed != 0)
        {
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        }
	}
}
