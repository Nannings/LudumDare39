using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public enum GameStates
    {
        Playing,
        GameOver,
        Waiting,
        Starting
    }
    public GameStates gameState = GameStates.Waiting;
    AsyncOperation async;

    private void Awake()
    {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start ()
    {
        StartCoroutine(CoLoadLevel());

    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void LoadLevel()
    {
        gameState = GameStates.Waiting;
        async.allowSceneActivation = true;
        StartCoroutine(CoLoadLevel());
    }

    IEnumerator CoLoadLevel()
    {
        async = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        async.allowSceneActivation = false;
        yield return async;
    }
}
