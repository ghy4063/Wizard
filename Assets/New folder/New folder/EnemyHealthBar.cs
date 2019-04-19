using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour {
	public static EnemyHealthBar enemyHealthBar;
	public Slider healthBar;

	void Start(){
		if (!enemyHealthBar) {
			enemyHealthBar = this;
		} else {
			Destroy (this);
		}
	}
	public void healthBarTracker(){
		healthBar.value = Boss.boss.bossHealth;
	}
}
