using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ALTIAController : MonoBehaviour
{
    public Circuit circuit;
    Vector3 target;
    int currentWP = 0;
    float speed = 20.0f;
    float accuracy = 4.0f;
    float rotSpeed = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        target = circuit.waypoints[currentWP].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToTarget = Vector3.Distance(target, this.transform.position);
        Vector3 direction = target - this.transform.position;
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotSpeed);

        this.transform.Translate(0, 0, speed * Time.deltaTime);

        if (distanceToTarget < accuracy)
        {
            currentWP++;
            if (currentWP >= circuit.waypoints.Length)
                currentWP = 0;
            target = circuit.waypoints[currentWP].transform.position;
        }
    }
}
