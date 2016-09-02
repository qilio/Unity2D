using UnityEngine;
using System.Collections;

public class ArmRotation : MonoBehaviour 
{
	public float rotationOffset = 0;

	public float zRotation = 1;

	void Update () 
	{
		Vector3 difference = Camera.main.ScreenToWorldPoint (Input.mousePosition) - transform.position;
		difference.Normalize ();

		float rotZ = Mathf.Atan2 (difference.y, difference.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler (0f, 0f, (zRotation)*rotZ + rotationOffset);


	}
}
