using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObjectLocally : MonoBehaviour
{
	public static float globalMult = 1;
	public float rotationSpeed = 1.0f; // Adjust this value to control the rotation speed.
	public Vector2 yz;
	void Update()
	{
		// Calculate the rotation quaternion.
		Quaternion deltaRotation = Quaternion.Euler(0, rotationSpeed * Time.deltaTime * globalMult * yz.x, rotationSpeed * Time.deltaTime * globalMult * yz.y);

		// Apply the rotation to the object's local rotation.
		transform.localRotation *= deltaRotation;
	}
	
	public static void ChangeGlobalMult(float newMult){
		globalMult = newMult;
	}	
}