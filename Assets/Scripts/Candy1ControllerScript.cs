using UnityEngine;
using System.Collections;

public class Candy1ControllerScript : MonoBehaviour {

	public ParticleSystem particles;
	public int hp = 2;
	public GameObject candyHold;
	private Rigidbody2D rigidBody2D;

	// Use this for initialization
	void Start () {
		rigidBody2D = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if (hp < 0) {
			Destroy (candyHold);
			rigidbody2D.mass = 5;
		}
	}

	public void hit () {
		particles.Emit (25);
		hp --;
	}
}
