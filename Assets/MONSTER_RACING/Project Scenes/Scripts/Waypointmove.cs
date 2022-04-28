using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypointmove : MonoBehaviour
{
    
    [SerializeField] private Waypoints1 Waypoints;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float distanceThreshold = 0.1f;

    private Transform currentWaypoint;

    void Start()
    {
        currentWaypoint = Waypoints.GetNextWaypoint(currentWaypoint);
        transform.position = currentWaypoint.position;

        currentWaypoint = Waypoints.GetNextWaypoint(currentWaypoint);
        transform.LookAt(currentWaypoint);
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, currentWaypoint.position, moveSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, currentWaypoint.position) < distanceThreshold);
        
    }
}
