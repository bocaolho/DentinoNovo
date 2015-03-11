using UnityEngine;
using System.Collections;

/*
 * Created by Joao Paulo T Ruschel in 
 * */
public class BackerController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// When a element hits 
	void OnCollisionEnter2D (Collision2D coll) {
		collisionBack (coll.gameObject);
	}
	void OnTriggerEnter2D (Collider2D coll) {
		collisionBack (coll.gameObject);
	}

	// Callback from Collision or Trigger enter
	private void collisionBack (GameObject go) {
		DynamicElementController dec = go.GetComponent<DynamicElementController>();
		if (dec != null) {
			dec.goBack ();
		}
	}
}
