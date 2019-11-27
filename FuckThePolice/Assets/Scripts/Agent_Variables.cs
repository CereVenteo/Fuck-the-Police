using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent_Variables : MonoBehaviour
{
    public Game_Manager Game_Manager;
    SteeringFollowNavMeshPath nav;
    public int id;
    public GameObject work_target;
    public Vector3 work_position;
    public Vector3 criminal_cell;
    public GameObject interrogator_position;
    public GameObject interrogator_supervisor;
    public GameObject criminal_target;
    public bool request_for_interrogation;
    public bool request_civilian;
    public bool waiting = false;
    public bool civil_waiting;
    public bool civile_talk = false;
    public bool go_back = true;
    int civil;
    int civil_id = -1;
    // Start is called before the first frame update
    void Start()
    {
        nav = this.GetComponent<SteeringFollowNavMeshPath>();
        Game_Manager = GameObject.Find("Game_Manager").GetComponent<Game_Manager>();
        work_position = work_target.transform.position;
        //temporal
        criminal_cell = criminal_target.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            request_civilian = true;
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            request_for_interrogation = true;
        }
        if (nav.path.corners.Length > 1)
        if (nav.current_point == nav.path.corners.Length - 1)
        {
            Vector3 diff = nav.path.corners[nav.path.corners.Length - 1] - transform.position;

            if (diff.magnitude < 2)
                waiting = true;
            else
                waiting = false;
        }
        if (!go_back)
        {
            //Vector3 diff = nav.path.corners[nav.path.corners.Length - 1] - transform.position;

            //if (diff.magnitude < 2)
            //{
            //    go_back = true;
            //}
        }
        if(civil != 0 && civil_id != -1)
        {
            civil_waiting = Game_Manager.civilians[civil_id].GetComponent<Civilian_Variables>().waiting;
            civile_talk = true;
        }
    }

    public void Call_Civil()
    {
            civil = Game_Manager.Request_identity();
            civil_id = Game_Manager.Request_Civilian(civil);
            Game_Manager.Call_Civilian(civil, id);   
    }

    public IEnumerator AgentTalk()
    {
        yield return new WaitForSeconds(15);
        request_civilian = false;
        civile_talk = false;
    }

    public IEnumerator AgentInterrogtion()
    {
        yield return new WaitForSeconds(30);
    }

    public void Interrogation()
    {
        nav.CreatePath(criminal_cell);
    }
}
