using UnityEngine;
using System.Collections;

public class BigPlayerAnimation : MonoBehaviour {

	[Header("Player Animations")]
	public AnimationClip BigPlayer;
	public AnimationClip BigRun;
	public AnimationClip BigJump;
	public AnimationClip BigPlayerShoot;
	public AnimationClip BigShootRun;
	public AnimationClip BigShootJump;

	private static Animator animator;

	public static BigPlayerAnimation bigAnim;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
		if (!bigAnim) {
			bigAnim = this;
		} else {
			Destroy (this);
		}
	}

	public void bigPlayerAnimation(){
		animator.Play (BigPlayer.name);
	}
	public void bigPlayerShootAnimation(){
		animator.Play (BigPlayerShoot.name);
	}
	public void bigRunAnimation(){
		animator.Play(BigRun.name);
	}
	public void bigRunShootAnimation(){
		animator.Play (BigShootRun.name);
	}
	public void bigJumpAnimation(){
		animator.Play (BigJump.name);
	}
	public void bigJumpShootAnimation(){
		animator.Play (BigShootJump.name);
	}
}
