using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_Agent : MonoBehaviour
{
    SteeringFollowNavMeshPath nav;
    public GameObject target;
    public GameObject go_away;
    // Start is called before the first frame update
    void Start()
    {
        nav = this.GetComponent<SteeringFollowNavMeshPath>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target.activeInHierarchy == true)
            nav.CreatePath(target.transform.position);
        else
            Go_Away();

    }
    public void Go_Away()
    {
        nav.CreatePath(go_away.transform.position);
    }
}
