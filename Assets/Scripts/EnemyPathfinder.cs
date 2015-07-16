using UnityEngine;
using System.Collections;

public class EnemyPathfinder : MonoBehaviour {
	public float movementSpeed = 2.0f;
	public float orbitRadius = 3.0f;
	public float rotationSpeed = 2.0f;
	Rigidbody rigidBody;
	GameObject player;
	void Awake() {
		rigidBody = GetComponent<Rigidbody>();
		player = GameObject.Find("Player");
	}
	// Use this for initialization
	void Start() {
		
	}
	
	// Update is called once per frame
	void Update() {
		float distToTarget = Vector3.Distance(transform.position, player.transform.position);
		if (distToTarget > orbitRadius) {
			MoveToTarget(new Vector3(player.transform.position.x, 0, player.transform.position.z));
		} else {
			rigidBody.velocity = Vector3.zero;
		}
		Vector3 playerPos = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
		Vector3 forwardDir = (playerPos - transform.position).normalized;
		Quaternion finalRotation = Quaternion.LookRotation(forwardDir, Vector3.up);
		transform.rotation = Quaternion.RotateTowards(transform.rotation, finalRotation, rotationSpeed);
		rigidBody.angularVelocity = Vector3.zero;
	}
	void MoveToTarget(Vector3 target) {
		Vector3 direction = (target - transform.position).normalized;
		rigidBody.velocity = direction * movementSpeed;
	}
}
