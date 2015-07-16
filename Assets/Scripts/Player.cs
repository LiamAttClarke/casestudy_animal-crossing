using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public float moveSpeed = 6.0f;
	public float jumpPower = 200.0f;
	public float laserBeamLength = 2.0f;
	public bool isGrounded = true; 
	Rigidbody rigidBody;
	LineRenderer laserBeam;
	
	void Awake() {
		rigidBody = GetComponent<Rigidbody>();
		laserBeam = GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		rigidBody.velocity =  new Vector3(Input.GetAxis("Horizontal") * moveSpeed,
										  rigidBody.velocity.y, 
										  Input.GetAxis("Vertical") * moveSpeed);
		if(Input.GetButtonDown("Jump") && isGrounded) {
			rigidBody.AddForce(Vector3.up * jumpPower);
		}
		Vector3 viewDir = ViewDirection();
		laserBeam.SetPosition(0, transform.position);
		laserBeam.SetPosition(1, transform.position + viewDir * laserBeamLength);
	}
	void OnTriggerStay(Collider other) {
		if(other.tag == "Ground") {
			isGrounded = true;
		}
	}
	
	void OnTriggerExit(Collider other) {
		if(other.tag == "Ground") {
			isGrounded = false;
		}
	}
	
	Vector3 ViewDirection() {
		Vector3 viewPortPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
		Vector3 viewDir = new Vector3(viewPortPos.x * 2f - 1f, 0, viewPortPos.y * 2f - 1f).normalized; // convert [0,1] viewport coords to [-1,1] 
		return viewDir;
	}
}
