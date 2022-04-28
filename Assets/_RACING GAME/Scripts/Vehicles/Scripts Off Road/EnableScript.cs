using UnityEngine;
using System.Collections;

public class EnableScript : MonoBehaviour {

	private EnableLights myScript;
	private MeshRenderer myLight;
	// Use this for initialization
	void Start () {
	
		myScript = GetComponent<EnableLights>();
		myLight = GetComponent<MeshRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyUp(KeyCode.P ))
		{
			myScript.enabled = !myScript.enabled;
			myLight.enabled = false;
		}
	
	}
}
