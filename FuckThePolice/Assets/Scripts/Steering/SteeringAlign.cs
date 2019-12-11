using UnityEngine;
using System.Collections;

// TO ROTATIONS
public class SteeringAlign : SteeringAbstract
{

    public float min_angle = 5.0f;
    public float slow_angle = 10.0f;
    public float time_to_accel = 2.0f;

    Move move;

    // Use this for initialization
    void Start()
    {
        move = GetComponent<Move>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (move.target != null)
        //    Steer(move.target.transform.position);
    }
    public bool Steer(Vector3 target)
    {
        //    if (!move)
        //        move = GetComponent<Move>();
        //    else
        //    {
        //        float delta_angle = Vector3.SignedAngle(transform.forward, move.GetPriorityVelocity(), new Vector3(0.0f, 1.0f, 0.0f));

        //        float diff_absolute = Mathf.Abs(delta_angle);

        //        if (diff_absolute < min_angle)
        //        {
        //            move.SetRotationVelocity(0.0f);
        //            return;
        //        }

        //        float ideal_rotation_speed = move.max_rot_speed;

        //        if (diff_absolute < slow_angle)
        //            ideal_rotation_speed *= (diff_absolute / slow_angle);

        //        float angular_acceleration = ideal_rotation_speed / time_to_accel;

        //        //Invert rotation direction if the angle is negative
        //        if (delta_angle < 0)
        //            angular_acceleration = -angular_acceleration;

        //        move.AccelerateRotation(Mathf.Clamp(angular_acceleration, -move.max_rot_acceleration, move.max_rot_acceleration), priority);
        //    }

        //}

        float delta_angle = Vector3.SignedAngle(transform.forward, new Vector3(target.x - transform.position.x, 0, target.z - transform.position.z), new Vector3(0.0f, 1.0f, 0.0f));

        float diff_absolute = Mathf.Abs(delta_angle);

        if (diff_absolute < min_angle)
        {
            move.rotation = 0;
            return true;
        }

        float ideal_rotation_speed = move.max_rot_speed;

        if (diff_absolute < slow_angle)
            ideal_rotation_speed *= (diff_absolute / slow_angle);

        float angular_acceleration = ideal_rotation_speed / time_to_accel;

        //Invert rotation direction if the angle is negative
        if (delta_angle < 0)
            angular_acceleration = -angular_acceleration;

        move.AccelerateRotation(/*Mathf.Clamp(*/angular_acceleration/*, -move.max_rot_acceleration, move.max_rot_acceleration)*/, priority);

        return false;

    }
}

