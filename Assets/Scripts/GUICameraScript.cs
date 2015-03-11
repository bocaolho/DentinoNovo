using UnityEngine;
using System.Collections;

public class GUICameraScript : MonoBehaviour {

	private GUIText dentalfTipText;
	public Font dentalfTipTextFont;
	public string DentalfTipText = "Consejos de Dentalf: \n";//"Dica do Dentalf: \n";

	// Use this for initialization
	void Start () {
		GameObject go = new GameObject("GUIText dentalfTipText");
		dentalfTipText = (GUIText)go.AddComponent(typeof(GUIText));
		dentalfTipText.color = Color.white;
		dentalfTipText.font = dentalfTipTextFont;
		dentalfTipText.fontSize = 32;
		dentalfTipText.transform.position = new Vector3(0.2f, 0.96f, 1.0f);//new Vector2(0.5f, 0.5f);
		dentalfTipText.text = "";
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void DentalfTip(string tip, float time) {
		dentalfTipText.text = DentalfTipText + tip;
		CancelInvoke ("DentalfTipHide");
		Invoke ("DentalfTipHide", time);
	}

	public void DentalfTipRemove(string tip) {
		if (dentalfTipText.text == DentalfTipText + tip)
			DentalfTipHide ();
	}

	public void DentalfTipHide() {
		dentalfTipText.text = "";
	}

}
