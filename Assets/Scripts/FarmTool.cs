using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmTool : MonoBehaviour
{
    [SerializeField] private GameObject tool;
    [SerializeField] private GameObject seed;
    private Vector2 startPos;
    private bool dragging;
    private Vector2 mouseOffset;
    public GameObject plot;
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

        transform.position = GetMousePos();
    }

    void OnMouseDown() {
        if (dragging == false) {
            dragging = true;
            transform.position = GetMousePos();
        } else if (plot != null) {
            FarmPlot farmPlot = (FarmPlot) plot.GetComponent(typeof(FarmPlot));

            // tool functions
            switch(tool.tag) {
                case "Hoe":
                    farmPlot.Till();
                    break;
                case "SeedBag":
                    farmPlot.Plant(seed);
                    break;
                case "WateringCan":
                    farmPlot.Water();
                    break;
                case "Scythe":
                    farmPlot.Harvest();
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
        // Debug.Log("Enter: " + col.tag);
        // if (col.tag == "Plot") plot = col.gameObject;
        //     else
            if (col.tag == "ToolShed") onToolShed = true;
    }

    void OnTriggerExit2D(Collider2D col) {
        // Debug.Log("Exit: " + col.tag);
        // if (col.tag == "Plot" && col.name == plot.name) plot = null;
        //     else
            if (col.tag == "ToolShed") onToolShed = false;
    }
}
