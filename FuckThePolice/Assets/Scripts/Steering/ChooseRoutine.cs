using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChooseRoutine : MonoBehaviour
{
    Move move;
    SteeringFollowNavMeshPath nav;
    SteeringFollowPath pathnav;
    public GameObject target1;
    public GameObject target2;
    bool x = true;
    bool i = false;
    // Start is called before the first frame update
    void Start()
    {
        move = GetComponent<Move>();
        nav = GetComponent<SteeringFollowNavMeshPath>();
        pathnav = GetComponent<SteeringFollowPath>();

        if (nav.enabled && target2 != false && this.gameObject.name != "SimpleCitizens_Grandma_White")
        {
            nav.enabled = false;
        }
        if (!nav.enabled && target2 != false)
        {
            nav.enabled = true;
        }

    }
    // Update is called once per frame
    void Update()
    {
        if (i)
        {
            if (nav.enabled && target2 != false)
            {
                if (move.target == target1)
                {
                    move.target = target2;
                    nav.CreatePath(move.target.transform.position);
                }
                else
                {
                    move.target = target1;
                    nav.CreatePath(move.target.transform.position);
                }
            }
            else if (pathnav.enabled == true)
            {
                nav.enabled = true;
                move.target = target1;
                pathnav.enabled = false;
            }
            else
            {
                pathnav.enabled = true;
                nav.enabled = false;
            }
        }
        if (x)
        {
            StartCoroutine(Change());

        }
    }
    IEnumerator Change()
    {
        x = false;
        i = false;
        yield return new WaitForSeconds(60);
        x = true;
        i = true;

    }
}
