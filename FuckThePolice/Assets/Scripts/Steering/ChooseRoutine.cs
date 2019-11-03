using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChooseRoutine : MonoBehaviour
{
    Move move;
    SteeringFollowNavMeshPath nav;
    // Start is called before the first frame update
    void Start()
    {
        move = GetComponent<Move>();
        nav = GetComponent<SteeringFollowNavMeshPath>();

        nav.path = new NavMeshPath();
        NavMesh.CalculatePath(transform.position, move.target.transform.position, (1 << NavMesh.GetAreaFromName("Walkable")), nav.path);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
