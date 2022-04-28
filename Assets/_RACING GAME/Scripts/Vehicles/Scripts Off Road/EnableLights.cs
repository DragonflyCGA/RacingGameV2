using UnityEngine;
using System.Collections;

public class EnableLights : MonoBehaviour {
public float timer;
	private MeshRenderer myLight;
	// Use this for initialization
	void Start () {
	
		myLight = GetComponent<MeshRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;

     if (timer >= 0.5)
     {
        myLight.enabled = true; 
     }
        if (timer >= 1)
        {
            myLight.enabled = false;
            timer = 0;
        }

	}
}
