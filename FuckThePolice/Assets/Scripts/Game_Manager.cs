﻿using System.Collections;
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
    public GameObject secretary;
    public List<bool> free_cells;
    public List<GameObject> cars;
    public GameObject car_target;
    public GameObject car_police;
    public List<GameObject> lights;
    //public bool interrogatory_room = true;

    public GameObject interrogation_2;

    public bool night_state = false;
    int id_criminals = 0;

    // Start is called before the first frame update
    void Start()
    {
        secretary_free = true;
        Check_Cells();
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
        bool createCriminal = false;
        for (int i = 0; i < free_cells.Count; i++)
        {
            if (free_cells[i] == true)
                createCriminal = true;
        }
        if(cars[car].activeSelf)
        {
            for (int i = 0; i < criminals.Count; i++)
            {
                if (!criminals[i].activeSelf)
                {
                    if (createCriminal)
                    {
                        cars[car].transform.position = car_target.transform.position;
                        cars[car].transform.rotation = car_target.transform.rotation;
                        cars[car].GetComponent<SteeringFollowPath>().RestartPath();
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
    }

    public void AddPolice()
    {
        for (int i = 0; i < police.Count; i++)
        {
            if(desktops[i].activeSelf)
                police[i].SetActive(true);
        }
    }

    public void AddSecretary()
    {
        secretary.GetComponent<Secretay>().Go_Start();
    }

    public void Day()
    {
        AddCriminal(0);
        AddSecretary();
        AddPolice();
        civilians_waiting = 0;
        civilian_identity = 0;
        night_state = false;
        secretary_free = true;
        for (int i = 0; i < lights.Count; i++)
        {
            if (i == 0)
                lights[i].GetComponent<Light>().intensity = 1;
            else
                lights[i].GetComponent<Light>().enabled = false;
        }
    }

    public void Night()
    {
        night_state = true;
        secretary_free = false;
        for (int i = 0; i < civilians.Count; i++)
        {
            if (civilians[i].activeSelf)
            {
                civilians[i].GetComponent<Civilian_Variables>().Go_Away();
            }
        }
        
        secretary.GetComponent<Secretay>().Go_Away();

        //for (int i = 0; i < police.Count; i++)
        //{
        //    if (police[i].activeSelf && police[i].GetComponent<Agent_Variables>().request_for_interrogation == false)
        //    {
        //        police[i].GetComponent<Agent_Variables>().Go_Away();
        //    }
        //}
        
        for (int i = 0; i < lights.Count; i++)
        {
            if (i == 0)
                lights[i].GetComponent<Light>().intensity = 0.4f;
            else
                lights[i].GetComponent<Light>().enabled = true;
        }
    }
    
}
    