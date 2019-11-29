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
        int count = 0;
        int agent_pos_array = -1;
        for (int i = 0; i < manager.police.Count; i++)
        {
            Agent_Variables agent = manager.police[i].gameObject.GetComponent<Agent_Variables>();

            if (agent.request_for_interrogation == true)
                count++;
        }

        if (count < 4)
        {
            for (int i = 0; i < manager.police.Count; i++)
            {
                Agent_Variables agent = manager.police[i].gameObject.GetComponent<Agent_Variables>();

                if (agent.waiting == true && agent.talking == false && agent_pos_array == -1)
                    agent_pos_array = i;
                else if (agent.waiting == true && agent.talking == false)
                {
                    agent.request_for_interrogation = true;
                    agent.interrogator = true;
                    if (count < 2)
                        agent.room = true;
                    else
                        agent.room = false;

                    agent = manager.police[agent_pos_array].gameObject.GetComponent<Agent_Variables>();
                    agent.request_for_interrogation = true;
                    agent.supervisor = true;
                    if (count < 2)
                        agent.room = true;
                    else
                        agent.room = false;
                    break;
                }
            }
        }
    }
}
