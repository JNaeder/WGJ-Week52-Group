using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour {

    public int wave;

	public TextMeshProUGUI scoreNum, waveNum;
	public Text highscoreNum, finalScoreNum;

    int numberOfEnemies;

    public static int numberOfEnemiesLeft = 0;
    public static int score, highScore;

    public GameObject deathScreen, highScroreScreen;

    EnemySpawn[] enemySpawns;
	CameraBossManager camManager;

    HighScoreManager hSM;

    // Use this for initialization
    void Start() {
        enemySpawns = FindObjectsOfType<EnemySpawn>();
		camManager = FindObjectOfType<CameraBossManager>();
        hSM = GetComponent<HighScoreManager>();
		numberOfEnemiesLeft = 0;
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
		camManager.ActivateCameras();
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
        }



    }


    public void ResetScene() {
        SceneManager.LoadScene(0);
        score = 0;
        numberOfEnemiesLeft = 0;
    }

    public void GoToHighScoreScreen() {
        highScroreScreen.SetActive(true);
        deathScreen.SetActive(false);

    }

    public void GoToDeathScreen() {
        highScroreScreen.SetActive(false);
        deathScreen.SetActive(true);

    }


    public void ShowDeathScreen() {
        deathScreen.SetActive(true);
        finalScoreNum.text = score.ToString();
        highscoreNum.text = highScore.ToString();

    }


    public void SubmitHighScore() {
        hSM.AddNewHighScore(score);

    }

}
