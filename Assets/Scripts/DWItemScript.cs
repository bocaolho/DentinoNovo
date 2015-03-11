using UnityEngine;
using System.Collections;

public class DWItemScript : MonoBehaviour {

	public int itemType;

	void OnCollisionEnter2D (Collision2D coll) {
		if (coll.gameObject.tag == "Player") {
			coll.gameObject.GetComponent<DentinoControllerScript>().gotItem (itemType);
			Destroy (gameObject);
		}
	}
}
