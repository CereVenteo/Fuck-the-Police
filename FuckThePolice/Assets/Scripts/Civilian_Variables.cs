using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Civilian_Variables : MonoBehaviour
{
    public Game_Manager Game_Manager;
    SteeringFollowNavMeshPath nav;
    public bool secretary_free;
    public List<bool> agents_free;
    public Vector3 civil_target;
    public List<Vector3> wait_positions;
    public Vector3 wait_position;
    public bool waiting = false;
    public Vector3 secretary_position;
    public Vector3 go_away;
    public GameObject target;
    public GameObject target2;
    public bool talking;
    public bool created_path = false;
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
            waiting = true;
        }
        else
        {
            waiting = false;
        }


    }
    public IEnumerator Talk()
    {
        Game_Manager.secretary_free = false;
        yield return new WaitForSeconds(15);
        Game_Manager.secretary_free = true;
    }
    //public void hola()
    //{
    //    Debug.Log("Holaaaaa");
    //}
}
