using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmTool : MonoBehaviour
{
    [SerializeField] private GameObject tool;
    [SerializeField] private GameObject seed;
    [SerializeField] private GameObject crop;
    private Vector2 startPos;
    private bool dragging;
    private Vector2 mouseOffset;
    public GameObject plot;
    private bool onToolShed = false;

    [SerializeField] private Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        startPos = (Vector2)this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (dragging) transform.position = GetMousePos();

        if (Input.GetMouseButtonDown(0) && gameObject.GetComponent<Collider2D>().OverlapPoint(GetMousePos())) {
            if (dragging == false) {
                dragging = true;
                transform.position = GetMousePos();
            } else if (onToolShed) {
                dragging = false;
                this.transform.position = startPos;
            } else if (plot != null && plot.gameObject.tag == "Plot") {
                FarmPlot farmPlot = (FarmPlot) plot.GetComponent(typeof(FarmPlot));

                // tool functions
                switch(tool.tag) {
                    case "Hoe":
                        if (Manager.instance.UseStamina(2)) {
                            animator.SetBool("isTilling", true);
                            StartCoroutine(StopAnimation("isTilling"));
                            farmPlot.Till();
                        }
                        break;
                    case "SeedBag":
                        if (Manager.instance.UseStamina(1)) farmPlot.Plant(seed, crop);
                        break;
                    case "WateringCan":
                        if (Manager.instance.UseStamina(2)) {
                            animator.SetBool("isWatering", true);
                            StartCoroutine(StopAnimation("isWatering"));
                            farmPlot.Water();
                        }
                        break;
                    case "Scythe":
                        if (Manager.instance.UseStamina(3)) {
                            animator.SetBool("isHarvesting", true);
                            StartCoroutine(StopAnimation("isHarvesting"));
                            farmPlot.Harvest();
                        }
                        break;
                    default:
                        break;
                }  
            }
        }
    }

    IEnumerator StopAnimation(string animationBool) {
        yield return new WaitForSeconds(0.5f);

        animator.SetBool(animationBool, false);
    }

    void OnMouseDown() {
        
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
