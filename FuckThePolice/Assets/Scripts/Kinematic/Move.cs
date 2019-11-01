using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.AI;
using BansheeGz.BGSpline.Components;
using BansheeGz.BGSpline.Curve;

public class Move : MonoBehaviour {

	public GameObject target;
	public GameObject aim;
	public Slider arrow;
	public float max_mov_speed = 5.0f;
	public float max_mov_acceleration = 0.1f;
	public float max_rot_speed = 10.0f; // in degrees / second
	public float max_rot_acceleration = 0.1f; // in degrees

    public GameObject curveManager;

    public BGCurve curve;

    [Header("-------- Read Only --------")]
	public Vector3 current_velocity = Vector3.zero;
    
    public float current_rotation_speed = 0.0f; // degrees

    public Vector3[] movement_velocity;
    public float[] rotation_velocity;

    private void Start()
    {
        curve = gameObject.AddComponent<BGCurve>();
        curveManager = GameObject.Find("CurveManager");

        movement_velocity = new Vector3[SteeringConf.num_priorities];
        rotation_velocity = new float[SteeringConf.num_priorities];
    }

    // Methods for behaviours to set / add velocities
    public void SetMovementVelocity (Vector3 velocity) 
	{
        current_velocity = velocity;
	}

	public void AccelerateMovement (Vector3 acceleration, int priority) 
	{
        //current_velocity += acceleration;
        if (priority == 0)
        {
            movement_velocity[priority] += acceleration;
        }
        else
            movement_velocity[priority - 1] += acceleration;
    }

	public void SetRotationVelocity (float rotation_speed) 
	{
        current_rotation_speed = rotation_speed;
	}

	public void AccelerateRotation (float rotation_acceleration, int priority) 
	{
        //current_rotation_speed += rotation_acceleration;
        if (priority == 0)
        {
            rotation_velocity[priority] += rotation_acceleration;
        }
        rotation_velocity[priority-1] += rotation_acceleration;
	}

    // Update is called once per frame
    void Update()
    {
        
        if (GetComponent<NavMeshAgent>().destination.x != target.GetComponent<Transform>().position.x)
        {
            GetComponent<NavMeshAgent>().SetDestination(target.GetComponent<Transform>().position);
        }
        for (int i = movement_velocity.Length-1; i >= 0; i--)
        {
            if (movement_velocity[i] != Vector3.zero)
            {
                    current_velocity += movement_velocity[i];
                    break;
            }
            
        }
        //for (int i = rotation_velocity.Length-1; i < 0; i--)
        //{
        //    if (rotation_velocity[i] != 0)
        //    {
        //        if (current_velocity.magnitude > max_mov_speed)
        //        {
        //            // cap rotation
        //            current_rotation_speed = Mathf.Clamp(current_rotation_speed, -max_rot_speed, max_rot_speed);

        //            // rotate the arrow
        //            float angle = Mathf.Atan2(current_velocity.x, current_velocity.z);
        //            aim.transform.rotation = Quaternion.AngleAxis(Mathf.Rad2Deg * angle, Vector3.up);
        //            transform.rotation *= Quaternion.AngleAxis(current_rotation_speed * Time.deltaTime, Vector3.up);
        //            break;
        //        }
        //    }

        //}
        if (current_velocity.magnitude > max_mov_speed)
        {
            current_velocity = current_velocity.normalized * max_mov_speed;
        }
        arrow.value = current_velocity.magnitude * 4;
        transform.position += current_velocity * Time.deltaTime;
        for (int i = 0; i < SteeringConf.num_priorities; i++)
        {
            movement_velocity[i] = Vector3.zero;
            rotation_velocity[i] = 0;
        }
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
}
