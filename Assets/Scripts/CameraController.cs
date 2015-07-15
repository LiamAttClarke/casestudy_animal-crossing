using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	Transform playerTransform;
	Vector3 cameraStartPos;
	void Awake() {
		playerTransform = GameObject.Find("Player").transform;
	}
	void Start () {
		cameraStartPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(cameraStartPos.x + playerTransform.position.x, 
										 cameraStartPos.y, 
										 cameraStartPos.z + playerTransform.position.z);
	}
}
