using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxManager : MonoBehaviour
{
    public float power = 50;
    public float maxPower = 100;
    //public float drainPowerCounter;
    //public float drainPowerBetweenTime = 2f;
    public float drainBatteriesCounter;
    public float drainBatteriesBetweenTime = 1f;
    public BatteryController[] batteriesInChildren;
    private CanvasHudController hudController;
    private CanvasGameOver canvasGameOver;

    private void Awake()
    {
        hudController = FindObjectOfType<CanvasHudController>();
        canvasGameOver = FindObjectOfType<CanvasGameOver>();
    }

    // Use this for initialization
    void Start ()
    {
        canvasGameOver.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
    {
        switch (GameManager.instance.gameState)
        {
            case GameManager.GameStates.Playing:

                power -= 5 * Time.deltaTime;

                //drain the batteries
                drainBatteriesCounter += Time.deltaTime;
                if (drainBatteriesCounter >= drainBatteriesBetweenTime)
                {
                    drainBatteriesCounter = 0;
                    DrainBatteries();
                }

                CanvasHudController.newSliderValue = power;

                BatteriesPositions();

                if (power <= 0)
                {
                    Debug.Log("Game Over");
                    canvasGameOver.gameObject.SetActive(true);
                    GameManager.instance.gameState = GameManager.GameStates.GameOver;
                }
                break;
        }
    }

    public void DrainBatteries()
    {
        if (batteriesInChildren != null)
        {
            //drain batteries and extract their power
            if (batteriesInChildren.Length > 0)
            {
                var totalAmountOfPower = 0;
                foreach (BatteryController battery in batteriesInChildren)
                {
                    if (battery.power > 0)
                    {
                        if (power < maxPower)
                        {
                            if (battery.Drain(1))
                            {
                                power += 1;
                                CanvasHudController.accumulatedPower += 1;
                                totalAmountOfPower++;
                            }
                            else
                            {
                                battery.transform.parent = transform.parent;
                                CheckBatteries();
                            }
                        }
                    }
                }
                hudController.SpawnPoints("+", totalAmountOfPower);
            }
            else
            {
                hudController.SpawnPoints("-", 1);
            }
        }
    }

    public void BatteriesPositions()
    {
        if (batteriesInChildren != null)
        {
            //move batteries to a place in the box
            int batLength = batteriesInChildren.Length;
            if (batLength == 1)
            {
                batteriesInChildren[0].transform.position = Vector2.MoveTowards(batteriesInChildren[0].transform.position, new Vector2(0, 0), Time.deltaTime);
            }
            else if (batLength == 2)
            {
                batteriesInChildren[0].transform.position = Vector2.MoveTowards(batteriesInChildren[0].transform.position, new Vector2(-.25f, 0), Time.deltaTime);
                batteriesInChildren[1].transform.position = Vector2.MoveTowards(batteriesInChildren[1].transform.position, new Vector2(.25f, 0), Time.deltaTime);
            }
            else if (batLength == 3)
            {
                batteriesInChildren[0].transform.position = Vector2.MoveTowards(batteriesInChildren[0].transform.position, new Vector2(-.5f, 0), Time.deltaTime);
                batteriesInChildren[1].transform.position = Vector2.MoveTowards(batteriesInChildren[1].transform.position, new Vector2(0, 0), Time.deltaTime);
                batteriesInChildren[2].transform.position = Vector2.MoveTowards(batteriesInChildren[2].transform.position, new Vector2(.5f, 0), Time.deltaTime);
            }
            else if (batLength == 4)
            {
                batteriesInChildren[0].transform.position = Vector2.MoveTowards(batteriesInChildren[0].transform.position, new Vector2(-.75f, 0), Time.deltaTime);
                batteriesInChildren[1].transform.position = Vector2.MoveTowards(batteriesInChildren[1].transform.position, new Vector2(-.25f, 0), Time.deltaTime);
                batteriesInChildren[2].transform.position = Vector2.MoveTowards(batteriesInChildren[2].transform.position, new Vector2(.25f, 0), Time.deltaTime);
                batteriesInChildren[3].transform.position = Vector2.MoveTowards(batteriesInChildren[3].transform.position, new Vector2(.75f, 0), Time.deltaTime);
            }
            else if (batLength == 5)
            {
                batteriesInChildren[0].transform.position = Vector2.MoveTowards(batteriesInChildren[0].transform.position, new Vector2(-1f, 0), Time.deltaTime);
                batteriesInChildren[1].transform.position = Vector2.MoveTowards(batteriesInChildren[1].transform.position, new Vector2(-.5f, 0), Time.deltaTime);
                batteriesInChildren[2].transform.position = Vector2.MoveTowards(batteriesInChildren[2].transform.position, new Vector2(0, 0), Time.deltaTime);
                batteriesInChildren[3].transform.position = Vector2.MoveTowards(batteriesInChildren[3].transform.position, new Vector2(.5f, 0), Time.deltaTime);
                batteriesInChildren[4].transform.position = Vector2.MoveTowards(batteriesInChildren[4].transform.position, new Vector2(1f, 0), Time.deltaTime);
            }
        }
    }

    public void CheckBatteries()
    {
        batteriesInChildren = GetComponentsInChildren<BatteryController>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (GameManager.instance.gameState == GameManager.GameStates.Waiting)
            {
                GameManager.instance.gameState = GameManager.GameStates.Playing;
                Debug.Log("GameState Start");
            }
        }
    }
}
