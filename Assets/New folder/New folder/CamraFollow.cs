using UnityEngine;
using System.Collections;

public class CamraFollow : MonoBehaviour {
	public Transform playerTransform;
	public float cameraMoveSpeed;
	public Vector3 cameraPosition = new Vector3 (0, 0, -10);

	void Awake(){
		DontDestroyOnLoad (gameObject);
	}

	// Update is called once per frame
	void Update () {
		this.gameObject.transform.position = Vector3.Lerp (this.gameObject.transform.position, playerTransform.position + cameraPosition, cameraMoveSpeed * Time.deltaTime);
	}
}
