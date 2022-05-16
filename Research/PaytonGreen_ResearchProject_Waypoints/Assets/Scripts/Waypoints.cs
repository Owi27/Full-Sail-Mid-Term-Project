using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Transform> waypoints = new List<Transform>();
    private Transform currWaypoint;
    private int TargetWayPointIndex = 0;
    private float movemetSpeed = 5.0f;
            

    void Start()
    {
        currWaypoint = waypoints[TargetWayPointIndex];
    }

    // Update is called once per frame
    void Update()
    {
        float distanceBetweenPoints = Vector3.Distance(transform.position, currWaypoint.position);

        transform.position = Vector3.MoveTowards(transform.position, currWaypoint.position, movemetSpeed * Time.deltaTime);

        CheckDistance(distanceBetweenPoints);

    }

    void CheckDistance(float distance) 
    {
        if (distance < .1f) 
        {
            TargetWayPointIndex++;
            UpdateTargetWaypoint();
        }
    }

    void UpdateTargetWaypoint() 
    {
        currWaypoint = waypoints[TargetWayPointIndex];

        if (TargetWayPointIndex == 8) 
        {
            TargetWayPointIndex = 0;
            currWaypoint = waypoints[TargetWayPointIndex];

        }
    }
}
