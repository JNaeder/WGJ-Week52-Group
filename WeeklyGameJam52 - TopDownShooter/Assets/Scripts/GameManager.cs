using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public int wave;

    public Text scoreNum, waveNum, highscoreNum, finalScoreNum;

    int numberOfEnemies;

    public static int numberOfEnemiesLeft = 0;
    public static int score, highScore;
    public Texture2D cursorImage;

    public GameObject deathScreen;

    EnemySpawn[] enemySpawns;

    // Use this for initialization
    void Start() {
        enemySpawns = FindObjectsOfType<EnemySpawn>();


       Cursor.SetCursor(cursorImage, Vector2.zero, CursorMode.Auto);
    }

    // Update is called once per frame
    void Update() {
        scoreNum.text = score.ToString();
        waveNum.text = wave.ToString();

        if (numberOfEnemiesLeft <= 0) {
            NewWave();

        }
    }

    void NewWave() {
        wave++;
        numberOfEnemies = wave * 2;
        StartCoroutine(SpawnEnemies());
    }


    IEnumerator SpawnEnemies() {
        for (int i = 0; i < numberOfEnemies; i++) {
            int randomSpawnPos = Random.Range(0, enemySpawns.Length);
            enemySpawns[randomSpawnPos].SpawnNextEnemy();
            yield return new WaitForSeconds(1f);
        }

        yield break;
    }


    public void SetHighScore() {
        if (score > highScore) {
            highScore = score;
            Debug.Log("New HighScore: " + highScore);
        }



    }


    public void ResetScene() {
        SceneManager.LoadScene(0);
        score = 0;
        numberOfEnemiesLeft = 0;
    }


    public void ShowDeathScreen() {
        deathScreen.SetActive(true);
        finalScoreNum.text = score.ToString();
        highscoreNum.text = highScore.ToString();

    }

}
