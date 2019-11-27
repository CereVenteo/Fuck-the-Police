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
        uint limit = 20;
        cameraFollowPosition = transform.position;

        if ((Input.mousePosition.x > Screen.width - edgeSize || Input.GetKey(KeyCode.D)) && cameraFollowPosition.z > -limit)
        {
            cameraFollowPosition.z -= moveAmount * Time.deltaTime;
        }
        if ((Input.mousePosition.x < edgeSize || Input.GetKey(KeyCode.A)) && cameraFollowPosition.z < limit)
        {
            cameraFollowPosition.z += moveAmount * Time.deltaTime;
        }
        if ((Input.mousePosition.y > Screen.height - edgeSize || Input.GetKey(KeyCode.W)) && cameraFollowPosition.x < limit)
        {
            cameraFollowPosition.x += moveAmount * Time.deltaTime;
        }
        if ((Input.mousePosition.y <  edgeSize || Input.GetKey(KeyCode.S)) && cameraFollowPosition.x > -limit)
        {
            cameraFollowPosition.x -= moveAmount * Time.deltaTime;
        }

        transform.position = cameraFollowPosition;
    }
}
