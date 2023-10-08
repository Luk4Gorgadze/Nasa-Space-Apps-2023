using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelView : MonoBehaviour
{
	[SerializeField] Transform[] children;

	Quaternion correctRotation;
	[SerializeField] Camera cam;
	[SerializeField] float scaler;
	[SerializeField] Vector2 range;
	[SerializeField] Material[] materials;
	[SerializeField] ParticleSystem winParticle;
	
	[SerializeField] Vector3 posMult;
	[SerializeField] Vector3 posMultInside;
	[SerializeField] AnimationCurve scaleCurve;
	[SerializeField] float animationTime;
	[SerializeField] float scaleTime;
	[SerializeField] Vector3 rotSpeed;
	Vector3[] randomArray;
	Quaternion[] startRots;
	
	Vector3[] startPositions;
	
	bool isCompleted = false;
	public float angle;
	public bool isBuildable;
	void Start()
	{
		Vector3 dir = new Vector3(Random.Range(range.x, range.y), Random.Range(range.x, range.y) * 0.2f, Random.Range(range.x, range.y));
		correctRotation = Quaternion.Euler(-dir.normalized * scaler);
		randomArray = new Vector3[children.Length];
		startRots = new Quaternion[children.Length];
		startPositions = new Vector3[children.Length];
		for (int i = 0; i < children.Length; i++)
		{
			startRots[i] = children[i].localRotation;
			randomArray[i] = new Vector3(Random.Range(range.x, range.y), Random.Range(range.x, range.y), Random.Range(range.x, range.y));
			int randy = Random.Range(0,materials.Length);
			children[i].GetComponent<MeshRenderer>().material = materials[randy];
			startPositions[i] = children[i].localPosition;
		}
		
		Vector3 dir1 = transform.position - cam.transform.position;
		
		GoShuffle(dir1);
		
	}
	
	void Update()
	{
		RotateAfterWin();
		Vector3 dir = transform.position - cam.transform.position;
		if (dir.magnitude > 1) return;
		
		GoShuffle(dir);
		
		

	}
	
	void GoShuffle(Vector3 dir)
	{
		Quaternion currentRotation = Quaternion.Euler(-dir.normalized * scaler);
		
		angle = Quaternion.Angle(currentRotation, correctRotation) / 180f;
		
		if (transform.rotation != correctRotation && !isCompleted)
		{
			for(int i = 0; i < children.Length; i++)
			{
				children[i].localRotation = startRots[i] * Quaternion.Inverse(currentRotation * Quaternion.Euler(0,angle * randomArray[i].x, 0)) * correctRotation;

				children[i].localPosition =  new Vector3(angle * randomArray[i].x * posMultInside.x * posMult.x, angle * randomArray[i].y * posMultInside.y * posMult.y ,0);
				children[i].localPosition += startPositions[i];
			}
		}
		
		if (angle < 0.06f && !isCompleted)
		{
			Reset();
		}
	}
	
	IEnumerator goToStartRotation()
	{
		float startTime = Time.time;
		float t = 0;
		while (t < 1)
		{
			t = (Time.time - startTime) / animationTime;
			if (t > 1)
			{
				t = 1;
			}
			for (int i = 0; i < children.Length; i++)
			{
				children[i].localRotation = Quaternion.Lerp(children[i].localRotation, startRots[i], t);
				children[i].localPosition = Vector3.Lerp(children[i].localPosition, startPositions[i], t);
			}
			yield return null;
		}
	}
	
	IEnumerator bounceScaleWithCurve()
	{
		float startTime = Time.time;
		float t = 0;
		while (t < 1)
		{
			t = (Time.time - startTime) / scaleTime;
			if (t > 1)
			{
				t = 1;
			}
			transform.localScale = Vector3.one * scaleCurve.Evaluate(t);
			yield return null;
		}
	}
	
	public void Reset()
	{
		isCompleted = true;
		StartCoroutine(goToStartRotation());
		StartCoroutine(bounceScaleWithCurve());
		// winParticle.gameObject.SetActive(true);
		winParticle.Play();
	}
	
	public void RotateAfterWin()
	{
		if (!isCompleted) return;
		float xRotation = rotSpeed.x * Time.deltaTime;
        float yRotation = rotSpeed.y * Time.deltaTime;
        float zRotation = rotSpeed.z * Time.deltaTime;

        transform.Rotate(Vector3.right * xRotation);
        transform.Rotate(Vector3.up * yRotation);
        transform.Rotate(Vector3.forward * zRotation);
	}
}
