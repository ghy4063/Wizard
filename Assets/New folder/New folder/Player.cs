using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {
	
	[Header("Player Upgrade")]
	public GameObject smallPlayer;
	public GameObject bigPlayer;
	public bool inBigPlayerMood = false;

	[Header("Player Controls")]
	public List<KeyCode> jumpKey;
	public  List<KeyCode> leftKey;
	public List<KeyCode> rightKey;
	public List<KeyCode> shootKey;

	[Header ("Player Stats")]
	public int playerHealth;
	public int moveSpeed;
	public int jumpHeight;
	public int doubleJumpHeith;
	public float shootDelay;
	public bool isTouchingGround = true;
	public bool didDoubleJump = false;

	[Header("Player Bullets")]
	public Vector3 bulletOffset = new Vector3 (.1f,0,0);
	public Vector3 bigBulletOffset = new Vector3 (.1f,0,0);
	public GameObject smallBulletPrefab;
	public GameObject bigBulletPrefab;

	private bool isRunning = false;
	private bool isFacingToTheRight = true;
	private bool readyToShoot = true;
	private float timeTillShoot;
	private int movedirection;

	public static Player player;
	private Transform tf;
	private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		tf = GetComponent<Transform>();
		rb = GetComponent<Rigidbody2D> ();
		Vector3 firstPoint = tf.position;
		if (GameManager.gameManager) {
			GameManager.gameManager.lastCheckpoint = firstPoint;
		}
		if (!player) {
			player = this;
		} else {
			Destroy (this);
		}
	}
	/*void Awake(){
		if (GameManager.gameManager.dontDestroy == true) {
			DontDestroyOnLoad (gameObject);
		}
	}*/
	
	// Update is called once per frame
	void Update () {
		timeTillShoot -= Time.deltaTime;
		movedirection = 0;

		foreach (KeyCode left in leftKey)
		{
			if (Input.GetKey(left)) {
				if (isFacingToTheRight == true) {
					flipThePlayer ();
				}
				isFacingToTheRight = false;
				movedirection = -1;
				playerMove (movedirection);
			}
		}
		foreach (KeyCode right in rightKey)
		{
			if (Input.GetKey(right)) {
				if (isFacingToTheRight == false) {
					flipThePlayer ();
				}
				isFacingToTheRight = true;
				movedirection = 1;
				playerMove (movedirection);
			}
		}
		foreach (KeyCode jump in jumpKey)
		{
			if (Input.GetKeyDown(jump)) {
				Jump ();
			}
		}
		foreach (KeyCode shoot in shootKey)
		{
			if (Input.GetKeyDown(shoot)) {
				playerShoot ();
			}
		}

		isRunning = movedirection != 0;
		if (isRunning == false) {
			if (isTouchingGround) {
				if (inBigPlayerMood == true) {
					BigPlayerAnimation.bigAnim.bigPlayerAnimation ();
				} else {
					PlayerAnimation.smallAnim.playerAnimation ();
				}
			}
		}
	}

	public void flipThePlayer(){
		Vector3 flipScale = transform.localScale;
		flipScale.x *= -1;
		transform.localScale = flipScale;
		bulletOffset *= -1;
	}
	public void playerMove(int movedirection) {
		isRunning = true;
		if (isRunning == true && isTouchingGround == true) {
			if (inBigPlayerMood == true) {
				BigPlayerAnimation.bigAnim.bigRunAnimation();
			} else {
				PlayerAnimation.smallAnim.runAnimation ();
			}
		}
		Vector3 movement = new Vector3 (movedirection * moveSpeed, 0, 0);
		tf.Translate (movement * Time.deltaTime, Space.World);
	}
	public void Jump(){
		AudioSource.PlayClipAtPoint (GameManager.gameManager.playerJumpSound, Camera.main.transform.position);
		if (inBigPlayerMood == true) {
			BigPlayerAnimation.bigAnim.bigJumpAnimation ();
		}else{
			PlayerAnimation.smallAnim.jumpAnimation ();
		}
		if (isTouchingGround){
			isTouchingGround = false;
			rb.AddForce (new Vector2 (0, jumpHeight), ForceMode2D.Impulse);
		}else if (didDoubleJump == false){
			didDoubleJump = true;
			rb.velocity = new Vector2 (rb.velocity.x, 0);
			rb.AddForce (new Vector2 (0, doubleJumpHeith), ForceMode2D.Impulse);
		}
	}
	public void playerShoot(){
		if (inBigPlayerMood == true) {
			if ( timeTillShoot<= 0) {
				readyToShoot = true;
			} else {
				readyToShoot = false;
			}
			if (readyToShoot) {
				AudioSource.PlayClipAtPoint (GameManager.gameManager.bigPlayerShootSound, Camera.main.transform.position);
				if(isRunning == false && isTouchingGround == true){
					BigPlayerAnimation.bigAnim.bigPlayerShootAnimation ();
				}
				if(isRunning == true && isTouchingGround == true){
					BigPlayerAnimation.bigAnim.bigRunShootAnimation ();
				}
				if (isTouchingGround == false) {
					BigPlayerAnimation.bigAnim.bigJumpShootAnimation ();
				}
				GameObject newBullet;
				newBullet = Instantiate (bigBulletPrefab, transform.position + bigBulletOffset, Quaternion.identity) as GameObject;
				if (isFacingToTheRight == true) {
					newBullet.GetComponent<Bullet> ().force *= 1;
				} else {
					newBullet.GetComponent<Bullet> ().force *= -1;
				}
				timeTillShoot = shootDelay;
			}
		}else{
			if ( timeTillShoot<= 0) {
				readyToShoot = true;
			} else {
				readyToShoot = false;
			}
			if (readyToShoot == true) {
				AudioSource.PlayClipAtPoint (GameManager.gameManager.smallPlayerShootSound, Camera.main.transform.position);
				if(isRunning == false && isTouchingGround == true){
					PlayerAnimation.smallAnim.playerShootAnimation ();
				}
				if(isRunning == true && isTouchingGround == true){
					PlayerAnimation.smallAnim.runShootAnimation ();
				}
				if (isTouchingGround == false) {
					PlayerAnimation.smallAnim.jumpShootAnimation ();
				}
				GameObject newBullet;
				newBullet = Instantiate (smallBulletPrefab, transform.position + bulletOffset, Quaternion.identity) as GameObject;
				if (isFacingToTheRight == true) {
					newBullet.GetComponent<Bullet> ().force *= 1;
				} else {
					newBullet.GetComponent<Bullet> ().force *= -1;
				}
				timeTillShoot = shootDelay;
			}
		}
	}
	public void playerTookDamage(){
		if (inBigPlayerMood == false) {
			playerHealth = playerHealth - 1;
			if (playerHealth <= 0) {
				GameManager.gameManager.playerLostAllHealth();
			}else{
				AudioSource.PlayClipAtPoint (GameManager.gameManager.playerTakeDamgeSound, Camera.main.transform.position);
				this.gameObject.transform.position = GameManager.gameManager.lastCheckpoint;
			}
		}else{
			smallPlayer.SetActive(true);
			bigPlayer.SetActive (false);
			inBigPlayerMood = false;
		}
	}
	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.CompareTag ("Check Point")) {
			GameManager.gameManager.lastCheckpoint = this.gameObject.transform.position;
		}
		if (other.gameObject.CompareTag ("Power Up")) {
			AudioSource.PlayClipAtPoint (GameManager.gameManager.pickUpUpgradSound, Camera.main.transform.position);
			other.gameObject.SetActive (false);
			smallPlayer.SetActive (false);
			bigPlayer.SetActive (true);
			inBigPlayerMood = true;
		}
		if (other.gameObject.CompareTag ("Coin")) {
			other.gameObject.SetActive (false);
			GameManager.gameManager.pickUpCoins();
		}
		if (other.gameObject.CompareTag ("Enemy")) {
			playerTookDamage ();
		}
		if (other.gameObject.CompareTag ("Boss")) {
			playerTookDamage ();
		}
		if (other.gameObject.CompareTag ("Kill Box")) {
			playerTookDamage ();
		}
	}
}
