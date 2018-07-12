using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour {

    public int wave;

	public TextMeshProUGUI scoreNum, waveNum, nextWaveTimer;
	public Text highscoreNum, finalScoreNum;
	public GameObject nextWaveBubble;

    int numberOfEnemies;
	public float timeToNextWave;
	public bool newWaveStarted;

    public static int numberOfEnemiesLeft = 0;
    public static int score, highScore;
	public float timeBetweenWaves;

    public GameObject deathScreen, highScroreScreen;

    EnemySpawn[] enemySpawns;
	CameraBossManager camManager;

    HighScoreManager hSM;
    musicScript musicScript;

    // Use this for initialization
    void Start() {
        enemySpawns = FindObjectsOfType<EnemySpawn>();
		camManager = FindObjectOfType<CameraBossManager>();
        hSM = GetComponent<HighScoreManager>();
		numberOfEnemiesLeft = 0;

        musicScript = FindObjectOfType<musicScript>();
        musicScript.SetGameManager(this);
    }

    // Update is called once per frame
    void Update() {
		UpdateUI();
		CheckIfNewWaveShouldStart();
    }


	void UpdateUI(){
		scoreNum.text = score.ToString();
        waveNum.text = wave.ToString();      
        if (timeToNextWave > 0)
        {
            nextWaveTimer.enabled = true;
			nextWaveBubble.SetActive(true);
            nextWaveTimer.text = timeToNextWave.ToString("F0");         
        }
        else
        {
			nextWaveBubble.SetActive(false);
            nextWaveTimer.enabled = false;         
        }      
        if (timeToNextWave > 0)
        {
            timeToNextWave -= Time.deltaTime;         
        }
    }




	void CheckIfNewWaveShouldStart(){
		if (!newWaveStarted)
        {
            if (numberOfEnemiesLeft <= 0)
            {
                StartCoroutine("NewWave");

            }
        }      
	}

	IEnumerator NewWave() {
		newWaveStarted = true;
		timeToNextWave = timeBetweenWaves;
		wave++;
		camManager.DeActivateAllCams();
		yield return new WaitForSeconds(timeBetweenWaves);
        numberOfEnemies = wave + 2;
		camManager.ActivateCameras();
        StartCoroutine(SpawnEnemies());
    }


    IEnumerator SpawnEnemies() {
        for (int i = 0; i < numberOfEnemies; i++) {
            int randomSpawnPos = Random.Range(0, enemySpawns.Length);
            enemySpawns[randomSpawnPos].SpawnNextEnemy();
            yield return new WaitForSeconds(3f);
        }
		newWaveStarted = false;
        yield break;
    }


    public void SetHighScore() {
        if (score > highScore) {
            highScore = score;
        }



    }


    public void ResetScene() {
        SceneManager.LoadScene(1);
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
        wave = 0;   
        deathScreen.SetActive(true);
        finalScoreNum.text = score.ToString();
        highscoreNum.text = highScore.ToString();

    }

	public void PauseMenu(){


	}

	public void UnPauseMenu(){



	}


    public void SubmitHighScore() {
        hSM.AddNewHighScore(score);

    }

}
