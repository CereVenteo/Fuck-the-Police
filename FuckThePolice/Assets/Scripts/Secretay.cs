using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Secretay : MonoBehaviour
{
    SteeringFollowNavMeshPath nav;
    public GameObject target;
    public GameObject go_away;
    bool positioned = false;
    // Start is called before the first frame update
    void Start()
    {
        nav = this.GetComponent<SteeringFollowNavMeshPath>();
    }

    void Update()
    {
        if (nav.path.corners.Length > 1 && !positioned)
            if (nav.current_point == nav.path.corners.Length - 1)
            {
                Vector3 diff = nav.path.corners[nav.path.corners.Length - 1] - transform.position;

                if (diff.magnitude < 1)
                {
                    this.transform.position = target.transform.position;
                    this.transform.rotation = target.transform.rotation;
                    GetComponent<Move>().current_velocity = Vector3.zero;
                    GetComponent<Move>().rotation = 0.0f;
                    nav.path = new NavMeshPath();
                    positioned = true;
                }
                    
            }
    
    }

    public void Go_Start()
    {
        nav.CreatePath(target.transform.position);
        positioned = false;
    }

    public void Go_Away()
    {
        nav.CreatePath(go_away.transform.position);
    }
}
