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
    public List<GameObject> agents_pos;
    public List<GameObject> civilians;
    int active_civilians = 0;
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

    public void Call_Civilian(int identity)
    {
        for (int i = 0; i < civilians.Count; i++)
        {
            if(civilians[i].GetComponent<Civilian_Variables>().identity == identity)
            {
                civilians[i].GetComponent<Civilian_Variables>().agent_call = true;
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
    