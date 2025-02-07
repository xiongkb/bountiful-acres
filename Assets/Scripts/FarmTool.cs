using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmTool : MonoBehaviour
{
    [SerializeField] private GameObject tool;
    [SerializeField] private Sprite seedSprite;
    private Vector2 startPos;
    private bool dragging;
    private Vector2 mouseOffset;
    private GameObject plot;
    private bool onToolShed = false;

    // Start is called before the first frame update
    void Start()
    {
        startPos = (Vector2)this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!dragging) return;

        var mousePos = GetMousePos();

        transform.position = mousePos - mouseOffset;
    }

    void OnMouseDown() {
        if (dragging == false) {
            dragging = true;
            mouseOffset = GetMousePos() - (Vector2)transform.position;
        } else if (plot != null) {
            switch(tool.name) {
                case "Hoe":
                    FarmPlot farmPlot = (FarmPlot) plot.GetComponent(typeof(FarmPlot));
                    farmPlot.Till();
                    break;
                default:
                    break;
            }  
        } else if (onToolShed) {
            dragging = false;
            this.transform.position = startPos;
        }
    }

    Vector2 GetMousePos() {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.tag == "Plot") plot = col.gameObject;
            else if (col.tag == "ToolShed") onToolShed = true;
    }

    void OnTriggerExit2D(Collider2D col) {
        if (col.tag == "Plot") plot = null;
            else if (col.tag == "ToolShed") onToolShed = false;
    }
}
