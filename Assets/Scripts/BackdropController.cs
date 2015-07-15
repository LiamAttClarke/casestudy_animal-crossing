using UnityEngine;
using System.Collections;

public class BackdropController : MonoBehaviour {
	Transform playerTransform;
	Vector3 backdropStartPos;
	void Awake() {
		playerTransform = GameObject.Find("Player").transform;
	}
	void Start () {
		backdropStartPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(backdropStartPos.x, 
										 backdropStartPos.y, 
										 backdropStartPos.z + playerTransform.position.z);
	}
}