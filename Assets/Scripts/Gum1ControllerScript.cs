using UnityEngine;
using System.Collections;

public class Gum1ControllerScript : MonoBehaviour {

	public ParticleSystem particles;
	public Sprite[] hpImages;
	private int a_hp;

	// Use this for initialization
	void Start () {
		a_hp = hpImages.Length - 1;
	}
	
	// Update is called once per frame
	void Update () {
		if (a_hp < 0) {
			GetComponent<SpriteRenderer>().sprite = null;
			GetComponent<BoxCollider2D>().enabled = false;
			Invoke ("destroySelf", 5);
		} else
			GetComponent<SpriteRenderer>().sprite = hpImages[a_hp];
	}
	
	void OnCollisionEnter2D (Collision2D coll) {

	}

	void OnTriggerEnter2D (Collider2D coll) {

	}

	public void hit() {
		a_hp--;
		particles.Emit (15);
	}

	private void destroySelf() {
		Destroy (gameObject);
	}
}
