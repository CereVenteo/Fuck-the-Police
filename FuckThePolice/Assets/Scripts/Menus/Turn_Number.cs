using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn_Number : MonoBehaviour
{
    public GameObject turn_number_ui;
    TextMesh text;
    bool pass;
    Quaternion rotation;
    // Start is called before the first frame update
    void Start()
    {
        pass = true;
        turn_number_ui.SetActive(false);
        rotation = Quaternion.Euler(45, 90, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(this.GetComponent<Civilian_Variables>().identity != 0 && pass)
        {
            turn_number_ui.SetActive(true);
            SetNumber(this.GetComponent<Civilian_Variables>().identity);
            pass = false;
        }
        else if(this.GetComponent<Civilian_Variables>().identity == 0 && !pass)
        {
            turn_number_ui.SetActive(false);
            pass = true;
        }

        if (turn_number_ui)
        {
            turn_number_ui.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 4, this.gameObject.transform.position.z);
            turn_number_ui.transform.rotation = rotation;
        }
    }
    
    public void SetNumber(int number)
    {
        text = turn_number_ui.GetComponent<TextMesh>();
        text.text = number.ToString();
    }
}
