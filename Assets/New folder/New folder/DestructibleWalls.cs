using UnityEngine;
using System.Collections;

public class DestructibleWalls : MonoBehaviour {
	public int wallHealth;

	public int checkWallHealth{
		get {
			return wallHealth;
		}
		set {
			wallHealth = value;
			AudioSource.PlayClipAtPoint (GameManager.gameManager.destructibleWallSound, Camera.main.transform.position);
			if (wallHealth <= 0) {
				Destroy (this.gameObject);
			}
		}
	}
}
