using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class ParallaxBackground : MonoBehaviour
{
	List<ParallaxLayer> parallaxLayers = new List<ParallaxLayer>();

	void Start()
	{
		gameObject.GetComponent<MainCameraScript>().onCameraTranslate += Move;
		
		SetLayers();
	}
	
	void SetLayers()
	{
		parallaxLayers.Clear();
		
		for (int i = 0; i < transform.childCount; i++)
		{
			ParallaxLayer layer = transform.GetChild(i).GetComponent<ParallaxLayer>();
			
			if (layer != null)
			{
				layer.name = "Layer-" + i;
				parallaxLayers.Add(layer);
			}
		}
	}
	
	
	void Move(float deltaX, float deltaY)
	{
		foreach (ParallaxLayer layer in parallaxLayers)
		{
			layer.Move(deltaX, deltaY);
		}
	}
}