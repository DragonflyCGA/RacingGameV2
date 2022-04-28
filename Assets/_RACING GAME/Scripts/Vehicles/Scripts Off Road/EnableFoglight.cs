using UnityEngine;
using System.Collections;

public class EnableFoglight : MonoBehaviour {

	private MeshRenderer myLight;
	// Use this for initialization
	void Start () {
	
		myLight = GetComponent<MeshRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		//For Fog light
		if(Input.GetKeyUp(KeyCode.F ))
		{
			myLight.enabled = !myLight.enabled;
		}
	
	}
}
