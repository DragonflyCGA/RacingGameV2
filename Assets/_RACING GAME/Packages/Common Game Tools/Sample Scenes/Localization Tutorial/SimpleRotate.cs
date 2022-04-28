using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LocalizationTutorial
{
	public class SimpleRotate : MonoBehaviour
	{
		public float speed = 0.5f;

		void Update()
		{
			transform.Rotate(0, speed, 0, Space.World);
		}
	}
}