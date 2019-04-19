using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour {
	// For when the quit button is pressed the game quits
	public void quitBtn(){
		Application.Quit();
	}
	// When the start button is pressed it goes to the game
	public void startBtn(){
		AudioSource.PlayClipAtPoint (GameManager.gameManager.buttonSound, Camera.main.transform.position);
		SceneManager.LoadScene ("OutSideLevel");
	}
	public void backToMain(){
		GameManager.gameManager.loseMenu.SetActive (false);
		GameManager.gameManager.winMenu.SetActive (false);
		GameManager.gameManager.mainMenu.SetActive (true);
	}
}
