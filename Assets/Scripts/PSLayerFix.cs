using UnityEngine;
using System.Collections;

public class PSLayerFix : MonoBehaviour {

	public string SortingLayerName;
	public int SortingOrder;

	// Use this for initialization
	void Start () {
		GetComponent<ParticleSystem>().renderer.sortingLayerName = SortingLayerName;
		GetComponent<ParticleSystem>().renderer.sortingOrder = SortingOrder;
	}

}
