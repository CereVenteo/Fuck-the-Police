using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Secretay : MonoBehaviour
{
    SteeringFollowNavMeshPath nav;
    public GameObject target;
    public GameObject go_away;
    // Start is called before the first frame update
    void Start()
    {
        nav = this.GetComponent<SteeringFollowNavMeshPath>();
    }

    public void Go_Start()
    {
        nav.CreatePath(target.transform.position);
    }

    public void Go_Away()
    {
        nav.CreatePath(go_away.transform.position);
    }
}
