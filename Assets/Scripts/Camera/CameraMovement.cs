using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    private Vector3 dragOrigin;

    // Update is called once per frame
    void Update()
    {
        PanCam();
    }

    private void PanCam()
    {
        if (Input.GetMouseButtonDown(1)) dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButton(1)) {
            Vector3 diff = dragOrigin - cam.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log(diff);
            cam.transform.position += diff;
        }
    }
}
