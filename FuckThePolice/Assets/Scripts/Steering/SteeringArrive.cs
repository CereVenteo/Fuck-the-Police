﻿using UnityEngine;
using System.Collections;

// TO SLOW ARRIVE
public class SteeringArrive : SteeringAbstract
{

	public float min_distance = 2f;
	public float slow_distance = 5.0f;
	public float time_to_target = 0.1f;

	Move move;

	// Use this for initialization
	void Start () { 
		move = GetComponent<Move>();
	}

	// Update is called once per frame
	void Update () 
	{
		Steer(move.target.transform.position);
	}

	public bool Steer(Vector3 target)
	{
		if(!move)
			move = GetComponent<Move>();
        else
        {
            // Velocity we are trying to match
            float ideal_speed = 0.0f;
            Vector3 diff = target - transform.position;

            if (diff.magnitude < min_distance)
            {
                move.current_velocity = Vector3.zero;
                return true;
            }

            // Decide which would be our ideal velocity
            if (diff.magnitude > slow_distance)
                ideal_speed = move.max_mov_speed;
            else
                ideal_speed = move.max_mov_speed * (diff.magnitude / slow_distance);

            // Create a vector that describes the ideal velocity
            Vector3 ideal_movement = diff.normalized * ideal_speed;

            // Calculate acceleration needed to match that velocity
            Vector3 acceleration = ideal_movement - move.current_velocity;
            acceleration /= time_to_target;

            // Cap acceleration
            if (acceleration.magnitude > move.max_mov_acceleration)
            {
                acceleration = acceleration.normalized * move.max_mov_acceleration;
            }
            acceleration.y = 0;
            move.AccelerateMovement(acceleration, priority);
        }
		
        return false;
	}

	void OnDrawGizmosSelected() 
	{
		// Display the explosion radius when selected
		Gizmos.color = Color.white;
		Gizmos.DrawWireSphere(transform.position, min_distance);

		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(transform.position, slow_distance);
	}
}
