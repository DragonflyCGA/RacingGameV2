using UnityEngine;
using System.Collections;

public class EnableSound : MonoBehaviour {

	public AudioSource pSound; 
	// Use this for initialization
	void Start () {
		pSound = GetComponent<AudioSource> ();
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyUp(KeyCode.P))
		{
			pSound.enabled = !pSound.enabled;
	}
  }
}