﻿using UnityEngine;
using System.Collections;

// TO MOVE WITHOUT PROGRESSION STOP
public class SteeringSeek : SteeringAbstract
{

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

	public void Steer(Vector3 target)
	{
        if (!move)
            move = GetComponent<Move>();
        else
        {
            Vector3 diff = target - transform.position;
            diff.Normalize();
            diff *= move.max_mov_acceleration;
            diff.y = 0;
            move.AccelerateMovement(diff, priority);
        }
        
	}
}
