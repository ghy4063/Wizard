using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	[Header("Enemy Stats")]
	public float movementSpeed;
	public int enemyHealth;
	public bool canEnemyShoot;
	public float shootDelay;

	[Header("Enemy Move Points")]
	public Transform firstPoint;
	public Transform secondPoint;

	[Header("Enemy Bullet")]
	public GameObject enemyBullet;
	public Vector3 bulletOffset;

	private bool readyToFight = true;
	private float timeTillFight;
	private float howCloseToPoint = 0.1f;
	private bool atFirstPoint;

	public int checkEnemyHealth{
		get {
			return enemyHealth;
		}
		set {
			enemyHealth  = value;
			if (enemyHealth <= 0) {
				Destroy (this.gameObject);
			}
		}
	}
	// Update is called once per frame
	void Update () {
		timeTillFight -= Time.deltaTime;
		if (firstPoint && secondPoint) {
			Vector2 targetPosistion;
			if (atFirstPoint == true) {
				targetPosistion = secondPoint.position;
			} else {
				targetPosistion = firstPoint.position;
			}
			transform.position = Vector2.MoveTowards (new Vector2 (transform.position.x, transform.position.y), targetPosistion, movementSpeed * Time.deltaTime);
			if (Vector2.Distance (transform.position, targetPosistion) < howCloseToPoint) {
				atFirstPoint = !atFirstPoint;
			}
		}
	}

	public void enemyCanShoot(){
		if (canEnemyShoot == true) {
			if (timeTillFight <= 0) {
				readyToFight = true;
			} else {
				readyToFight = false;
			}
			if (readyToFight) {
				AudioSource.PlayClipAtPoint (GameManager.gameManager.enemySootSound, Camera.main.transform.position);
				GameObject newBullet;
				newBullet = Instantiate (enemyBullet, transform.position + bulletOffset, Quaternion.identity) as GameObject;
				newBullet.GetComponent<EnemyBullets> ().force *= -1;
				timeTillFight = shootDelay;
			}
		}
	}

	void OnTriggerStay2D(Collider2D other){
		if (other.gameObject.CompareTag ("Player")) {
			enemyCanShoot ();
		}
	}

}
