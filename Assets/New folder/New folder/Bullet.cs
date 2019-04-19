using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	public float force;
	public int damgeOfBullet;
	public float destroyBulletAfter;

	public Enemy enemy;
	public DestructibleWalls dW;

	// Use this for initialization
	void Start () {
		this.gameObject.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (force, 0));
		Destroy (gameObject, destroyBulletAfter);
		dW = GameObject.FindObjectOfType (typeof(DestructibleWalls)) as DestructibleWalls;
		enemy = GameObject.FindObjectOfType (typeof(Enemy)) as Enemy;
	}
	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.CompareTag ("Enemy")) {
			//Enemy Take Damage
			Destroy (gameObject);
			other.gameObject.GetComponent<Enemy> ().checkEnemyHealth -= damgeOfBullet;
		}
		if (other.gameObject.CompareTag ("Boss")) {
			//Boss Take damge
			Destroy (gameObject);
			Boss.boss.checkBossHealth();
		}
		if (other.gameObject.CompareTag ("Destructible Walls")) {
			// Wall Take Damage
			Destroy (gameObject);
			other.gameObject.GetComponent<DestructibleWalls> ().checkWallHealth -= damgeOfBullet;
		}
	}
}
