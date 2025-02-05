using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmTool : MonoBehaviour
{
    [SerializeField] private GameObject tool;
    private bool dragging;
    private Vector2 mouseOffset;
    private GameObject plot;

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

        if (plot == null) return;

        switch(tool.name) {
            case "Hoe":
                FarmPlot farmPlot = (FarmPlot) plot.GetComponent(typeof(FarmPlot));
                farmPlot.Till();
                break;
            default:
                break;
        }        
    }

    Vector2 GetMousePos() {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.tag == "Plot") {
            plot = col.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D col) {
        if (col.tag == "Plot") {
            plot = null;
        }
    }
}
