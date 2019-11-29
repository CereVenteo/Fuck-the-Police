using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Call_Police : MonoBehaviour
{
    public GameObject menu;
    public GameObject police;

    private void OnMouseUpAsButton()
    {
        CallNextCitizen();
        menu.SetActive(false);
        
    }

    void CallNextCitizen()
    {
        Agent_Variables agent = police.GetComponent<Agent_Variables>();
        agent.request_civilian = true;
        //agent.talking = true;
    }
}
