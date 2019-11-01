using UnityEngine;
using System.Collections;
using BansheeGz.BGSpline.Components;
using BansheeGz.BGSpline.Curve;
using UnityEngine.AI;

public class SteeringFollowPath : SteeringAbstract
{
    Move move;
    public PathManager man;
    SteeringSeek seek;
    SteeringArrive arrive;
    Vector3 closest_point;

    //public float ratio_increment = 0.1f;
    public float min_distance = 0.5f;
    //float current_ratio = 0.0f;

    // Use this for initialization
    void Start()
    {
        //man = GameObject.Find("CurveManager").GetComponent<PathManager>();
        move = GetComponent<Move>();
        seek = GetComponent<SteeringSeek>();
        arrive = GetComponent<SteeringArrive>();

        // TODO 1: Calculate the closest point from the tank to the curve
        //float distance;
        //current_ratio = distance / man.path.Curve.Points.Length;
    }

    // Update is called once per frame
    void Update()
    { 
        closest_point = GetComponent<NavMeshAgent>().path.corners[1];
        if (GetComponent<NavMeshAgent>().path.corners.Length > 2)
            seek.Steer(closest_point);
        else
            arrive.Steer(closest_point);
    }

    void OnDrawGizmosSelected()
    {

        if (isActiveAndEnabled)
        {
            // Display the explosion radius when selected
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(closest_point, 5);
            // Useful if you draw a sphere were on the closest point to the path
        }

    }
}
