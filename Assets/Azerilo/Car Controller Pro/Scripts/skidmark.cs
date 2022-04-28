using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// We use this class to draw skid marks of the tires
public class skidmark : MonoBehaviour
{
    CarControllerPro acc;       // Reference to the Azerilo car controller class
    AudioSource audioSource;        // Reference to this object audio source
    TrailRenderer trailRenderer;    // Reference to this object trail renderer 
    string objectName;              // The current game object name
    WheelHit wheelHitRR;            // The rear right wheel hit information
    WheelHit wheelHitRL;            // The rear left wheel hit information

    public float sideSkidThreshold = 0.3f;  // After side way slipping exceeds this value we begin to draw skid marks on the road and playing skid sound      

    void Start()
    {
        acc = GetComponentInParent<CarControllerPro>();
        audioSource = GetComponent<AudioSource>();
        trailRenderer = GetComponent<TrailRenderer>();
        objectName = transform.gameObject.name;
    }

    void Update()
    {
        // Checks rear right tire for slipping
        if (objectName == "skidRR")
        {
            acc.Wheel_Collider_Rear_Right.GetGroundHit(out wheelHitRR);

            if (Mathf.Abs(wheelHitRR.sidewaysSlip) > sideSkidThreshold)
            {
                if (!audioSource.isPlaying)
                {
                    audioSource.Play();
                    trailRenderer.emitting = true;
                }
            }
            else
            {
                audioSource.Stop();
                trailRenderer.emitting = false;
            }
        }
        // Checks rear left tire for slipping
        else if (objectName == "skidRL")
        {
            acc.Wheel_Collider_Rear_Left.GetGroundHit(out wheelHitRL);

            if (Mathf.Abs(wheelHitRL.sidewaysSlip) > sideSkidThreshold)
            {
                if (!audioSource.isPlaying)
                {
                    audioSource.Play();
                    trailRenderer.emitting = true;
                }
            }
            else
            {
                audioSource.Stop();
                trailRenderer.emitting = false;
            }
        }
        else
            Debug.Log("The name of game object is not valid!");

    }
}
