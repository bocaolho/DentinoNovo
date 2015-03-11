using UnityEngine;
using System.Collections;

public class ParallaxLayer : MonoBehaviour
{
	public float parallaxFactorX, parallaxFactorY;
	
	public void Move(float deltaX, float deltaY)
	{
		Vector3 newPos = transform.localPosition;
		newPos.x -= deltaX * parallaxFactorX;
		newPos.y -= deltaY * parallaxFactorY;

		transform.localPosition = newPos;
	}
	
}