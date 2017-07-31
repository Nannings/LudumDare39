using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasWaiting : MonoBehaviour
{
    private GameManager gameManager;
    private Text textTutorial;
    private Vector2 textTutorialStartPosition;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        textTutorial = GetComponentInChildren<Text>();
        textTutorialStartPosition = textTutorial.transform.position;
    }

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (gameManager.gameState != GameManager.GameStates.Waiting)
        {
            var textTutorialEndPosition = new Vector2(textTutorialStartPosition.x, textTutorialStartPosition.y - 50);
            textTutorial.transform.position = Vector2.MoveTowards(textTutorial.transform.position, textTutorialEndPosition, 25 * Time.deltaTime);
        }
	}
}
