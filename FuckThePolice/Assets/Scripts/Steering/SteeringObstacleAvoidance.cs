﻿using UnityEngine;
using System.Collections;

[System.Serializable]
public class my_ray
{
    public float length = 2.0f;
    public Vector3 direction = Vector3.forward;
}

// TO NOT COLLISION WITH OBJECTS
public class SteeringObstacleAvoidance : SteeringAbstract
{

    public LayerMask mask;
    public float avoid_distance = 5.0f;
    public my_ray[] rays;

    Move move;
    SteeringSeek seek;

    // Use this for initialization
    void Start () {
        move = GetComponent<Move>(); 
        seek = GetComponent<SteeringSeek>();
    }
    
    // Update is called once per frame
    void Update () 
    {
        if(!move)
            move = GetComponent<Move>();
        else
        {

            float angle = Mathf.Atan2(move.current_velocity.x, move.current_velocity.z);
            Quaternion q = Quaternion.AngleAxis(Mathf.Rad2Deg * angle, Vector3.up);

            foreach (my_ray ray in rays)
            {
                RaycastHit hit;

                if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), q * ray.direction.normalized, out hit, ray.length, mask) == true)
                    move.AccelerateMovement(new Vector3(transform.position.x, 0, transform.position.z) + hit.normal * avoid_distance, priority);
            }
        }
        
    }

    void OnDrawGizmosSelected() 
    {
        if(move && this.isActiveAndEnabled)
        {
            // Display the explosion radius when selected
            Gizmos.color = Color.red;
            float angle = Mathf.Atan2(move.current_velocity.x, move.current_velocity.z);
            Quaternion q = Quaternion.AngleAxis(Mathf.Rad2Deg * angle, Vector3.up);

            foreach(my_ray ray in rays)
                Gizmos.DrawLine(transform.position, transform.position + (q * ray.direction.normalized) * ray.length);
        }
    }
}

