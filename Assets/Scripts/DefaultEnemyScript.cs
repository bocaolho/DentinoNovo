using UnityEngine;
using System.Collections;

// Default controller for a enemy. First script in all enemies.
public class DefaultEnemyScript : MonoBehaviour {

	// Events
	public delegate void AttackEvent (float value);
	public event AttackEvent OnAttack;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D coll) {
	}

	// Attack
	public void Attack (float value) {
		if (OnAttack != null)
			OnAttack (value);
	}
}
