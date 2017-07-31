using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public BoxManager boxManager;
    public CameraShake cameraShake;

    private void Awake()
    {
        boxManager = FindObjectOfType<BoxManager>();
        cameraShake = FindObjectOfType<CameraShake>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy1"))
        {
            ObjectSpawner.enemiesOnField--;
            cameraShake.ShakeCamera(.5f, .025f);
            boxManager.power -= 10;
            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy1"))
        {
            ObjectSpawner.enemiesOnField--;
            cameraShake.ShakeCamera(.5f, .025f);
            boxManager.power -= 10;
            Destroy(collision.gameObject);
        }
    }
}
