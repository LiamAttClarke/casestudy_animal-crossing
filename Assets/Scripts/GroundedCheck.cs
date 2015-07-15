using UnityEngine;
using System.Collections;

public class GroundedCheck : MonoBehaviour {
	Player playerScript;
	void Awake() {
		playerScript = transform.parent.GetComponent<Player>();
	}
	
	void OnTriggerStay(Collider other) {
		if(other.tag == "Ground") {
			playerScript.isGrounded = true;
		}
	}
	
	void OnTriggerExit(Collider other) {
		if(other.tag == "Ground") {
			playerScript.isGrounded = false;
		}
	}
}
