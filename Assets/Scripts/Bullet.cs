using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	void OnCollisionEnter(Collision col) {
		GameObject hit = col.gameObject;
		Health health = hit.GetComponent<Health> ();
		if (health != null) {
			health.takeDamage (10);
		}
		Destroy (this.gameObject);
	}
}
