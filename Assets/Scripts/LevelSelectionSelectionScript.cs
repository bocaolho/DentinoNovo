using UnityEngine;
using System.Collections;

public class LevelSelectionSelectionScript : MonoBehaviour {

	private Vector3 []positions = 
		{ new Vector3(-3.701114f, -2.402275f, -1), 
		new Vector3(-3.541504f, -0.9657828f),
		new Vector3(-3.151345f, 0.1337543f),
		new Vector3(-2.778922f, 1.073681f, -1),
		new Vector3(-2.300091f, 1.978139f, -1),
		new Vector3(-1.537509f, 2.669784f, -1),
		new Vector3(-0.5621127f, 3.095411f, -1),
		new Vector3(0.7679725f, 3.077677f, -1),
		new Vector3(1.725634f, 2.776191f, -1),
		new Vector3(2.523685f, 2.049077f, -1),
		new Vector3(3.020251f, 1.126884f, -1),
		new Vector3(3.321737f, 0.1692228f, -1),
		new Vector3(3.587754f, -1.018987f, -1),
		new Vector3(3.836036f, -2.384541f, -1) };
	public Sprite[] images;
	public int toothIndex;
	public int archIndex;
	public SpriteRenderer archRenderer;
	private bool canChange;

	// Use this for initialization
	void Start () {
		toothIndex = 0;
		archIndex = 0;
		udpateImage();
		canChange = true;
	}
	
	// Update is called once per frame
	void Update () {
		foreach (Touch t in Input.touches) {
			if (t.phase == TouchPhase.Began) {
				if (t.position.x >= Screen.width/2) {
					//updatePos(+1);
				}
				if (t.position.x < Screen.width/4) {
					if (t.position.y > Screen.height/4) {
						Application.LoadLevel ("MainMenu");
					} else {
						//updatePos(-1);
					}
				}
			}
		}
		if (Input.GetMouseButtonDown(0)) {
			if (Input.mousePosition.x >= Screen.width/2) {
				//updatePos(+1);
			}
			if (Input.mousePosition.x < Screen.width/4) {
				if (Input.mousePosition.y > Screen.height/4) {
					Application.LoadLevel ("MainMenu");
				} else {
					//updatePos(-1);
				}
			}
		}
	}

	void udpateImage() {
		archRenderer.sprite = images[archIndex];
	}

	void updatePos(int delta) {
		if (canChange) {
			canChange = false;
			toothIndex += delta;
			if (toothIndex < 0)
				toothIndex = 0;
			if (toothIndex > 13)
				toothIndex = 13;
			transform.position = positions[toothIndex];
			Invoke ("setCanChange", 0.15f);
			//transform.rotation = rotations[toothIndex];
		}
	}

	public void updatePosTo(int dest) {
		if (canChange) {
			canChange = false;
			toothIndex = dest;
			if (toothIndex < 0)
				toothIndex = 0;
			if (toothIndex > 13)
				toothIndex = 13;
			transform.position = positions[toothIndex];
			Invoke ("setCanChange", 0.15f);
			//transform.rotation = rotations[toothIndex];
		}
	}

	void setCanChange() {
		canChange = true;
	}
}
