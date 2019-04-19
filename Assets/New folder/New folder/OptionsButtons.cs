using UnityEngine;
using System.Collections;

public class OptionsButtons : MonoBehaviour {

	public void resumeBtn(){
		AudioSource.PlayClipAtPoint (GameManager.gameManager.buttonSound, Camera.main.transform.position);
		GameManager.gameManager.optionMenu.SetActive (false);
		GameManager.gameManager.playerController.SetActive (true);
	}
	public void exitBtn(){
		Application.Quit ();
	}
}
