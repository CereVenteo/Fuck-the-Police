using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BansheeGz.BGSpline.Components;
using BansheeGz.BGSpline.Curve;

public class Game_Manager : MonoBehaviour
{
    public bool secretary_free;
    public List<bool> agents_free;
    // Start is called before the first frame update
    void Start()
    {
        secretary_free = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
    