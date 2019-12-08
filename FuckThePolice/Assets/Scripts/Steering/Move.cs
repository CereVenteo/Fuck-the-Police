using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.AI;
using BansheeGz.BGSpline.Components;
using BansheeGz.BGSpline.Curve;

public class Move : MonoBehaviour {
    public Animator anim;
    public GameObject target;
    public GameObject aim;
    public Slider arrow;
    public float max_mov_speed = 5.0f;
    public float max_mov_acceleration = 0.1f;
    public float max_rot_speed = 10.0f; // in degrees / second
    public float max_rot_acceleration = 0.1f; // in degrees

    [Header("-------- Read Only --------")]
    public Vector3 current_velocity = Vector3.zero;
    public float rotation = 0.0f; // degrees

    private Vector3[] movement_velocity;
    private float[] angular_velocity;

    public void Start()
    {
        movement_velocity = new Vector3[SteeringConf.num_priorities];
        angular_velocity = new float[SteeringConf.num_priorities];
        anim = GetComponent<Animator>();

        ResetPriorities();
    }

    // Methods for behaviours to set / add velocities
    public void SetMovementVelocity(Vector3 velocity, int priority)
    {
        movement_velocity[priority] = velocity;
    }

    public void AccelerateMovement(Vector3 velocity, int priority)
    {
        movement_velocity[priority] += velocity;
    }

    public void SetRotationVelocity(float rotation_velocity, int priority)
    {
        angular_velocity[priority] = rotation_velocity;
    }

    public void AccelerateRotation(float rotation_acceleration, int priority)
    {
        angular_velocity[priority] += rotation_acceleration;
    }

    //public Vector3 GetPriorityVelocity()
    //{
    //    Vector3 aux_current_velocity = Vector3.zero;

    //    for (int i = SteeringConf.num_priorities; i >= 0; --i)
    //    {
    //        if (movement_velocity[i] != Vector3.zero)
    //        {
    //            aux_current_velocity = movement_velocity[i];
    //            break;
    //        }
    //    }

    //    return aux_current_velocity;
    //}

    public void ResetPriorities()
    {
        for (int i = 0; i < SteeringConf.num_priorities; ++i)
        {
            movement_velocity[i] = Vector3.zero;
            angular_velocity[i] = 0.0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 new_movement_velocity = Vector3.zero;
        float new_angular_velocity = 0.0f;

        // Pick the lowest priority level
        foreach (Vector3 v in movement_velocity)
        {
            if (Mathf.Approximately(v.magnitude, 0.0f) == false)
            {
                new_movement_velocity = v;
                break;
            }
        }


        foreach (float f in angular_velocity)
        {
            if (Mathf.Approximately(f, 0.0f) == false)
            {
                new_angular_velocity = f;
                break;
            }
        }

        ResetPriorities();

        // Apply
        current_velocity += new_movement_velocity;
        rotation += new_angular_velocity;

        // cap velocity
        if (current_velocity.magnitude > max_mov_speed)
        {
            current_velocity.Normalize();
            current_velocity *= max_mov_speed;
        }

        if (current_velocity.magnitude < 0.1)
        {
            current_velocity = Vector3.zero;
        }

        // cap rotation
        Mathf.Clamp(rotation, -max_rot_speed, max_rot_speed);

        // rotate the arrow
        float angle = Mathf.Atan2(current_velocity.x, current_velocity.z);
        aim.transform.rotation = Quaternion.AngleAxis(Mathf.Rad2Deg * angle, Vector3.up);
        anim.SetFloat("Speed_f", current_velocity.magnitude);
        // strech it
        //arrow.value = current_velocity.magnitude * 4;

        // final rotate
        transform.rotation *= Quaternion.AngleAxis(rotation * Time.deltaTime, Vector3.up);

        // finally move
        transform.position += current_velocity * Time.deltaTime;
    }
    /*public GameObject target;
	public GameObject aim;
	public Slider arrow;
	public float max_mov_speed = 5.0f;
	public float max_mov_acceleration = 0.1f;
	public float max_rot_speed = 300.0f; // in degrees / second
	public float max_rot_acceleration = 100.0f; // in degrees
    public Animator anim;

    [Header("-------- Read Only --------")]
	public Vector3 current_velocity = Vector3.zero;
    
    public float current_rotation_speed = 0.0f; // degrees

    public Vector3[] priorities_velocity;
    public float[] priorities_rotation;

    private void Start()
    {
        anim = GetComponent<Animator>();

        priorities_velocity = new Vector3[SteeringConf.num_priorities + 1];
        for (int i = 0; i < SteeringConf.num_priorities; i++)
        {
            priorities_velocity[i] = Vector3.zero;
        }

        priorities_rotation = new float[SteeringConf.num_priorities + 1];
        for (int i = 0; i < SteeringConf.num_priorities; i++)
        {
            priorities_rotation[i] = 0.0f;
        }
    }

    // Methods for behaviours to set / add velocities
    public void SetMovementVelocity (Vector3 velocity) 
	{
        current_velocity = velocity;
	}

	public void AccelerateMovement (Vector3 acceleration, int priority) 
	{
        priorities_velocity[priority] += acceleration;
    }

	public void SetRotationVelocity (float rotation_speed) 
	{
        current_rotation_speed = rotation_speed;
	}

    public void AccelerateRotation(float rotation_acceleration, int priority)
    {
        priorities_rotation[priority] += rotation_acceleration;
    }

    public Vector3 GetPriorityVelocity()
    {
        Vector3 aux_current_velocity = Vector3.zero;

        for (int i = SteeringConf.num_priorities; i >= 0; --i)
        {
            if (priorities_velocity[i] != Vector3.zero)
            {
                aux_current_velocity = priorities_velocity[i];
                break;
            }
        }

        return aux_current_velocity;
    }

    public float GetPriorityRotation()
    {
        float aux_current_rotation = 0.0f;

        for (int i = SteeringConf.num_priorities; i >= 0; --i)
        {
            if (priorities_rotation[i] != 0.0f)
            {
                aux_current_rotation = priorities_rotation[i];
                break;
            }
        }

        return aux_current_rotation;
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 new_movement_velocity = Vector3.zero;
        float new_angular_velocity = 0.0f;

        // Pick the lowest priority level
        foreach (Vector3 v in movement_velocity)
        {
            if (Mathf.Approximately(v.magnitude, 0.0f) == false)
            {
                new_movement_velocity = v;
                break;
            }
        }


        foreach (float f in angular_velocity)
        {
            if (Mathf.Approximately(f, 0.0f) == false)
            {
                new_angular_velocity = f;
                break;
            }
        }

        ResetPriorities();

        // Apply
        movement += new_movement_velocity;
        rotation += new_angular_velocity;

        // cap velocity
        if (movement.magnitude > max_mov_velocity)
        {
            movement.Normalize();
            movement *= max_mov_velocity;
        }

        // cap rotation
        Mathf.Clamp(rotation, -max_rot_velocity, max_rot_velocity);

        // rotate the arrow
        float angle = Mathf.Atan2(movement.x, movement.z);
        aim.transform.rotation = Quaternion.AngleAxis(Mathf.Rad2Deg * angle, Vector3.up);

        // strech it
        arrow.value = movement.magnitude * 4;

        // final rotate
        transform.rotation *= Quaternion.AngleAxis(rotation * Time.deltaTime, Vector3.up);

        // finally move
        transform.position += movement * Time.deltaTime;
    }

    /*current_velocity += GetPriorityVelocity();

    // cap velocity
    if (current_velocity.magnitude > max_mov_speed)
        current_velocity = current_velocity.normalized * max_mov_speed;

    // cap rotation
    current_rotation_speed = GetPriorityRotation();
    current_rotation_speed = Mathf.Clamp(current_rotation_speed, -max_rot_speed, max_rot_speed);

    float angle = Mathf.Atan2(current_velocity.x, current_velocity.z);
    aim.transform.rotation = Quaternion.AngleAxis(Mathf.Rad2Deg * angle, Vector3.up);
    transform.rotation *= Quaternion.AngleAxis(current_rotation_speed * Time.deltaTime, Vector3.up);

    //arrow.value = current_velocity.magnitude * 4;
    //if (current_velocity.magnitude < 0.1)
    //{
    //    current_velocity = Vector3.zero;
    //}
        //Debug.Log(current_velocity.magnitude);
        anim.SetFloat("Speed_f", current_velocity.magnitude);
        current_velocity.y = 0;
        transform.position += current_velocity * Time.deltaTime;

    for (int i = 0; i <= SteeringConf.num_priorities; ++i)
    {
        priorities_velocity[i] = Vector3.zero;
        priorities_rotation[i] = 0.0f;
    }
    */
}
    //void CreateCurve()
    //{
    //    for (int i = 0; i < curve.Points.Length; i++)
    //    {
    //        curve.Delete(curve.Points[i]);
    //    }
    //    curve.AddPoint(new BGCurvePoint(curve, transform.position, BGCurvePoint.ControlTypeEnum.Absent, true));
    //    for (int i = 0; i < GetComponent<NavMeshAgent>().path.corners.Length; i++)
    //    {
    //        curve.AddPoint(new BGCurvePoint(curve, GetComponent<NavMeshAgent>().path.corners[i], BGCurvePoint.ControlTypeEnum.Absent, true));
    //    }
    //    curveManager.GetComponent<PathManager>().CreateManagerCurve(this.gameObject, curve);
    //}
