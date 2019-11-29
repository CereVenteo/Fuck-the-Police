using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Civilian_Variables : MonoBehaviour
{
    public Game_Manager Game_Manager;
    SteeringFollowNavMeshPath nav;
    public bool secretary_free;
    public Vector3 civil_target;
    public List<GameObject> wait_positions;
    public Vector3 wait_position;
    public bool waiting = false;
    public Vector3 secretary_position;
    public GameObject go_away;
    public int identity;
    public bool agent_call;
    public Vector3 go_agent_position;
    public bool agent_talk = false;
    public GameObject target;
    public GameObject target2;
    public bool go_back = true;
    // Start is called before the first frame update
    void Start()
    {
        nav = this.GetComponent<SteeringFollowNavMeshPath>();
        Game_Manager = GameObject.Find("Game_Manager").GetComponent<Game_Manager>();
        wait_position = target.transform.position;
        secretary_position = target2.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        secretary_free = Game_Manager.secretary_free;
        if(nav.path.corners.Length >1)
        if (nav.current_point == nav.path.corners.Length - 1)
        {
                Vector3 diff = nav.path.corners[nav.path.corners.Length - 1] - transform.position;

                if (diff.magnitude < 2)
                    waiting = true;
                else
                    waiting = false;
        }
        if(!go_back)
        {
            Vector3 diff = nav.path.corners[nav.path.corners.Length -1] - transform.position;

            if (diff.magnitude < 2)
            {
                go_back = true;
            }
        }
    }

    public void Wait()
    {
        int size = wait_positions.Count;
        if (nav.path.corners.Length < 1)
            nav.CreatePath(wait_positions[Random.Range(0, size)].transform.position);
        if (waiting)
        {
            this.GetComponent<Move>().max_mov_speed = 2;
            nav.CreatePath(wait_positions[Random.Range(0, size)].transform.position);
        }
    }

    public void Go_Agent()
    {
        agent_talk = true;
        Game_Manager.civilians_waiting--;
        nav.CreatePath(go_agent_position);
    }

    public IEnumerator Talk()
    {
        Game_Manager.secretary_free = false;
        yield return new WaitForSeconds(10);
        identity = ++Game_Manager.civilian_identity;
        Game_Manager.civilians_waiting++;
        Game_Manager.secretary_free = true;
    }

    public IEnumerator AgentTalk()
    {
        agent_call = false;
        yield return new WaitForSeconds(15);
        agent_talk = false;
        GameObject.Find("Game_Manager").GetComponent<Canvas>().CivilianHelped();
    }

    public void Go_Away()
    {
        nav.CreatePath(go_away.transform.position);
        identity = 0;
        go_back = false;
    }

    public void End_Round()
    {
       this.gameObject.SetActive(false);
    }
   
}
