using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public float moveSpeed = 6.0f;
	public float jumpPower = 200.0f;
	public bool isGrounded = true; 
	Rigidbody rigidBody;
	
	void Awake() {
		rigidBody = GetComponent<Rigidbody>();
	}
	// Use this for initialization
	void Start() {
		
	}
	
	// Update is called once per frame
	void Update () {
		rigidBody.velocity =  new Vector3(Input.GetAxis("Horizontal") * moveSpeed,
										  rigidBody.velocity.y, 
										  Input.GetAxis("Vertical") * moveSpeed);
		if(Input.GetButtonDown("Jump") && isGrounded) {
			rigidBody.AddForce(Vector3.up * jumpPower);
		}
	}
}
