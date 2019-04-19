using UnityEngine;
using System.Collections;

public class Boss : MonoBehaviour {
	[Header("Boss Stats")]
	public int bossHealth;
	public float fightDelay;
	public float movementSpeed;

	[Header("Boss Movement")]
	public Transform firstPoint;
	public Transform secondPoint;
	public Transform thirdPoint;
	public Transform fourthPoint;
	public Transform fithPoint;

	[Header("Boss Summoning")]
	public int summonCount;
	public int howManySummons;
	public GameObject spawn1;
	public GameObject spawn2;
	public GameObject spawn3;
	public Transform spawnPoint1;
	public Transform spawnPoint2;
	public Transform spawnPoint3;
	public Transform spawnPoint4;

	[Header("Boss Bullet")]
	public GameObject bossBullet;
	public Vector3 bulletOffset;

	private bool readyToFight = true;
	private float timeTillFight;
	private float howCloseToPoint = 0.1f;
	private bool atFirstPoint;
	private bool atSecondPoint =  false;
	private bool atThirdPoint = false;
	private bool atFourthPoint = false;
	public static Boss boss;

	void Start(){
		if (!boss) {
			boss = this;
		} else {
			Destroy (this);
		}
	}
	// Update is called once per frame
	void Update () {
		timeTillFight -= Time.deltaTime;
		if (firstPoint && secondPoint && thirdPoint && fourthPoint && fithPoint) {
			Vector2 targetPosistion;
			if (atFirstPoint == true) {
				targetPosistion = firstPoint.position;
			} else if (atSecondPoint == true) {
				targetPosistion = secondPoint.position;
			} else if (atThirdPoint == true) {
				targetPosistion = thirdPoint.position;
			} else if (atFourthPoint == true) {
				targetPosistion = fourthPoint.position;
			} else {
				targetPosistion = fithPoint.position;
			}
			transform.position = Vector2.MoveTowards (new Vector2 (transform.position.x, transform.position.y), targetPosistion, movementSpeed * Time.deltaTime);
			if (Vector2.Distance (transform.position, targetPosistion) < howCloseToPoint) {
				if (atFirstPoint == true) {
					atFirstPoint = !atFirstPoint;
					atSecondPoint = !atSecondPoint;
				} else if (atSecondPoint == true) {
					atSecondPoint = !atSecondPoint;
					atThirdPoint = !atThirdPoint;
				} else if (atThirdPoint == true) {
					atThirdPoint = !atThirdPoint;
					atFourthPoint = !atFourthPoint;
				} else if (atFourthPoint == true) {
					atFourthPoint = !atFourthPoint;
				} else {
					atFirstPoint = !atFirstPoint;
				}
			}
		}
	}
	public void checkBossHealth(){
		bossHealth--;
		EnemyHealthBar.enemyHealthBar.healthBarTracker ();
		if (bossHealth == 0) {
			GameManager.gameManager.bossLostAllHealth();
		}
	}

	public void bossFightOptions(){
		if (summonCount < howManySummons) {
			if (timeTillFight <= 0) {
				readyToFight = true;
			} else {
				readyToFight = false;
			}
			if (readyToFight) {
				AudioSource.PlayClipAtPoint (GameManager.gameManager.bossSummonSound, Camera.main.transform.position);
				summonCount++;
				RandomSpawn ();
				timeTillFight = fightDelay;
			}
		} else if (summonCount >= howManySummons){
			if (timeTillFight <= 0) {
				readyToFight = true;
			} else {
				readyToFight = false;
			}
			if (readyToFight) {
				AudioSource.PlayClipAtPoint (GameManager.gameManager.bossShootSound, Camera.main.transform.position);
				GameObject newBullet;
				newBullet = Instantiate (bossBullet, transform.position + bulletOffset, Quaternion.identity) as GameObject;
				newBullet.GetComponent<EnemyBullets> ().force *= -1;
				timeTillFight = fightDelay;
			}
		}
	}
	void OnTriggerStay2D(Collider2D other){
		if (other.gameObject.CompareTag ("Player")) {
			bossFightOptions ();
		}
	}

	void RandomSpawn(){
		int spawnSpot = Random.Range (0, 11);
		switch (spawnSpot) {
		case 0:
			Instantiate (spawn1, spawnPoint1.position, Quaternion.Euler(0,0,0));
			break;
		case 1:
			Instantiate (spawn1, spawnPoint2.position, Quaternion.Euler(0,0,0));
			break;
		case 2:
			Instantiate (spawn1, spawnPoint3.position, Quaternion.Euler(0,0,0));
			break;
		case 3:
			Instantiate (spawn1, spawnPoint4.position, Quaternion.Euler(0,0,0));
			break;
		case 4:
			Instantiate (spawn2, spawnPoint1.position, Quaternion.Euler(0,0,0));
			break;
		case 5:
			Instantiate (spawn2, spawnPoint2.position, Quaternion.Euler(0,0,0));
			break;
		case 6:
			Instantiate (spawn2, spawnPoint3.position, Quaternion.Euler(0,0,0));
			break;
		case 7:
			Instantiate (spawn2, spawnPoint4.position, Quaternion.Euler(0,0,0));
			break;
		case 8:
			Instantiate (spawn3, spawnPoint1.position, Quaternion.Euler(0,0,0));
			break;
		case 9:
			Instantiate (spawn3, spawnPoint2.position, Quaternion.Euler(0,0,0));
			break;
		case 10:
			Instantiate (spawn3, spawnPoint3.position, Quaternion.Euler(0,0,0));
			break;
		case 11:
			Instantiate (spawn3, spawnPoint4.position, Quaternion.Euler(0,0,0));
			break;
		}
	}
}
