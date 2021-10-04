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
		camera = this;
	}

	void Update()
	{
		float newX = target.transform.position.x;
		transform.position = new Vector3 (newX, target.transform.position.y + yOffset, transform.position.z);
	}
}
