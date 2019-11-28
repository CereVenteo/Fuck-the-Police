using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_Agent : MonoBehaviour
{
    SteeringFollowNavMeshPath nav;
    public GameObject target;
    public GameObject go_away;
    Vector3 start_position;
    bool start_again = false;
    // Start is called before the first frame update
    void Start()
    {
        nav = this.GetComponent<SteeringFollowNavMeshPath>();
        start_position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (target.activeInHierarchy == true)
            if (start_again)
            {
                transform.position = start_position;
                start_again = false;
            }
            else
                nav.CreatePath(target.transform.position);
        else
        {
            start_again = true;
            Go_Away();
        }
    }
    public void Go_Away()
    {
        nav.CreatePath(go_away.transform.position);
    }
}
