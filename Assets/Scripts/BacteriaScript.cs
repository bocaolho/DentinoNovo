using UnityEngine;
using System.Collections;

public class BacteriaScript : MonoBehaviour {

	// Default enemy script
	private DefaultEnemyScript des;
	// Animator
	private Animator anim;

	// Use this for initialization
	void Start () {
		des = GetComponent<DefaultEnemyScript>();
		des.OnAttack += HandleOnAttack;
		anim = GetComponent<Animator>();
	}

	void HandleOnAttack (float value)
	{
		kill ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// Called when Death Animation is done
	public void animDeathDone()
	{
		Invoke ("DestroyElement", 2);
	}

	public void DestroyElement()
	{
		Destroy (gameObject);
	}

	// attacked and died
	public void kill() {
		anim.SetBool ("Dead", true);
	}

	void OnTriggerEnter2D(Collider2D coll) {
		/*if (coll.gameObject.tag == "Player") {
			DentinoControllerScript dcs = coll.gameObject.transform.parent.gameObject.GetComponent<DentinoControllerScript>();
			if (dcs.attacking)
				anim.SetBool ("Dead", true);
		}*/
	}
}
