using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    private Vector2 foodGridPosition;
    public GameObject foodGameObject;
    public int width = 25;
    public int height = 25;
    private Snake snake;
    private SnakeAI snakeAI;
    public GameObject foodPref;
    
    public void Start()
    {
    }

    public void SpawnFood()
    {
        snake = GameManager.instance.snakeInstance.GetComponent<Snake>();
        if(GameManager.instance.EnableEnemies)
        {
            snakeAI = GameManager.instance.snakeAIInstance.GetComponent<SnakeAI>();
        }
        do
        {
            foodGridPosition = new Vector2Int(Random.Range(0, width), Random.Range(0, height));
        } while (checkIfFree(Vector2Int.CeilToInt(foodGridPosition)));

        foodGameObject = Instantiate(foodPref);
        foodGameObject.name = "Food";
        foodGameObject.transform.position = new Vector3(foodGridPosition.x, foodGridPosition.y);
    }
    bool checkIfFree(Vector2Int foodGridPosition)
    {
        if (snake.snakeMoveHistory.IndexOf(foodGridPosition) != -1)
            return true;
        else if(snakeAI != null && snakeAI.snakeMoveHistory.IndexOf(foodGridPosition) != -1)
        { 
            return true;
        }
        return false;
    }

    public bool TrySnakeEatFood(Vector2 snakeGridPosition)
    {
        if (snakeGridPosition == foodGridPosition)
        {
            Destroy(foodGameObject);
            SpawnFood();
            return true;
        }
        else
        {
            return false;
        }
    }

    public Vector2 ValidateGridPosition(Vector2 gridPosition)
    {
        if (gridPosition.x < 0)
        {
            gridPosition.x = width - 1;
        }
        if (gridPosition.x > width - 1)
        {
            gridPosition.x = 0;
        }
        if (gridPosition.y < 0)
        {
            gridPosition.y = height - 1;
        }
        if (gridPosition.y > height - 1)
        {
            gridPosition.y = 0;
        }
        return gridPosition;
    }
    public void ClearScreen()
    {
        Destroy(foodGameObject);
    }
}
