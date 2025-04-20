using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private Camera cam;
   
    [SerializeField]
    private SpriteRenderer bgRenderer;
    
    private float bgMinX, bgMaxX, bgMinY, bgMaxY;

    private Vector3 dragOrigin;

    private void Awake ()
    {
        bgMinX = bgRenderer.transform.position.x - bgRenderer.bounds.size.x / 2f;
        bgMaxX = bgRenderer.transform.position.x + bgRenderer.bounds.size.x / 2f;

        bgMinY = bgRenderer.transform.position.y - bgRenderer.bounds.size.y / 2f;
        bgMaxY = bgRenderer.transform.position.y + bgRenderer.bounds.size.y / 2f;
    }
    // Update is called once per frame
    void Update()
    {
        PanCam();
    }

    private void PanCam()
    {
        if (!MailManager.instance.mailActive && Input.GetMouseButtonDown(1)) dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);

        if (!MailManager.instance.mailActive && Input.GetMouseButton(1)) {
            Vector3 diff = dragOrigin - cam.ScreenToWorldPoint(Input.mousePosition);
            // Debug.Log(diff);

            cam.transform.position = ClampCam(cam.transform.position + diff);

        }
    }

    private Vector3 ClampCam(Vector3 targetPos)
    {
        float camHeight = cam.orthographicSize;
        float camWidth = cam.orthographicSize * cam.aspect;

        float minX = bgMinX + camWidth;
        float maxX = bgMaxX - camWidth;
        float minY = bgMinY + camHeight;
        float maxY = bgMaxY - camHeight;

        float newX = Mathf.Clamp(targetPos.x, minX, maxX);
        float newY = Mathf.Clamp(targetPos.y, minY, maxY);

        return new Vector3(newX, newY, targetPos.z);
    }
}
