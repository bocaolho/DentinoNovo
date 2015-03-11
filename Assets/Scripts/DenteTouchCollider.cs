using UnityEngine;
using System.Collections;

public class DenteTouchCollider : MonoBehaviour {

	// Which teeth to change to
	public int toothIndex;
	// Selection script
	public LevelSelectionSelectionScript levelSelectionSelectionScript;

	private Collider2D collider2D;

	// Use this for initialization
	void Start () {
		collider2D = GetComponent<CircleCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
		foreach (Touch t in Input.touches) {
			Vector3 wp = Camera.main.ScreenToWorldPoint(t.position);
			Vector2 touchPos = new Vector2(wp.x, wp.y);
			if (collider2D == Physics2D.OverlapPoint(touchPos))
			{
				levelSelectionSelectionScript.updatePosTo (toothIndex);
			}
		}
		if (Input.GetMouseButtonDown(0)) {
			Vector3 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector2 touchPos = new Vector2(wp.x, wp.y);
			if (collider2D == Physics2D.OverlapPoint(touchPos))
			{
				levelSelectionSelectionScript.updatePosTo (toothIndex);
			}
		}
	}
}
