using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreManager : MonoBehaviour {

    const string privateCode = "1pKJ-OT9IkyGp7vMgn0TggPY6SMUfEWUayTGNy1zu_Gg";
    const string publicCode = "5b429d9f191a8a0bcc0d83ef";
    const string webURL = "http://dreamlo.com/lb/";

    public Highscore[] highscoresList;

    public Text[] highscoreUserNames;
    public Text[] highscoreScores;

    string playerName;

    private void Awake()
    {
        DownloadHighScores();
    }


    public void UpdatePlayerName(string newName) {
        playerName = newName;
    }



    public void AddNewHighScore(int score) {
        StartCoroutine(UpLoadNewHighScore(playerName, score));


    }

    public void DownloadHighScores() {

        StartCoroutine("DownloadHighScoresFromServer");

    }


    IEnumerator UpLoadNewHighScore(string username, int score) {

        WWW www = new WWW(webURL + privateCode + "/add/" + WWW.EscapeURL(username) + "/" + score);
        yield return www;

        if (string.IsNullOrEmpty(www.error))
        {
            Debug.Log("Upload Siccessul");
        }
        else {
            Debug.Log("Error Uploading" + www.error);
        }


    }


    IEnumerator DownloadHighScoresFromServer() {
        WWW www = new WWW(webURL + publicCode + "/pipe/");
        yield return www;

        if (string.IsNullOrEmpty(www.error))
        {
            FormatHighScores(www.text);
        }
        else {
            print("Error downloading");
        }


    }


    void FormatHighScores(string textStream) {
        string[] entries = textStream.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
        highscoresList = new Highscore[entries.Length];
        for (int i = 0; i < entries.Length; i++) {
            string[] entryInfo = entries[i].Split(new char[] { '|' });
            string username = entryInfo[0];
            int score = int.Parse(entryInfo[1]);
            highscoresList[i] = new Highscore(username, score);
            //print(highscoresList[i].userName + " : " + highscoresList[i].score);
            highscoreUserNames[i].text = highscoresList[i].userName;
            highscoreScores[i].text = highscoresList[i].score.ToString();
        }

    }
}


public struct Highscore {
    public string userName;
    public int score;

    public Highscore(string _username, int _score) {
        userName = _username;
        score = _score;

    }
}
 
