using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public LevelManager levelScript;
    public GameObject snakePrefab;
    public GameObject snakeInstance;
    public GameObject snakeAIPref;
    public GameObject snakeAIInstance;
    public bool EnableEnemies = false;

    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
      
        StartGame();
    }

    public void StartGame()
    {
        levelScript = GetComponent<LevelManager>();
        Debug.Log("spawned");
        snakeInstance = Instantiate(snakePrefab, Vector3.zero, Quaternion.identity);
        if(EnableEnemies)
        snakeAIInstance =Instantiate(snakeAIPref, new Vector3(24,24), Quaternion.identity);
        snakeInstance.name = "SnakeHead";
        levelScript.SpawnFood();

    }
    public void ReloadGame()
    {
        Snake snakeScript = snakeInstance.GetComponent<Snake>();
        for (int i =0;i< snakeScript.bodyParts.Count;i++)
        {
            Destroy(snakeScript.bodyParts[i]);
        }
        Destroy(snakeInstance);
        if(EnableEnemies && snakeAIInstance)
        {
            SnakeAI snakeAIScript = snakeAIInstance.GetComponent<SnakeAI>();
            for (int i = 0; i < snakeAIScript.bodyParts.Count; i++)
            {
                Destroy(snakeAIScript.bodyParts[i]);
            }
            Destroy(snakeAIInstance);
        }
        levelScript.ClearScreen();
        StatsManager.statsManager.reset();
        StartGame();
    }
    void Start()
    { 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
