using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

	public Sprite[] startScreenSprites;
	public Image sP;





	public void StartGame(){
		SceneManager.LoadScene(1);

	}

	public void QuitGame(){
		Application.Quit();

	}
	public void ShowControls(){
		print("Controls!");
		sP.overrideSprite = startScreenSprites[1];

	}
	public void ShowCredits(){
		sP.overrideSprite = startScreenSprites[2];
		print("Credits!");

	}

	public void BackToMainMenu(){
		sP.sprite = startScreenSprites[0];

	}

    
}
