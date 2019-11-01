using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BansheeGz.BGSpline.Components;
using BansheeGz.BGSpline.Curve;

public class PathManager : MonoBehaviour
{
    public BGCcMath path;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CreateManagerCurve(GameObject origin,BGCurve agentCurve)
    {
        foreach (Transform eachChild in transform)
        {
            if (eachChild.name == origin.name)
            {
                GameObject.Destroy(eachChild.gameObject);
            }
        }
        GameObject originChild = new GameObject();
        originChild.transform.parent = this.gameObject.transform;
        originChild.name = origin.name;
        BGCurve curve = agentCurve;
        curve = originChild.gameObject.AddComponent<BGCurve>();
        path = originChild.gameObject.AddComponent<BGCcMath>();
        for (int i = 0; i < agentCurve.Points.Length; i++)
        {
            originChild.GetComponent<BGCurve>().AddPoint(new BGCurvePoint(curve, agentCurve.Points[i].PositionWorld, BGCurvePoint.ControlTypeEnum.Absent, true));
        }
    }
}
    