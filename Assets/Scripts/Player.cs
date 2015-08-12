using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public float moveSpeed = 6.0f;
	public float jumpPower = 200.0f;
	public GameObject loadOut;
	float fireRate = 0.01f;
	float firePower = 500f;
	public bool isGrounded = true; 
	Rigidbody rigidBody;
	bool isAiming = false;
	float timeSinceFired = 0;
	void Awake() {
		rigidBody = GetComponent<Rigidbody>();
	}
	
	void Start() {
		timeSinceFired = fireRate;
	}
	
	// Update is called once per frame
	void Update () {
		// Movement
		rigidBody.velocity =  new Vector3(Input.GetAxis("Stick 1 Horizontal") * moveSpeed,
										  rigidBody.velocity.y, 
										  Input.GetAxis("Stick 1 Vertical") * moveSpeed);
		// View Diretion 
		Vector3 viewDir = new Vector3(Input.GetAxis("Stick 2 Horizontal"), 0, Input.GetAxis("Stick 2 Vertical")).normalized;
		if (viewDir != Vector3.zero) {
			transform.rotation = Quaternion.LookRotation(viewDir);
			isAiming = true;
		} else {
			isAiming = false;
		}
		// Jumping
		if(Input.GetButtonDown("Button A") && isGrounded) {
			rigidBody.AddForce(Vector3.up * jumpPower);
		}
		if(Input.GetAxis("Trigger 2") > 0.0f && isAiming) {
			timeSinceFired += Time.deltaTime;
			if(timeSinceFired >= fireRate) {
				timeSinceFired = 0.0f;
				Fire(loadOut);
			}
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
	
	void Fire(GameObject ammo) {
		Vector3 forward = transform.forward;
		GameObject bullet = (GameObject)Instantiate(ammo, transform.position + forward, transform.rotation);
		bullet.GetComponent<Rigidbody>().AddForce(forward * firePower);
	}
}
