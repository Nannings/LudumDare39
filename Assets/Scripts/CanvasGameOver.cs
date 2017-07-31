using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasGameOver : MonoBehaviour
{
    public Text textScore;
    public Text textLocalScore;
    public static float localScore;

    private void Awake()
    {

    }

    private void OnEnable()
    {
        if (localScore < CanvasHudController.accumulatedPower)
        {
            localScore = CanvasHudController.accumulatedPower;
        }

        textScore.text = "" + CanvasHudController.accumulatedPower;
        textLocalScore.text = "" + localScore;
    }

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {

	}

    public void RestartButton()
    {
        GameManager.instance.LoadLevel();
    }

}
