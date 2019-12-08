using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_Agent : MonoBehaviour
{
    SteeringFollowNavMeshPath nav;
    GameObject target;
    public GameObject go_away;
    Vector3 start_position;
    // Start is called before the first frame update
    void Start()
    {
        nav = this.GetComponent<SteeringFollowNavMeshPath>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(target.GetComponent<Criminal_Variables>().following)
            nav.CreatePath(target.transform.position);
        else
            Go_Away();
        
    }

    public void SetTarget(GameObject _target)
    {
        target = _target;
        transform.position = new Vector3(target.transform.position.x - 2, target.transform.position.y, target.transform.position.z + 2);
    }
    public void Go_Away()
    {
        nav.CreatePath(go_away.transform.position);
    }
}
