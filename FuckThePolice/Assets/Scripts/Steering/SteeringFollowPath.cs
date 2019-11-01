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
    int i = 0;

    public float ratio_increment = 0.1f;
    public float min_distance = 1.0f;
    float current_ratio = 0.0f;

    // Use this for initialization
    void Start()
    {
        //man = GameObject.Find("CurveManager").GetComponent<PathManager>();
        move = GetComponent<Move>();
        seek = GetComponent<SteeringSeek>();
        arrive = GetComponent<SteeringArrive>();

        // TODO 1: Calculate the closest point from the tank to the curve
        //float distance;
        closest_point = GetComponent<NavMeshAgent>().path.corners[i];
        //current_ratio = distance / man.path.Curve.Points.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if (i <= GetComponent<NavMeshAgent>().path.corners.Length)
        {
            if (Vector3.Distance(transform.position, closest_point) <= min_distance)
            {
                i++;
                if (i <= GetComponent<NavMeshAgent>().path.corners.Length)
                {
                    closest_point = GetComponent<NavMeshAgent>().path.corners[i];
                }
                else nextPath();
                /* current_ratio += ratio_increment;
                 if (current_ratio > 1)
                     current_ratio = 0;
                 closest_point = man.path.CalcPositionByDistanceRatio(current_ratio);*/
            }
            if (i < GetComponent<NavMeshAgent>().path.corners.Length)
            {
                seek.Steer(closest_point);
            }
            else
            {
                arrive.Steer(closest_point);

            }
        }else
        {
            nextPath();
        }
        // TODO 2: Check if the tank is close enough to the desired point
        // If so, create a new point further ahead in the path
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
    public void nextPath()
    {
        i = 0;
        closest_point = GetComponent<NavMeshAgent>().path.corners[i];
    }
}
