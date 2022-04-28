using UnityEngine;
using System.Collections;

public class EnableHeadlight : MonoBehaviour {

	private MeshRenderer myLight;
	
	// Use this for initialization
	void Start () {
	
		myLight = GetComponent<MeshRenderer>();
		
	}
	
	// Update is called once per frame
	void Update () {
		//For Headlight glow
		if(Input.GetKeyUp(KeyCode.H ))
		{
			myLight.enabled = !myLight.enabled;
		
		}
	
	}
}
