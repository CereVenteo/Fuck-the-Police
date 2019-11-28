using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BansheeGz.BGSpline.Components;
using BansheeGz.BGSpline.Curve;

public class Game_Manager : MonoBehaviour
{
    public bool secretary_free;
    public int civilian_identity = 0;
    public List<GameObject> civil_agents_pos;
    public List<GameObject> civilians;
    public List<GameObject> agents;
    public List<GameObject> cells;
    public List<GameObject> criminals;
    public List<bool> free_cells;
    public List<GameObject> police;
    //int active_civilians = 0;
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(Enable_Civilian());
        secretary_free = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public int Request_identity()
    {
        for (int i = 0; i < civilians.Count; i++)
        {
            if (civilians[i].GetComponent<Civilian_Variables>().agent_call != true)
            {
                return civilians[i].GetComponent<Civilian_Variables>().identity;
            }
        }
        return 0;
    }
    public void Call_Civilian(int identity , int id)
    {
        for (int i = 0; i < civilians.Count; i++)
        {
            if(civilians[i].GetComponent<Civilian_Variables>().identity == identity)
            {
                if (identity != 0)
                {
                    civilians[i].GetComponent<Civilian_Variables>().go_agent_position = civil_agents_pos[id].transform.position;
                    civilians[i].GetComponent<Civilian_Variables>().agent_call = true;
                }
            }
        }
    }

    public int Request_Civilian(int identity)
    {
        for (int i = 0; i < civilians.Count; i++)
        {
            if (civilians[i].GetComponent<Civilian_Variables>().identity == identity)
            {
                if (identity != 0)
                {
                    return i;
                }
            }
        }
        return -1;
    }
    public void Check_Cells()
    {
        for (int i = 0; i < cells.Count; i++)
        {
            if (cells[i].activeInHierarchy == true)
            {
                free_cells[i] = true;
            }
        }
    }

    //public IEnumerator Enable_Civilian()
    //{
    //    if (civilians[active_civilians].GetComponent<Civilian_Variables>().active_civilian != true)
    //    {
    //        civilians[active_civilians].GetComponent<Civilian_Variables>().active_civilian = true;
    //    }
    //    active_civilians++;
    //    Debug.Log(active_civilians);
    //    yield return new WaitForSeconds(15);
    //    StartCoroutine(Enable_Civilian());
    //}
}
    