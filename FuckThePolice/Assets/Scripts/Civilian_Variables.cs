using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Civilian_Variables : MonoBehaviour
{
    public bool secretary_free = false;
    public List<bool> agents_free;
    public Vector3 civil_target;
    public List<Vector3> wait_positions;
    public Vector3 wait_position;
    public Vector3 go_away;
    public GameObject target;
    public bool created_path = false;
    // Start is called before the first frame update
    void Awake()
    {
        Vector3 x = new Vector3 (7,-1.3f, 4 );
        wait_positions.Add(x);
        wait_position = target.transform.position;
        secretary_free = false;
    }

    // Update is called once per frame
    void Update()
    {
     
    }
    public void hola()
    {
        Debug.Log("Holaaaaa");
    }
}
