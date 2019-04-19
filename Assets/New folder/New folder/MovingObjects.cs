using UnityEngine;
using System.Collections;

public class MovingObjects : MonoBehaviour {

	public Transform firstPoint;
	public Transform secondPoint;
	public float movementSpeed;

	private float howCloseToPoint = 0.1f;
	private bool atFirstPoint;
	
	// Update is called once per frame
	void Update () {
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
}
