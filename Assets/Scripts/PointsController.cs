using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsController : MonoBehaviour
{
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    // Use this for initialization
    void Start ()
    {
        canvasGroup.alpha = 1;

    }
	
	// Update is called once per frame
	void Update ()
    {
        canvasGroup.alpha -= Time.deltaTime / 2F;
        if (canvasGroup.alpha <= 0)
        {
            CanvasHudController.pointsInField--;
            Destroy(this.gameObject);
        }
    }
}
