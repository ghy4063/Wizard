using UnityEngine;
using System.Collections;

public class EnemyBullets : MonoBehaviour {
	public float force;
	public int damgeOfBullet;
	public float destroyBulletAfter;

	// Use this for initialization
	void Start () {
		this.gameObject.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (force, 0));
		Destroy (gameObject, destroyBulletAfter);
	}
	
	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.CompareTag ("Player")) {
			//Player Take damge
			Player.player.playerTookDamage();
			Destroy (gameObject);
		}
	}
}