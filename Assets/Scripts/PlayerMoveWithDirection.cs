using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveWithDirection : MonoBehaviour
{
    public PlayerMovement playerMovement;

    private void Awake()
    {
        playerMovement = transform.parent.GetComponent<PlayerMovement>();
    }

    // Use this for initialization
    void Start ()
    {
		
	}
	
	void FixedUpdate ()
    {
        if (playerMovement.direction != Vector2.zero)
        {
            float angle = Mathf.Atan2(playerMovement.direction.y, playerMovement.direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}
