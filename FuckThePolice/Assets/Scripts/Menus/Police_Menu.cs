using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Police_Menu : MonoBehaviour
{
    public GameObject menu;
    // Start is called before the first frame update
    void Start()
    {
        menu.SetActive(false);
    }

    private void OnMouseUpAsButton()
    {
        if (menu.activeSelf)
        {
            menu.SetActive(false);
        }
        else
        {
            menu.SetActive(true);
            menu.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 4, this.gameObject.transform.position.z);
        }
    }

    void OnMouseDown()
    {
        CallNextCitizen();
    }

    void CallNextCitizen()
    {

    }
}
