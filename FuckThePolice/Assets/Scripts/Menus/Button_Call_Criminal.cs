using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Call_Criminal : MonoBehaviour
{
    public GameObject menu;
    bool full = true;

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
            if (manager.police[i].gameObject.GetComponent<Agent_Variables>().request_for_interrogation == true)
                count++;
        }

        if (count < 4)
        {
            full = true;
            for (int i = 0; i < manager.police.Count; i++)
            {
                Agent_Variables agent = manager.police[i].gameObject.GetComponent<Agent_Variables>();
                if (agent.room == false)
                    full = agent.room;
            }
            for (int i = 0; i < manager.police.Count; i++)
            {
                Agent_Variables agent = manager.police[i].gameObject.GetComponent<Agent_Variables>();
                if (agent.waiting == true && agent.talking == false && agent_pos_array == -1 && agent.request_for_interrogation == false)
                    agent_pos_array = i;
                else if (agent.waiting == true && agent.talking == false && agent.request_for_interrogation == false)
                {
                    agent.request_for_interrogation = true;
                    agent.criminal_target = this.GetComponentInParent<Criminal_Variables>().target_cell;
                    agent.criminal_id = this.GetComponentInParent<Criminal_Variables>().id;
                    agent.interrogator = true;
                    if (count < 2)
                    {
                        this.GetComponentInParent<Criminal_Variables>().room = true;
                        agent.room = true;
                    }
                    else
                    {
                        if (full != false)
                        {
                            this.GetComponentInParent<Criminal_Variables>().room = false;
                            agent.room = false;
                        }
                        else
                        {
                            agent.room = true;
                        }
                    }

                    agent = manager.police[agent_pos_array].gameObject.GetComponent<Agent_Variables>();
                    agent.request_for_interrogation = true;
                    agent.waiting = true;
                    agent.supervisor = true;
                    if (count < 2)
                        agent.room = true;
                    else
                    {
                        if (full != false)
                        {
                            agent.room = false;
                        }
                        else
                        {
                            agent.room = true;
                        }
                    }
                    break;
                }
            }
        }
    }
}
