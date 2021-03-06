using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarScript_DGF : MonoBehaviour
{

    public WheelCollider[] WCs;
    public GameObject[] Wheels;

    public GameObject brakeLight;

    public Transform t_CenterOfMass;
    public float maxSteerAngle = 30;
    public float maxBrakeTorque = 500;
    public AudioSource highAccel;


    public float torque = 200;

    private Rigidbody r_Rigidbody;

    public Rigidbody rb;
    public float gearLength = 3;
    public float currentSpeed { get { return rb.velocity.magnitude * gearLength; } }
    public float lowPitch = 1f;
    public float highPitch = 6f;
    public int numGears = 5;
    float rpm;
    int currentGear = 1;
    float currentGearPerc;
    public float maxSpeed = 200;

    public Transform skidTrailPrefab;
    Transform[] skidTrails = new Transform[4];

    public ParticleSystem smokePrefab;
    ParticleSystem[] skidSmoke = new ParticleSystem[4];

    public void StartSkidTrail(int i)
    {

        if (skidTrails[i] == null)
        {

            skidTrails[i] = Instantiate(skidTrailPrefab);
        }

        skidTrails[i].parent = WCs[i].transform;
        skidTrails[i].localPosition = -Vector3.up * WCs[i].radius;
    }




    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            skidSmoke[i] = Instantiate(smokePrefab);
            skidSmoke[i].Stop();
        }
        r_Rigidbody = GetComponent<Rigidbody>();
        r_Rigidbody.centerOfMass = t_CenterOfMass.localPosition;
        brakeLight.SetActive(false);

    }

    public void CalculateEngineSound()
    {

        float gearPercentage = (1 / (float)numGears);
        float targetGearFactor = Mathf.InverseLerp(gearPercentage * currentGear, gearPercentage * (currentGear + 1),
            Mathf.Abs(currentSpeed / maxSpeed));

        currentGearPerc = Mathf.Lerp(currentGearPerc, targetGearFactor, Time.deltaTime * 5.0f);

        var gearNumFactor = currentGear / (float)numGears;
        rpm = Mathf.Lerp(gearNumFactor, 1, currentGearPerc);

        float speedPercentage = Mathf.Abs(currentSpeed / maxSpeed);
        float upperGearMax = (1 / (float)numGears) * (currentGear + 1);
        float downGearMax = (1 / (float)numGears) * currentGear;

        if (currentGear > 0 && speedPercentage < downGearMax)
        {

            currentGear--;
        }

        if (speedPercentage > upperGearMax && (currentGear < (numGears - 1)))
        {

            currentGear++;
        }

        float pitch = Mathf.Lerp(lowPitch, highPitch, rpm);
        highAccel.pitch = Mathf.Min(highPitch, pitch) * 0.25f;

    }


    public void CheckForSkid()
    {

        int numSkidding = 0;
        for (int i = 0; i < 4; ++i)
        {

            WheelHit wheelHit;
            WCs[i].GetGroundHit(out wheelHit);

            if (Mathf.Abs(wheelHit.forwardSlip) >= 0.4f || Mathf.Abs(wheelHit.sidewaysSlip) >= 0.4f)
            {

                numSkidding++;

                // StartSkidTrail(i);
                skidSmoke[i].transform.position = WCs[i].transform.position - WCs[i].transform.up * WCs[i].radius;
                skidSmoke[i].Emit(1);
            }
            else
            {

                //EndSkidTrail(i);
            }
        }


    }

    public void Go(float accel, float steer, float brake) // was not public
    {
        accel = Mathf.Clamp(accel, -1, 1);
        steer = Mathf.Clamp(steer, -1, 1) * maxSteerAngle;
        brake = Mathf.Clamp(brake, 0, 1) * maxBrakeTorque;

        if (brake != 0)
            brakeLight.SetActive(true);
        else
            brakeLight.SetActive(false);

        float thrutTorque = 0;
        if (currentSpeed < maxSpeed)
            thrutTorque = accel * torque;

        for (int i = 0; i < 4; i++)
        {
            WCs[i].motorTorque = thrutTorque;
            WCs[i].brakeTorque = brake;
            if (i < 2)
                WCs[i].steerAngle = steer;
            //else
            //WCs[i].brakeTorque = brake;

            Quaternion quat;
            Vector3 position;
            WCs[i].GetWorldPose(out position, out quat);
            Wheels[i].transform.position = position;
            Wheels[i].transform.localRotation = quat;
        }

    }
    /*
    // Update is called once per frame
    void Update()
    {
        float a = Input.GetAxis("Vertical");
        float s = Input.GetAxis("Horizontal");
        float b = Input.GetAxis("Jump");
        Go(a, s, b);
        CalculateEngineSound();
        CheckForSkid();

    }*/
}
