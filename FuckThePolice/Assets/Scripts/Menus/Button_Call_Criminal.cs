using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Call_Criminal : MonoBehaviour
{
    public GameObject menu;

    private void OnMouseUpAsButton()
    {
        CallCriminal();
        menu.SetActive(false);

    }

    void CallCriminal()
    {
        Game_Manager manager = GameObject.Find("Game_Manager").GetComponent<Game_Manager>();
        for (int i = 0; i < manager.police.Count; i++)
        {
            Agent_Variables agent = manager.police[i].gameObject.GetComponent<Agent_Variables>();
            if (agent.talking == false)
                agent.request_for_interrogation = true;
        }
    }
}
