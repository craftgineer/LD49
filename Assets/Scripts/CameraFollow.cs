using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public static CameraFollow camera;
	public Transform target;
    public float yOffset;

	void Awake()
	{
		if (camera == null) {
			DontDestroyOnLoad (gameObject);
			camera = this;
		} else if(camera != this)
		{
			Destroy (gameObject);
		}
	}

	void Update()
	{
		float newX = target.transform.position.x;
		transform.position = new Vector3 (newX, yOffset, transform.position.z);
	}
}
