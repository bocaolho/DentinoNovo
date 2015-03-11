using UnityEngine;
using System.Collections;

public class DentinoEscovaScript : MonoBehaviour {

	DentinoControllerScript dcs;

	// Use this for initialization
	void Start () {
		dcs = gameObject.transform.parent.gameObject.GetComponent<DentinoControllerScript>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D coll) {
		HandleCollision (coll.gameObject);
		HandleCollisionEnter (coll.gameObject);
	}

	void OnCollisionEnter2D(Collision2D coll) {
		HandleCollision (coll.gameObject);
		HandleCollisionEnter (coll.gameObject);
	}

	void OnTriggerStay2D(Collider2D coll) {
		HandleCollision (coll.gameObject);
	}
	
	void OnTriggerStay2D(Collision2D coll) {
		HandleCollision (coll.gameObject);
	}

	private void HandleCollision(GameObject go) {
		if (go.tag == "Enemy") {
			if (dcs.attacking) {
				DefaultEnemyScript des = go.GetComponent<DefaultEnemyScript>();
				des.Attack (1);
			}
		}
		if (go.tag == "Gum1") {
			if (dcs.attacking && !dcs.attackHandled) {
				Gum1ControllerScript des = go.GetComponent<Gum1ControllerScript>();
				dcs.attackHandled = true;
				des.hit ();
			}
		}
		if (go.tag == "Candy") {
			if (dcs.attacking && !dcs.attackHandled) {
				Candy1ControllerScript ccs = go.GetComponent<Candy1ControllerScript>();
				dcs.attackHandled = true;
				ccs.hit ();
			}
		}
	}
	
	private void HandleCollisionEnter(GameObject go) {

	}

	public void stopAttack()
	{
		dcs.attackStop();
	}

	public void startAttack()
	{
		dcs.attackStart();
	}
}
