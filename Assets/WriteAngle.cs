using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WriteAngle : MonoBehaviour
{
	[SerializeField] ModelView scrpt;
	[SerializeField] Text text;
	private void Update() {
		text.text = scrpt.angle + "";
	}
}
