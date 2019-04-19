using UnityEngine;
using System.Collections;

public class PlayerFeetTrigger : MonoBehaviour {
	
	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.CompareTag ("Floor")) {
			Player.player.isTouchingGround = true;
			Player.player.didDoubleJump = false;
		}
		if (other.gameObject.CompareTag ("Destructible Walls")) {
			Player.player.isTouchingGround = true;
			Player.player.didDoubleJump = false;
		}
		if (other.gameObject.CompareTag ("Check Point")) {
			Player.player.isTouchingGround = true;
			Player.player.didDoubleJump = false;
		}
	}
}
