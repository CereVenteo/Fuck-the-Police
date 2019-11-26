using UnityEngine;
using System.Collections;
using BansheeGz.BGSpline.Components;
using BansheeGz.BGSpline.Curve;
using UnityEngine.AI;

// TO FOLLOW PATH
public class SteeringFollowNavMeshPath : SteeringAbstract
{
    Move move;
    SteeringArrive arrive;
    SteeringSeek seek;
    SteeringAlign align;
    public NavMeshPath path;

    public float min_distance;
    public int current_point = 1;
    bool once = false;
    public bool created_path = false;

    // Use this for initialization
    void Start()
    {
        move = GetComponent<Move>();
        arrive = GetComponent<SteeringArrive>();
        seek = GetComponent<SteeringSeek>();
        align = GetComponent<SteeringAlign>();
        Vector3 x = Vector3.zero;
        path = new NavMeshPath();
        //NavMesh.CalculatePath(transform.position,move.target.transform.position, (1 << NavMesh.GetAreaFromName("Walkable")), path);

    }

    // Update is called once per frame
    void Update()
    {
        if(!move)
            move = GetComponent<Move>();
        else
        {
            if (path.status == NavMeshPathStatus.PathComplete)
            {
                align.Steer(path.corners[current_point]);
                if (current_point != path.corners.Length - 1)
                {
                    seek.Steer(path.corners[current_point]);
                    if (Vector3.Distance(transform.position, path.corners[current_point]) < min_distance)
                        current_point++;
                }
                else
                {
                    if (arrive.Steer(path.corners[current_point]))
                    {
                        if (!once)
                        {
                            once = true;
                        }

                    }
                    else
                        once = false;
                }
            }
        }
    }

    public void CreatePath(Vector3 pos)
    {
        current_point = 1;
        if(path != null)
        path.ClearCorners();
        //if(!created_path)
        NavMesh.CalculatePath(transform.position, pos, (1 << NavMesh.GetAreaFromName("Walkable")), path);
    }
    //void OnDrawGizmosSelected()
    //{
    //    for (int i = 0; i < path.corners.Length - 1; i++)
    //    {
    //        // Display the explosion radius when selected
    //        Gizmos.color = Color.green;
    //        Gizmos.DrawWireSphere(path.corners[i], 5);
    //        // Useful if you draw a sphere were on the closest point to the path
    //    }
    //}
}
