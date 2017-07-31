using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattery : MonoBehaviour
{
    public GameObject battery = null;

    private void Update()
    {
        if (battery != null)
        {
            battery.transform.position = transform.position;
        }
    }

    private void OnTrigger(Collider2D collision)
    {
        if (collision.CompareTag("Battery"))
        {
            if (battery == null)
            {
                battery = collision.gameObject;
                battery.GetComponent<BatteryController>().wiggle.enabled = false;
                battery.GetComponent<BatteryController>().flyInTarget = Vector2.zero;
                battery.GetComponent<Rigidbody2D>().isKinematic = true;
                battery.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                battery.GetComponent<Collider2D>().isTrigger = true;
                battery.GetComponentsInChildren<SpriteRenderer>()[0].sortingLayerName = "Player";
                battery.GetComponentsInChildren<SpriteRenderer>()[0].sortingOrder = 2;
            }
        }

        if (collision.CompareTag("Box"))
        {
            if (battery != null)
            {
                battery.GetComponentsInChildren<SpriteRenderer>()[0].sortingLayerName = "Objects";
                battery.GetComponent<Collider2D>().enabled = false;
                battery.transform.parent = collision.transform;

                battery = null;

                collision.GetComponent<BoxManager>().CheckBatteries();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnTrigger(collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        OnTrigger(collision);
    }
}
