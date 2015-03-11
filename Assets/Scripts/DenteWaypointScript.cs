using UnityEngine;
using System.Collections;

public class DenteWaypointScript : MonoBehaviour {

	void OnTriggerEnter2D (Collider2D coll) {
		if (coll.gameObject.tag == "Player") {
			coll.gameObject.GetComponent<DynamicElementController>().backLocation = transform.position;
			Destroy (gameObject);
		}
	}
}
