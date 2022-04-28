using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLeft : MonoBehaviour
{
    public Animator Mydoor;
        
        
    
    // Start is called before the first frame update
    void Start()
    {
       Mydoor = GetComponent<Animator>(); 
    }

    // Update is called once per frame
    void Update()
    {
        //For openeing the door
        if(Input.GetKeyUp(KeyCode.O ))
		{
			Mydoor.Play("OpenDoor");
		}
        
        //For closing the door
         if(Input.GetKeyUp(KeyCode.C ))
		{
			Mydoor.Play("CloseDoor");
		}
    }
}
