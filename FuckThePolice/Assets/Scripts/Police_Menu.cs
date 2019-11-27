using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Police_Menu : MonoBehaviour
{
    public GameObject menu_panel;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(menu_panel);
    }

    // Update is called once per frame
    void Update()
    {
        if(menu_panel)
        {
            menu_panel.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 4, this.gameObject.transform.position.z);
        }
    }

    void CallNextCitizen()
    {

    }
}
