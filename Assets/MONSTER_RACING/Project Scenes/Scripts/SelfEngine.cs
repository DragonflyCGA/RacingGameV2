using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfEngine : MonoBehaviour
{
    public Transform Waypoints;
    public float maxSteerAngle = 45f;
    public WheelCollider wheelFl;
    public WheelCollider wheelFR;
    public WheelCollider wheelRl;
    public WheelCollider wheelRR;
    public float maxMotorTorque = 80f;
    public float currenrtSpeed;
    public float maxSpeed = 100f;

    private List<Transform> nodes;
    private int currentNode = 0;


    private void Start() {
        Transform[] pathTransforms = GetComponentsInChildren<Transform>();
        nodes = new List<Transform>();

        for(int i = 0; i < pathTransforms.Length; i++) {
            if (pathTransforms[i] != transform){
                nodes.Add(pathTransforms[i]);
            }
        }
    }

  private void FixedUpdate () {
      ApplySteer();
      Drive();
      CheckWaypointDistance();
  }
    private void ApplySteer () {
        Vector3 relativeVector  = transform.InverseTransformPoint(nodes[currentNode].position);
        float newSteer = (relativeVector.x / relativeVector.magnitude) * maxSteerAngle;
        wheelFl.steerAngle = newSteer;
        wheelFR.steerAngle = newSteer;
        wheelRl.steerAngle = newSteer;
        wheelRR.steerAngle = newSteer;
    }
    private void Drive () {
        currenrtSpeed = 2 * Mathf.PI * wheelFl.radius * wheelFR.radius * wheelRl.rpm * wheelRR.rpm * 60 / 1000;
        if(currenrtSpeed < maxSpeed){
        wheelFl.motorTorque = maxMotorTorque;
        wheelFR.motorTorque = maxMotorTorque;
        wheelRl.motorTorque = maxMotorTorque;
        wheelRR.motorTorque = maxMotorTorque;
        } else{
        wheelFl.motorTorque = 0;
        wheelFR.motorTorque = 0;
        wheelRl.motorTorque = 0;
        wheelRR.motorTorque = 0;
        }
    }
    private void CheckWaypointDistance () {
        if(Vector3.Distance(transform.position, nodes[currentNode].position)< 0.5f){
            if(currentNode == nodes.Count - 1){
                currentNode = 0;
            }else{
                currentNode++;
            }
        }
    }
}
