using UnityEngine;
using System.Collections;

public class EnableScriptLeft : MonoBehaviour {

	private EnableMarker myScript;
	private MeshRenderer myLight;
	// Use this for initialization
	void Start () {
	
		myScript = GetComponent<EnableMarker>();
		myLight = GetComponent<MeshRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		//For left signal
		if(Input.GetKeyUp(KeyCode.Q ))
		{
			myScript.enabled = !myScript.enabled;
			myLight.enabled = false;
		}
		//For double signal 
		if(Input.GetKeyUp(KeyCode.T ))
		{
			myScript.enabled = !myScript.enabled;
			myLight.enabled = false;
		}
	
	}
}