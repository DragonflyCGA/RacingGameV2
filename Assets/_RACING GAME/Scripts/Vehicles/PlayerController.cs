using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Drive ds;//Calling the drive script
    //CarScript_DGF ds;//calling the my driveScript_DGF script

    //Variables to track the location of the player car to respawn when stock
    float lastTimeMoving = 0;
    Vector3 lastPosition;
    Quaternion lastRotation;

    void ResetLayer()
    {
        ds.rb.gameObject.layer = 0;
        this.GetComponent<Ghost>().enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        ds = this.GetComponent <Drive> (); //Initiating the Drive script
        this.GetComponent<Ghost>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!RaceMonitor.racing) return;

        float a = Input.GetAxis("Vertical");
        float s = Input.GetAxis("Horizontal");
        float b = Input.GetAxis("Jump");

        //check if the vehicle is moving
        if (ds.rb.velocity.magnitude > 1 || !RaceMonitor.racing)
            lastTimeMoving = Time.time;
        
        //Three if statement to respawn the car***********************
        RaycastHit hit;
        if (Physics.Raycast(ds.rb.gameObject.transform.position, -Vector3.up, out hit, 10))
        {
            if (hit.collider.gameObject.tag == "road")
            {
                //Debug.Log("off road");
                lastPosition = ds.rb.gameObject.transform.position;
                lastRotation = ds.rb.gameObject.transform.rotation;

            }
        }
        if (Time.time > lastTimeMoving + 4)
        {
            ds.rb.gameObject.transform.position = lastPosition;
            ds.rb.gameObject.transform.rotation = lastRotation;
            ds.rb.gameObject.layer = 8;
            this.GetComponent<Ghost>().enabled = true;
            Invoke("ResetLayer", 3);
        }//***************************************************************
             
        if (!RaceMonitor.racing) a = 0;//This line stops the player from starting before the countdown. Aceleration = 0

        ds.Go(a, s, b);
        ds.CheckForSkid();
        ds.CalculateEngineSound();
    }
}
