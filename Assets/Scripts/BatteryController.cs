using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryController : MonoBehaviour
{
    //public GameObject thePlayer;
    public float power = 100;
    private float maxPower;
    public Animator animator;
    public SpriteRenderer[] spriteRenderers;
    public Vector2 flyInTarget;
    public Wiggle wiggle;

    private void Awake()
    {
        //thePlayer = FindObjectOfType<PlayerController>().gameObject;
        animator = GetComponentInChildren<Animator>();
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        wiggle = GetComponent<Wiggle>();
    }

    // Use this for initialization
    void Start ()
    {
        wiggle.enabled = false;

        float newScale = Random.Range(.7f, 1.3f);
        transform.localScale = new Vector3(newScale, newScale, newScale);

        power = (power * newScale);

        maxPower = power;
    }
	
	// Update is called once per frame
	void Update ()
    {

        if(flyInTarget != null && flyInTarget != Vector2.zero)
        {
            transform.position = Vector2.MoveTowards(transform.position, flyInTarget, 5 * Time.deltaTime);
            if (transform.position == new Vector3( flyInTarget.x, flyInTarget.y, transform.position.z))
            {
                wiggle.enabled = true;
            }
        }

        if (power <= 1 || GameManager.instance.gameState == GameManager.GameStates.GameOver)
        {
            //disappear
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
                    ObjectSpawner.batteriesOnField--;
                    Destroy(gameObject);
                }
            }
        }
    }

    public bool Drain(int powerDecrease)
    {
        if (power > 1)
        {
            power -= powerDecrease;

            float percentPower = (power * 100) / maxPower;
            float timePower = 100 - percentPower;
            animator.Play("battery_drain", 0, timePower / 100);
            return true;
        }
        else
        {
            animator.Play("battery_drain", 0, .99f);
            return false;
        }
    }
}
