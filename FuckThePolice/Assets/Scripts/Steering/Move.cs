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
	public float max_rot_speed = 300.0f; // in degrees / second
	public float max_rot_acceleration = 100.0f; // in degrees
    public Animator anim;

    public GameObject curveManager;

    public BGCurve curve;

    [Header("-------- Read Only --------")]
	public Vector3 current_velocity = Vector3.zero;
    
    public float current_rotation_speed = 0.0f; // degrees

    public Vector3[] priorities_velocity;
    public float[] priorities_rotation;

    private void Start()
    {
        anim = GetComponent<Animator>();
        curve = gameObject.AddComponent<BGCurve>();
        curveManager = GameObject.Find("CurveManager");

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
        current_velocity += GetPriorityVelocity();

        // cap velocity
        if (current_velocity.magnitude > max_mov_speed)
            current_velocity = current_velocity.normalized * max_mov_speed;

        // cap rotation
        current_rotation_speed = GetPriorityRotation();
        current_rotation_speed = Mathf.Clamp(current_rotation_speed, -max_rot_speed, max_rot_speed);

        float angle = Mathf.Atan2(current_velocity.x, current_velocity.z);
        aim.transform.rotation = Quaternion.AngleAxis(Mathf.Rad2Deg * angle, Vector3.up);
        transform.rotation *= Quaternion.AngleAxis(current_rotation_speed * Time.deltaTime, Vector3.up);

        arrow.value = current_velocity.magnitude * 4;
        if (current_velocity.magnitude < 0.1)
        {
            current_velocity = Vector3.zero;
        }
            //Debug.Log(current_velocity.magnitude);
            anim.SetFloat("Speed_f", current_velocity.magnitude);
            transform.position += current_velocity * Time.deltaTime;

        for (int i = 0; i <= SteeringConf.num_priorities; ++i)
        {
            priorities_velocity[i] = Vector3.zero;
            priorities_rotation[i] = 0.0f;
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
