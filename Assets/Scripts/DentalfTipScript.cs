using UnityEngine;
using System.Collections;

public class DentalfTipScript : MonoBehaviour {

	public string tipText;
	public float time;
	public bool MultipleTimes;
	public bool hideWhenLeft;
	public GUICameraScript guiCamera;

	void Start() {
		tipText = tipText.Replace("/n","\n");
	}

	void OnTriggerEnter2D (Collider2D coll) {
		if (guiCamera != null) {
			if (coll.gameObject.tag == "Player") {
				guiCamera.DentalfTip (tipText, time);
				if (!MultipleTimes) 
					Destroy (gameObject);
			}
		}
	}

	void OnTriggerExit2D (Collider2D coll) {
		if (guiCamera != null && hideWhenLeft) {
			if (coll.gameObject.tag == "Player") {
				guiCamera.DentalfTipRemove (tipText);
			}
		}
	}
}
