using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private Vector2 gridPosition;
    private Vector2 moveDirection;
    private float faceDirection;
    private float oldTime;
    private float currenTime = 0;
    public float gamespeed = 0.2f;
    private BoxCollider2D boxCollider;
    public LayerMask blockingLayer;
    private bool canMove = false;
    private int bodySize = 1;
    public List<Vector2> snakeMoveHistory;
    private List<Quaternion> rotationHistory;
    public List<GameObject> bodyParts;
    public GameObject bodyPref;
    private AudioSource eat;
    private AudioSource death;

    // Start is called before the first frame update
    void Start()
    {
        eat = GameObject.Find("Eat").GetComponent<AudioSource>();
        death = GameObject.Find("Death").GetComponent<AudioSource>();
        gridPosition = new Vector2(0, 0);
        oldTime = 0;
        moveDirection = new Vector2(0, 1);
        boxCollider = GetComponent<BoxCollider2D>();
        bodyParts = new List<GameObject>();
        snakeMoveHistory = new List<Vector2>();
        rotationHistory = new List<Quaternion>();
        snakeMoveHistory.Insert(0, gridPosition);
        rotationHistory.Insert(0, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        currenTime = Time.time;
        handleKeyPress();
        if (currenTime - oldTime >=gamespeed)
        {
         
            RaycastHit2D hit;
            canMove = Move((int)moveDirection.x, (int)moveDirection.y, out hit);
            if(!canMove)
            {
                death.Play();
                GameManager.instance.ReloadGame();
            }
            faceDirection = Mathf.Round(GetAngleFromVector(moveDirection));
            transform.eulerAngles = new Vector3(0, 0, faceDirection);

            
            snakeMoveHistory[0] = gridPosition;
            rotationHistory[0] = transform.rotation;
            oldTime = Time.time;
        }
    }
    private void handleKeyPress()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (faceDirection!= 180)
            {
                moveDirection = new Vector2(0, 1);
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (Mathf.Round(faceDirection) != 0)
            {
                moveDirection = new Vector2(0, -1);
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (faceDirection != 270)
            {
                moveDirection = new Vector2(-1, 0);
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (faceDirection != 90)
            {
                moveDirection = new Vector2(1, 0);
            }
        }
    }
    private float GetAngleFromVector(Vector2 dir)
    {
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg-90;
        if (n < 0) n += 360;
        return n;
    }
    private bool Move(int xDir, int yDir, out RaycastHit2D hit)
    {
        Vector2 start = transform.position; 
        Vector2 end = start + new Vector2(xDir, yDir);
        boxCollider.enabled = false;
        hit = Physics2D.Linecast(start, end, blockingLayer);
        boxCollider.enabled = true;
        if (hit.transform == null)
        {
            gridPosition += new Vector2(xDir, yDir);
            gridPosition = GameManager.instance.levelScript.ValidateGridPosition(gridPosition);
            transform.position = gridPosition;
            for (int i = 0; i < bodyParts.Count; i++)
            {
                bodyParts[i].transform.position = snakeMoveHistory[i];
                bodyParts[i].transform.rotation = rotationHistory[i];
                // Instantiate(bodyPref,new Vector3(snakeMovePosition.x,snakeMovePosition.y),Quaternion.identity);
            }
            if (GameManager.instance.levelScript.TrySnakeEatFood(transform.position))
            {
                bodySize++;
                eat.Play();
                Vector2 spawnPosition = snakeMoveHistory[snakeMoveHistory.Count - 1];
                Quaternion rotation = rotationHistory[rotationHistory.Count - 1];
                GameObject bodypart = Instantiate(bodyPref, new Vector3(spawnPosition.x, spawnPosition.y), rotation);
                bodypart.GetComponent<SpriteRenderer>().color = Random.ColorHSV(0.3f,0.5f,0.1f,0.5f,0.7f,0.9f);
                bodyParts.Add(bodypart);
                snakeMoveHistory.Add(spawnPosition);
                rotationHistory.Add(rotation);
                StatsManager.statsManager.UpdateScorePlayer(10);
            }
            for (int i = 0; i < bodyParts.Count; i++)
            {
                snakeMoveHistory[i + 1] = bodyParts[i].transform.position;
                rotationHistory[i + 1] = bodyParts[i].transform.rotation;
            }
            //  rb2D.MovePosition(end);
            return true;
        }
        return false;
    }
}