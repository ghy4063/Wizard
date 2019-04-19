using UnityEngine;
using System.Collections;

public class PlayerAnimation : MonoBehaviour {

	[Header("Player Animations")]
	public AnimationClip Player;
	public AnimationClip Run;
	public AnimationClip Jump;
	public AnimationClip PlayerShoot;
	public AnimationClip ShootRun;
	public AnimationClip ShootJump;

	public static PlayerAnimation smallAnim;
	private static Animator animator;

	// Use this for initialization
	void Start () {
		if (!smallAnim) {
			smallAnim = this;
		} else {
			Destroy (this);
		}
		animator = GetComponent<Animator>();
	}

	public void playerAnimation(){
		animator.Play (Player.name);
	}
	public void playerShootAnimation(){
		animator.Play (PlayerShoot.name);
	}
	public void runAnimation(){
		animator.Play(Run.name);
	}
	public void runShootAnimation(){
		animator.Play (ShootRun.name);
	}
	public void jumpAnimation(){
		animator.Play (Jump.name);
	}
	public void jumpShootAnimation(){
		animator.Play (ShootJump.name);
	}
}
