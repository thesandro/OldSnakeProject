using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsManager : MonoBehaviour
{
    public static StatsManager statsManager;
    public Text PlayerName;
    public Text PlayerScore;
    public Text EnemyName;
    public Text EnemyScore;
    public Text CurrentTime;
    private int time = 0;
    private int playerScore = 0;
    private int enemyScore = 0;
    private void Awake()
    {
        statsManager = this;
    }
    // Start is called before the first frame update    
    void Start()
    {
        PlayerName.text = "Player: " + PlayerStats.playerName;
        PlayerScore.text = "Score: " + playerScore;
        EnemyName.text = "Enemy: " + PlayerStats.enemyName;
        EnemyScore.text = "Score: " + enemyScore;
        CurrentTime.text = "Time: " + time;
        StartCoroutine(TimeFromStart());
    }

    // Update is called once per frame
    public void reset()
    {
        playerScore = 0;
        enemyScore = 0;
        time = 0;
        PlayerName.text = "Player: " + PlayerStats.playerName;
        PlayerScore.text = "Score: " + playerScore;
        EnemyName.text = "Enemy: " + PlayerStats.enemyName;
        EnemyScore.text = "Score: " + enemyScore;
        CurrentTime.text = "Time: " + time;
    }
    public void UpdateScorePlayer(int points)
    {
        playerScore += points;
       PlayerScore.text = "Score: " + playerScore;
       PlayerStats.playerScore = playerScore;
    }
    public void UpdateScoreEnemy(int points)
    {
        enemyScore += points;
        EnemyScore.text = "Score: " + enemyScore;
        PlayerStats.enemyScore = enemyScore;
    }
    private IEnumerator TimeFromStart()
    {
        CurrentTime.text = "Time: " + (++time);
        PlayerStats.time = time;
        yield return new WaitForSeconds(1);
        StartCoroutine(TimeFromStart());
       
    }
}
