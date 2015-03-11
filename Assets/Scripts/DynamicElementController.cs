using UnityEngine;
using System.Collections;

public class DynamicElementController : MonoBehaviour {

	// Back delegate
	public delegate bool BackEvent();
	public event BackEvent OnBack;

	// Settings
	public bool backable;
	public Vector3 backLocation;
	public Quaternion backRotation;

	// Use this for initialization
	void Start () {
		backLocation = transform.position;
		backRotation = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// Go Back (if backable)
	public void goBack () {
		if (backable) {
			if (OnBack == null || OnBack()) {
				transform.position = backLocation;
				transform.rotation = backRotation;
			}
		}
	}
}
