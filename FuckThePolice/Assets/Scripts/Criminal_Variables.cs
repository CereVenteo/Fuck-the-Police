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
    public GameObject interrogation_target_2;
    public bool following = true;
    public bool room = true;
    public bool police_waiting;
    public bool interrogation_time;
    public bool waiting;
    public GameObject go_away;
    AudioSource audio_sim;

    // Start is called before the first frame update
    void Start()
    {
        nav = this.GetComponent<SteeringFollowNavMeshPath>();
        Game_Manager = GameObject.Find("Game_Manager").GetComponent<Game_Manager>();
        Check_Cells();
        audio_sim = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position.Set(transform.position.x, -1.4f, transform.position.z);
        //room = Game_Manager.interrogatory_room;
        if (cell == null)
            Check_Cells();

        if (nav.path.corners.Length > 1)
            if (nav.current_point == nav.path.corners.Length - 1)
            {
                Vector3 diff = nav.path.corners[nav.path.corners.Length - 1] - transform.position;

                if (diff.magnitude < 1)
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
                    target_cell = target_cells[i].transform.position;
                    cell = Game_Manager.cells[i];
                    Game_Manager.free_cells[i] = false;
                    following = true;
                    break;
                }
            }
        }
    }
    public void Off_Follower()
    {
        follower_target.SetActive(false);
        following = false;
    }
    public void On_Follower()
    {
        follower_target.SetActive(true);
    }
    public void teleport_cell()
    {
        this.transform.position = cell.transform.position;
        GetComponent<Move>().current_velocity = Vector3.zero;
        GetComponent<Move>().rotation = 0.0f;
        nav.path = new NavMeshPath();
    }

    public void teleport_out_cell()
    {
        this.transform.position = new Vector3(target_cell.x, target_cell.y, target_cell.z - 3);
        for (int i = 0; i < target_cells.Count; i++)
        {
            if (cell == Game_Manager.cells[i])
            {
                Game_Manager.free_cells[i] = true;
            }
        }
    }

    public IEnumerator CriminalInterrogtion()
    {
        //Game_Manager.interrogatory_room = false;
        audio_sim.Play();
        yield return new WaitForSeconds(30);
        //Game_Manager.interrogatory_room = true;
        GameObject.Find("Game_Manager").GetComponent<Canvas>().SetPoints(300);
        audio_sim.Stop();
        interrogation_time = false;
    }
    public IEnumerator Wait_time(int time)
    {
        yield return new WaitForSeconds(time);
    }
    public void Go_Away()
    {
        nav.CreatePath(go_away.transform.position);
    }
    public void criminal_off()
    {
        Off_Follower();
        cell = null;
        waiting = false;
        this.gameObject.SetActive(false);
    }
}
