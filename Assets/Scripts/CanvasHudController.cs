using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasHudController : MonoBehaviour
{
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private Text textAccumulatedPower;
    private static float currentSliderValue = 50;
    public static float newSliderValue;
    public static float accumulatedPower;
    private float acculatedPowerShowing;
    public GameObject points;
    public static int pointsInField;
    public Color plus;
    public Color min;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    // Use this for initialization
    void Start ()
    {
        currentSliderValue = 50;
        newSliderValue = 50;
        accumulatedPower = 0;
        acculatedPowerShowing = 0;
        pointsInField = 0;

        textAccumulatedPower.text = "0";
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (GameManager.instance.gameState == GameManager.GameStates.GameOver)
        {
            if (canvasGroup.alpha > 0)
            {
                canvasGroup.alpha -= Time.deltaTime;
            }
        }

        slider.value = Mathf.Lerp(slider.value, newSliderValue, 100 * Time.deltaTime);

        if (acculatedPowerShowing < accumulatedPower)
        {
            acculatedPowerShowing++;
            //SpawnPoints("+", Random.Range(1, 10));
            textAccumulatedPower.text = ""+acculatedPowerShowing;
        }
        else
        {
            //SpawnPoints("-", 1);
        }
    }

    public void SpawnPoints(string minorplus, int pointsText)
    {
        if (pointsInField < 1)
        {
            pointsInField++;
            var newPoint = Instantiate(points, transform);
            var pos = new Vector3(newPoint.transform.position.x + Random.Range(-1f, 1f), newPoint.transform.position.y + Random.Range(-1f, 1f), newPoint.transform.position.z);
            newPoint.transform.position = pos;
            newPoint.GetComponent<Text>().text = minorplus+"" + pointsText;
            if (minorplus == "-")
            {
                newPoint.GetComponent<Text>().color = min;
            }
            else
            {
                newPoint.GetComponent<Text>().color = plus;
            }
        }

    }
}
