using UnityEngine;
using System.Collections;

public class EnableBreaklight : MonoBehaviour {

	private MeshRenderer myLight;
	// Use this for initialization
	void Start () {
	
		myLight = GetComponent<MeshRenderer>();
	}
	
	// Update is called once per frame
	
	void Update () {
		//For brake lights
		if(Input.GetKeyUp(KeyCode.Space ))
		{
			myLight.enabled = !myLight.enabled;
		}
	
	}
}
