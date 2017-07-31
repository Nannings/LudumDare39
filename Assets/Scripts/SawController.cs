using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawController : MonoBehaviour
{
    public Vector2 flyInTarget;
    private SpriteRenderer[] spriteRenderers;
    private float timeOnField = 0;
    private Vector3 startPos;
    private Collider2D collider2Da;

    private void Awake()
    {
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        collider2Da = GetComponent<Collider2D>();
    }

    // Use this for initialization
    void Start ()
    {
        float newScale = Random.Range(.5f, 1.5f);
        transform.localScale = new Vector3(newScale, newScale, newScale);

        startPos = transform.position;
    }
	
	// Update is called once per frame
	void Update ()
    {
        timeOnField += Time.deltaTime;

        if (timeOnField > 4)
        {
            collider2Da.enabled = false;
            transform.position = Vector2.MoveTowards(transform.position, startPos, 5 * Time.deltaTime);
            if (transform.position == startPos)
            {
                ObjectSpawner.enemiesOnField--;
                Destroy(gameObject);
            }

            return;
        }

        if (flyInTarget != null && flyInTarget != Vector2.zero)
        {
            transform.position = Vector2.MoveTowards(transform.position, flyInTarget, 5 * Time.deltaTime);
            if (transform.position == new Vector3(flyInTarget.x, flyInTarget.y, transform.position.z))
            {
                //start patrol
            }
        }

        if (GameManager.instance.gameState == GameManager.GameStates.GameOver)
        {
            foreach (SpriteRenderer spriteRender in spriteRenderers)
            {
                Color tmp = spriteRender.color;
                if (tmp.a > 0)
                {
                    spriteRender.sortingOrder--;
                    tmp.a -= Time.deltaTime;
                    spriteRender.color = tmp;
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Battery"))
        {
            collision.gameObject.GetComponent<BatteryController>().power = 0;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Battery"))
        {
            collision.gameObject.GetComponent<BatteryController>().power = 0;
        }
    }
}
