using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Criminal_Variables : MonoBehaviour
{
    public Game_Manager Game_Manager;
    SteeringFollowNavMeshPath nav;
    public int id = -1;
    public List<GameObject> target_cells;
    public Vector3 target_cell;
    public GameObject cell;
    public GameObject follower_target;
    public GameObject interrogation_target;
    public bool police_waiting;
    public bool interrogation_time;
    public bool waiting;

    // Start is called before the first frame update
    void Start()
    {
        nav = this.GetComponent<SteeringFollowNavMeshPath>();
        Game_Manager = GameObject.Find("Game_Manager").GetComponent<Game_Manager>();
        Game_Manager.Check_Cells();
        Check_Cells();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.G))
        //{
        //    interrogation_time = true;
        //}
        if (nav.path.corners.Length > 1)
            if (nav.current_point == nav.path.corners.Length - 1)
            {
                Vector3 diff = nav.path.corners[nav.path.corners.Length - 1] - transform.position;

                if (diff.magnitude < 2)
                    waiting = true;
                else
                    waiting = false;
            }
    }
    public void Check_Cells()
    {
        for (int i = 0; i < target_cells.Count; i++)
        {
            if(target_cells[i].activeInHierarchy == true)
            {
                if (Game_Manager.free_cells[i] == true)
                {
                    id = i;
                    target_cell = target_cells[i].transform.position;
                    cell = Game_Manager.cells[i];
                }
            }
        }
    }
    public void Off_Follower()
    {
        follower_target.SetActive(false);
    }
    public void teleport_cell()
    {
        this.transform.position = cell.transform.position;
        GetComponent<Move>().current_velocity = Vector3.zero;
        nav.path = new NavMeshPath();
    }

    public void teleport_out_cell()
    {
        this.transform.position = target_cell;
    }

    public IEnumerator CriminalInterrogtion()
    {
        yield return new WaitForSeconds(30);
        interrogation_time = false;
    }
}
