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
		// Movement
		rigidBody.velocity =  new Vector3(Input.GetAxis("Stick 1 Horizontal") * moveSpeed,
										  rigidBody.velocity.y, 
										  Input.GetAxis("Stick 1 Vertical") * moveSpeed);
		// View Diretion 
		Vector3 viewDir = new Vector3(Input.GetAxis("Stick 2 Horizontal"), 0, Input.GetAxis("Stick 2 Vertical")).normalized;
		laserBeam.SetPosition(0, transform.position);
		laserBeam.SetPosition(1, transform.position + viewDir * laserBeamLength);
		
		// Jumping
		if(Input.GetButtonDown("Button A") && isGrounded) {
			rigidBody.AddForce(Vector3.up * jumpPower);
		}
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
}
