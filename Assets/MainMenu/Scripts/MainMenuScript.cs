using UnityEngine;
using System.Collections;

// Attached to MainCamera
public class MainMenuScript : MonoBehaviour {

	// Background
	public Texture background;
	// Logo
	public Texture pet;
	// Dentino animation
	public Texture[] dentinoAnimation;
	private int dentinoAnimationFrameWidth, dentinoAnimationFrameHeight;
	private int dentinoAnimationFrame = 0;
	private float dentinoAnimationTime = 0.0f;
	public float DAMaxTimeBetweenBlinks;
	public float DAMinTimeBetweenBlinks;
	private float dentinoAnimationTimeBetweenBlinks;
	// Fonts
	public Font defaultFont;

	void Start() {
		dentinoAnimationFrameWidth = dentinoAnimation[0].width;
		dentinoAnimationFrameHeight = dentinoAnimation[0].height;
		dentinoAnimationTimeBetweenBlinks = Random.Range(DAMinTimeBetweenBlinks, DAMaxTimeBetweenBlinks);
	}

	void OnGUI() {
		// Background
		GUI.DrawTexture (new Rect(0,0, Screen.width, Screen.height), background, ScaleMode.StretchToFill);

		// Pet
		GUI.DrawTexture (new Rect(
			Screen.width - (Screen.width / 4), 
			Screen.height - (Screen.height / 4), 
			(Screen.width / 4), 
			(Screen.height / 4)), pet, ScaleMode.StretchToFill);

		// Dentino
		GUI.DrawTexture (new Rect(Screen.width/8, Screen.height/3, dentinoAnimationFrameWidth, dentinoAnimationFrameHeight), dentinoAnimation[dentinoAnimationFrame]);
	
		// Buttons
		GUIStyle defaultStyle = new GUIStyle();
		defaultStyle.font = defaultFont;
		defaultStyle.normal.textColor = Color.white;
		defaultStyle.fontSize = 40;
		defaultStyle.alignment = TextAnchor.MiddleCenter;
		defaultStyle.hover.textColor = Color.white;

		if (GUI.Button (new Rect(Screen.width/2,Screen.height/2 - (Screen.height/10),Screen.width/2.4f, Screen.height/5), "")) {
			Application.LoadLevel("LevelSelection");
		}
		GUI.Button (new Rect(Screen.width/2,Screen.height/2 - (Screen.height/10),Screen.width/2.4f, Screen.height/5), "Novo Jogo", defaultStyle);
		if (GUI.Button (new Rect(Screen.width/2,Screen.height/2 + (Screen.height/10),Screen.width/2.4f, Screen.height/5), "")) {
			Application.LoadLevel("Tutorial");
		}
		GUI.Button (new Rect(Screen.width/2,Screen.height/2 + (Screen.height/10),Screen.width/2.4f, Screen.height/5), "Tutorial", defaultStyle);
	}

	void Update() {
		// Animate dentino
		dentinoAnimationTime += 0.1f;
		if (dentinoAnimationTime >= dentinoAnimationTimeBetweenBlinks) {
			if (dentinoAnimationTime >= dentinoAnimationTimeBetweenBlinks + 0.4f)
				dentinoAnimationFrame = 1;
			if (dentinoAnimationTime >= dentinoAnimationTimeBetweenBlinks + 0.8f)
				dentinoAnimationFrame = 2;
			if (dentinoAnimationTime >= dentinoAnimationTimeBetweenBlinks + 1.2f)
				dentinoAnimationFrame = 1;
			if (dentinoAnimationTime >= dentinoAnimationTimeBetweenBlinks + 1.6f) {
				dentinoAnimationFrame = 0;
				dentinoAnimationTime = 0;
				dentinoAnimationTimeBetweenBlinks = Random.Range(DAMinTimeBetweenBlinks, DAMaxTimeBetweenBlinks);
			}
		}
	}
}
