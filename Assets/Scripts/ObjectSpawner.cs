using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject[] batteries;
    public GameObject[] enemies;
    public float spawnCounter;
    public float spawnBetweenTime  = 4f;
    public static int batteriesOnField = 0;
    private int rounds;
    private int roundFive;

    public float spawnEnemyCounter;
    public float spawnEnemyBetweenTime = 5f;
    public static int enemiesOnField = 0;
    public int maxEnemiesOnField = 2;

    Vector3 upPositions;
    Vector3 upPositionTarget;
    Vector3 downPositions;
    Vector3 downPositionTarget;
    Vector3 leftPositions;
    Vector3 leftPositionTarget;
    Vector3 rightPositions;
    Vector3 rightPositionTarget;

    private void Awake()
    {
    }

    // Use this for initialization
    void Start ()
    {
        batteriesOnField = 0;
        enemiesOnField = 0;

        spawnCounter = spawnBetweenTime;

        upPositions = new Vector3(Random.Range(-8, 8), Random.Range(6, 8), transform.position.z);
        upPositionTarget = new Vector3(Random.Range(-7, 7), Random.Range(2, 3.5f), transform.position.z);
        downPositions = new Vector3(Random.Range(-8, 8), Random.Range(-6, -8), transform.position.z);
        downPositionTarget = new Vector3(Random.Range(-7, 7), Random.Range(-1.5f, -4.5f), transform.position.z);
        leftPositions = new Vector3(Random.Range(-10, -14), Random.Range(-4.5f, 4.5f), transform.position.z);
        leftPositionTarget = new Vector3(Random.Range(-7, -2), Random.Range(-4.5f, 4.5f), transform.position.z);
        rightPositions = new Vector3(Random.Range(10, 14), Random.Range(-4.5f, 4.5f), transform.position.z);
        rightPositionTarget = new Vector3(Random.Range(2, 8), Random.Range(-4.5f, 3.5f), transform.position.z);

    }
	
	// Update is called once per frame
	void Update ()
    {
        switch (GameManager.instance.gameState)
        {
            case GameManager.GameStates.Playing:
                
                //battery
                spawnCounter += Time.deltaTime;
                if (spawnCounter >= spawnBetweenTime && batteriesOnField < 6)
                {
                    spawnCounter = 0;
                    rounds++;
                    roundFive++;
                    if (roundFive >= 5)
                    {
                        roundFive = 0;
                        maxEnemiesOnField++;//more enemies 
                        if (enemiesOnField < maxEnemiesOnField)
                        {
                            PreSpawnEnemy();
                        }

                        PreSpawnBattery();
                    }
                    PreSpawnBattery();
                }

                //enemy
                spawnEnemyCounter += Time.deltaTime;
                if (spawnEnemyCounter >= spawnEnemyBetweenTime && enemiesOnField < maxEnemiesOnField)
                {
                    spawnEnemyCounter = 0;
                    PreSpawnEnemy();
                    var rand = Random.Range(0, 2);
                    if (rand == 0) PreSpawnEnemy();

                }

                break;
            case GameManager.GameStates.Waiting:
                spawnCounter += Time.deltaTime;
                if (spawnCounter >= spawnBetweenTime && batteriesOnField < 1)
                {
                    spawnCounter = 0;
                    PreSpawnBattery();
                }
                break;
        }
    }

    void PreSpawnBattery()
    {
        var switchRand = Random.Range(0, 4);
        switch (switchRand)
        {
            case 0:
                SpawnBattery(upPositions, upPositionTarget);
                break;
            case 1:
                SpawnBattery(downPositions, downPositionTarget);
                break;
            case 2:
                SpawnBattery(leftPositions, leftPositionTarget);
                break;
            case 3:
                SpawnBattery(rightPositions, rightPositionTarget);
                break;
        }
    }

    void SpawnBattery(Vector3 pos, Vector3 tar)
    {
        batteriesOnField++;
        var newBattery = Instantiate(batteries[0], pos, Quaternion.identity, transform);
        newBattery.GetComponent<BatteryController>().flyInTarget = tar;
    }

    void PreSpawnEnemy()
    {
        var switchRand = Random.Range(0, 4);
        switch (switchRand)
        {
            case 0:
                SpawnEnemy(upPositions, upPositionTarget);
                break;
            case 1:
                SpawnEnemy(downPositions, downPositionTarget);
                break;
            case 2:
                SpawnEnemy(leftPositions, leftPositionTarget);
                break;
            case 3:
                SpawnEnemy(rightPositions, rightPositionTarget);
                break;
        }
    }

    void SpawnEnemy(Vector3 pos, Vector3 tar)
    {
        enemiesOnField++;
        var newEnemy = Instantiate(enemies[0], pos, Quaternion.identity, transform);
        newEnemy.GetComponent<SawController>().flyInTarget = tar;
    }
}
