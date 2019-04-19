using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	public static GameManager gameManager;
	//private int whichScene;
	public Vector3 lastCheckpoint;
	public GameObject playerController;

	[Header ("Menus")]
	public GameObject optionMenu;
	public GameObject mainMenu;
	public GameObject loseMenu;
	public GameObject winMenu;

	[Header("Coins")]
	public int numberOfCoinsInMap;
	public int getLifeFromCoin1;
	public int getLifeFromCoin2;
	public int getLifeFromCoin3;
	public int getLifeFromCoin4;
	public int getLifeFromCoin5;
	public int coinsPickedUp;

	[Header("Sounds")]
	public AudioClip playerTakeDamgeSound;
	public AudioClip bigPlayerShootSound;
	public AudioClip smallPlayerShootSound;
	public AudioClip playerJumpSound;
	public AudioClip pickUpCoinSound;
	public AudioClip pickUpUpgradSound;
	public AudioClip gainALifeSound;
	public AudioClip upgradeSound;
	public AudioClip enemySootSound;
	public AudioClip bossShootSound;
	public AudioClip bossSummonSound;
	public AudioClip destructibleWallSound;
	public AudioClip buttonSound;
	public AudioClip loseSound;
	public AudioClip winSound;

	[Header("UI Text")]
	public Text coinText;
	public Text livesText;

	//public bool dontDestroy = true;
	// Use this for initialization
	void Start () {
		if (!gameManager) {
			gameManager = this;
		} else {
			Destroy (this);
		}
	}
	/*void Awake(){
		if (dontDestroy == true) {
			DontDestroyOnLoad (gameObject);
		}
	}*/
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			optionMenu.SetActive (true);
			playerController.SetActive (false);
		}
		livesText.text = "Lives: " + Player.player.playerHealth;
		coinText.text = coinsPickedUp + " / " + numberOfCoinsInMap + " Coins";
	}

	public void playerLostAllHealth(){
		loseMenu.SetActive (true);
		playerController.SetActive (false);
		AudioSource.PlayClipAtPoint (loseSound, Camera.main.transform.position);
	}
	public void bossLostAllHealth(){
		winMenu.SetActive (true);
		playerController.SetActive (false);
		AudioSource.PlayClipAtPoint (winSound, Camera.main.transform.position);
	}
	public void pickUpCoins(){
		AudioSource.PlayClipAtPoint (pickUpCoinSound, Camera.main.transform.position);
		coinsPickedUp++;
		if (coinsPickedUp == getLifeFromCoin1) {
			AudioSource.PlayClipAtPoint (gainALifeSound, Camera.main.transform.position);
			Player.player.playerHealth++;
		}
		if (coinsPickedUp == getLifeFromCoin2) {
			Player.player.playerHealth++;
		}
		if (coinsPickedUp == getLifeFromCoin3) {
			Player.player.playerHealth++;
		}
		if (coinsPickedUp == getLifeFromCoin4) {
			Player.player.playerHealth++;
		}
		if (coinsPickedUp == getLifeFromCoin5) {
			Player.player.playerHealth++;
		}
	}
	/*public void loadNewScene(){
		if (whichScene == 0) {
			whichScene = 1;
			SceneManager.LoadScene ("DungeonLevel");

		} else if (whichScene == 1) {
			whichScene = 0;

			SceneManager.LoadScene ("CryptLevel");
		}
	}*/
}
