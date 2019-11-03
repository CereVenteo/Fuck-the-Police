using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{

    private Vector3 cameraFollowPosition;

    // Update is called once per frame
    void Update()
    {
        float moveAmount = 10f;
        float edgeSize = 30f;
        cameraFollowPosition = transform.position;

        if (Input.mousePosition.x > Screen.width - edgeSize)
        {
            cameraFollowPosition.z -= moveAmount * Time.deltaTime;
        }
        if (Input.mousePosition.x < edgeSize)
        {
            cameraFollowPosition.z += moveAmount * Time.deltaTime;
        }
        if (Input.mousePosition.y > Screen.height - edgeSize)
        {
            cameraFollowPosition.x += moveAmount * Time.deltaTime;
        }
        if (Input.mousePosition.y <  edgeSize)
        {
            cameraFollowPosition.x -= moveAmount * Time.deltaTime;
        }

        transform.position = cameraFollowPosition;
    }
}
