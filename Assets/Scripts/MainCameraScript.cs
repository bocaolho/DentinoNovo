﻿using UnityEngine;
using System.Collections;

public class MainCameraScript : MonoBehaviour {

	public float dampTime = 0.1f;
	private Vector3 velocity = Vector3.zero;
	public Transform target;
	public Vector2 pivot;

	public delegate void OnCameraTranslate(float deltaX, float deltaY);
	public event OnCameraTranslate onCameraTranslate;

	// Initialize
	void Start () {
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (target)
		{
			Vector3 point = camera.WorldToViewportPoint(target.position);
			Vector3 delta = target.position - camera.ViewportToWorldPoint(new Vector3(pivot.x, pivot.y, point.z));
			Vector3 destination = transform.position + delta;
			transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);

			onCameraTranslate (delta.x, delta.y);
		}
		
	}
}
