using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn_Number : MonoBehaviour
{
    public GameObject turn_number_ui;
    TextMesh text;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(turn_number_ui);
    }

    // Update is called once per frame
    void Update()
    {
        if(turn_number_ui)
        {
            turn_number_ui.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 4, this.gameObject.transform.position.z);
        }
    }
    
    public void SetNumber(uint number)
    {
        text = turn_number_ui.GetComponent<TextMesh>();
        text.text = number.ToString();
    }
}
