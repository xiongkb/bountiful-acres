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

    //Audio
    public AudioClip tillingSFX;
    public AudioClip wateringSFX;
    public AudioClip harvestingSFX;
    public AudioClip plantingSFX;
    AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        startPos = (Vector2)this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (MailManager.instance.mailActive) {
            dragging = false;
            this.transform.position = startPos;
        }

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
                            audioSource = GetComponent<AudioSource>();
                            audioSource.PlayOneShot(tillingSFX);

                            animator.Play("tilling");
                            farmPlot.Till();
                        }
                        break;
                    case "SeedBag":
                        if (Manager.instance.UseStamina(1)) {
                            audioSource = GetComponent<AudioSource>();
                            audioSource.PlayOneShot(plantingSFX);
                            animator.Play("Plant");
                            farmPlot.Plant(seed, crop);
                        }
                        break;
                    case "WateringCan":
                        if (Manager.instance.UseStamina(2)) {
                            audioSource = GetComponent<AudioSource>();
                            audioSource.PlayOneShot(wateringSFX);
                            animator.Play("Watering");
                            farmPlot.Water();
                        }
                        break;
                    case "Scythe":
                        if (Manager.instance.UseStamina(3)) {
                            audioSource = GetComponent<AudioSource>();
                            audioSource.PlayOneShot(harvestingSFX);
                            animator.Play("harvesting");
                            farmPlot.Harvest();
                        }
                        break;
                    default:
                        break;
                }  
            }
        }
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
