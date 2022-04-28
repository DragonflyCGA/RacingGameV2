using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableHeadlamp : MonoBehaviour
{
    private Light myLight;
    // Start is called before the first frame update
    void Start()
    {
        myLight = GetComponent<Light>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //For Head light
        if(Input.GetKeyUp(KeyCode.H ))
		{
			myLight.enabled = !myLight.enabled;
		}
    }
}
