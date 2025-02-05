using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmTool : MonoBehaviour
{
    private bool dragging;
    private Vector2 mouseOffset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!dragging) return;

        var mousePos = GetMousePos();

        transform.position = mousePos - mouseOffset;
    }

    void OnMouseDown() {
        dragging = true;
        mouseOffset = GetMousePos() - (Vector2)transform.position;
    }

    Vector2 GetMousePos() {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
