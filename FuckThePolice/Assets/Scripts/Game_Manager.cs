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
    public int civilians_waiting = 0;
    public List<GameObject> civil_agents_pos;
    public List<GameObject> civilians;
    public List<GameObject> agents_pos;
    public List<GameObject> police;
    public List<GameObject> desktops;
    public List<GameObject> cells;
    public List<GameObject> criminals;
    public List<bool> free_cells;
    public List<GameObject> cars;
    public GameObject car_police;
    //public bool interrogatory_room = true;

    public GameObject interrogation_2;

    int id_criminals = 0;
    //int active_civilians = 0;
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(Enable_Civilian());
        secretary_free = true;
        Check_Cells();
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
                if(civilians[i].GetComponent<Civilian_Variables>().identity != 0 && !civilians[i].GetComponent<Civilian_Variables>().agent_talk)
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
                    break;
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

    public void AddCivilian()
    {
        if(civilians_waiting < 1)
        {
            for (int i = 0; i < civilians.Count; i++)
            {
                if (!civilians[i].activeSelf)
                {
                    civilians[i].SetActive(true);
                    break;
                }
            }
        }
    }

    public void AddCriminal(int car)
    {
        if(cars[car].activeSelf)
        {
            for (int i = 0; i < criminals.Count; i++)
            {
                if (!criminals[i].activeSelf)
                {
                    criminals[i].SetActive(true);
                    criminals[i].GetComponent<Criminal_Variables>().id = ++id_criminals;
                    criminals[i].transform.position = new Vector3(cars[car].transform.position.x + 2, cars[car].transform.position.y, cars[car].transform.position.z);
                    car_police.SetActive(true);
                    car_police.GetComponent<Car_Agent>().SetTarget(criminals[i]);
                    break;
                }
            }
        }
    }

    public void AddPolice()
    {
        for (int i = 0; i < police.Count; i++)
        {
            if(desktops[i].activeSelf)
                police[i].SetActive(true);
        }
    }

    public void Night()
    {
        for (int i = 0; i < civilians.Count; i++)
        {
            if (civilians[i].activeSelf)
            {
                civilians[i].GetComponent<Civilian_Variables>().Go_Away();
                civilians_waiting = 0;
            }
        }
        //for (int i = 0; i < police.Count && i%2 == 0; i++)
        //{
        //    if (desktops[i].activeSelf)
        //        police[i].SetActive(false);
        //}
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
    