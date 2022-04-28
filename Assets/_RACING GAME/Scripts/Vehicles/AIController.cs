using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public Circuit circuit;
    public float brakingSensitivity = 3f;
    Drive ds; //calling the drive script
    //CarScript_DGF ds;//calling the my driveScript_DGF script
    public float steeringSensitivity = 0.01f;
    public float accelSensitivity = 0.3f;
    Vector3 target;
    Vector3 nextTarget;
    int currentWP = 0;

    float totalDistanceToTarget;

    //These three variables are neede it to define the tracker
    GameObject tracker;
    int currentTrackerWP = 0;
    public float lookAhead = 20;//Distance of the capsule from the car

    float lastTimeMoving = 0; //variable to track last position on the track base on waypoints


    // Start is called before the first frame update
    void Start()
    {
        ds= this.GetComponent<Drive>();
        target = circuit.waypoints[currentWP].transform.position;
        nextTarget = circuit.waypoints[currentWP + 1].transform.position;
        totalDistanceToTarget = Vector3.Distance(target, ds.rb.gameObject.transform.position);

        tracker = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        DestroyImmediate(tracker.GetComponent<Collider>());
        //tracker.GetComponent<MeshRenderer>().enabled = false; //to stop showing the capsule
        tracker.transform.position = ds.rb.gameObject.transform.position;
        tracker.transform.rotation = ds.rb.gameObject.transform.rotation;

        this.GetComponent<Ghost>().enabled = false;
    }

    void ProgressTracker()
    {
        Debug.DrawLine(ds.rb.gameObject.transform.position, tracker.transform.position);
        if (Vector3.Distance(ds.rb.gameObject.transform.position, tracker.transform.position) > lookAhead) return;

        tracker.transform.LookAt(circuit.waypoints[currentTrackerWP].transform.position);
        tracker.transform.Translate(0, 0, 1.0f); // SPEED OF TRACKER

        if (Vector3.Distance(tracker.transform.position, circuit.waypoints[currentTrackerWP].transform.position) < 2)//INCREASE IF IT GOES IN CIRCLES
        {
            currentTrackerWP++;
            if(currentTrackerWP >= circuit.waypoints.Length)
            {
                currentTrackerWP = 0;
            }


        }
    }

    void ResetLayer()
    {
        ds.rb.gameObject.layer = 0;
        this.GetComponent<Ghost>().enabled = false;
    }

    // Update is called once per frame
        void Update()
    {
        if (!RaceMonitor.racing)
        {
            lastTimeMoving = Time.time;
            return;
        }

        ProgressTracker();
        Vector3 localTarget;
        float targetAngle;

        //If statement to control the unstock process and reset the car to a waypoint
        if(ds.rb.velocity.magnitude > 1)
           lastTimeMoving = Time.time;

        //If statement to control the respawn of the car
        if (Time.time > lastTimeMoving + 4)//Setup at 4 second. The flic car was setup at 3 seconds
        {
            ds.rb.gameObject.transform.position = circuit.waypoints[currentTrackerWP].transform.position + Vector3.up * 2 
                + new Vector3(Random.Range(-1,1), 0, Random.Range(-1, 1));//Multiply by two to locate it 2 mts above the ground by multiplying Vector3.up * 2
            tracker.transform.position = ds.rb.gameObject.transform.position;
            ds.rb.gameObject.layer = 8;
            this.GetComponent<Ghost>().enabled = true;
            Invoke("ResetLayer", 3);
        }

        //If statement to control the avoide process between the cars
        if (Time.time < ds.rb.GetComponent<AvoidDetector>().avoidTime)
        {
            localTarget = tracker.transform.right * ds.rb.GetComponent<AvoidDetector>().avoidPath;

        }
        else
        {
            localTarget = ds.rb.gameObject.transform.InverseTransformPoint(tracker.transform.position);
        }
        targetAngle = Mathf.Atan2(localTarget.x, localTarget.z) * Mathf.Rad2Deg;
        
        float steer = Mathf.Clamp(targetAngle * steeringSensitivity, -1, 1) * Mathf.Sign(ds.currentSpeed);
        float speedFactor = ds.currentSpeed / ds.maxSpeed;
        float corner = Mathf.Clamp(Mathf.Abs(targetAngle), 0, 90);
        float cornerFactor = corner / 90.0f;

        float brake = 0;
        if(corner > 10 && speedFactor > 0.1f)
        {
            brake = Mathf.Lerp(0, 1 + speedFactor * brakingSensitivity, cornerFactor);
        }
        
        float accel = 0.5f; //Maximum speed if no corners higher than 20 degrees
        if(corner > 20 && speedFactor > 0.2f)
        {
            accel = Mathf.Lerp(0, 1 * accelSensitivity, 1 - cornerFactor);
        }

        //float distanceFactor = distanceToTarget / totalDistanceToTarget;
        

        //float accel = Mathf.Lerp(accelerationSencitivity, 0.5f, distanceFactor);// ACELERATION - increase if need more power
       //float brake = Mathf.Lerp((-1 - Mathf.Abs(nextTargetAngle)) * brakingSensitivity, 1 + speedFactor, 1 - distanceFactor);

        /*if(Mathf.Abs(nextTargetAngle) > 20)
        {
            brake += 0.8f;
            accel -= 0.1f;
        }

        if (isJump)
        {
            accel = 0.5f;
            brake = 0;

        }*/

        //Debug.Log("Brake: " + brake + "Acce" + accel);

        //if(distanceToTarget < 5) { brake = 0.8f; accel = 0.1f; }
        ds.Go(accel, steer, brake);
        
        /*
        if (distanceToTarget < 8)//Increase if car start to cicle waypoints
        {
            currentWP++;
            if(currentWP>= circuit.waypoints.Length)
            {
                currentWP = 0;
            }
            target = circuit.waypoints[currentWP].transform.position;
            
            if(currentWP == circuit.waypoints.Length - 1)
                nextTarget = circuit.waypoints[0].transform.position;
            else 
                nextTarget = circuit.waypoints[currentWP + 1].transform.position;
            
            totalDistanceToTarget = Vector3.Distance(target, ds.rb.gameObject.transform.position);

            if (ds.rb.gameObject.transform.InverseTransformPoint(target).y > 5)
            {
                isJump = true;
            }
            else isJump = false;
        }*/
        ds.CheckForSkid();
        ds.CalculateEngineSound();

    }
}
