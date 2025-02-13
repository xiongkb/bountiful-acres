using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmPlot : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite tilledSprite1;
    [SerializeField] private Sprite tilledSprite2;
    [SerializeField] private Sprite tilledSprite3;
    private Vector2 plotPos;
    // starting status of the plot
    private int tillLevel = 0;
    private bool isPlanted = false;
    private bool isWatered = false;
    private GameObject currentTool;

    // Start is called before the first frame update
    void Start()
    {
       plotPos = (Vector2)this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (currentTool != null && gameObject.GetComponent<Collider2D>().OverlapPoint(point)) {        
            Debug.Log("TEST20");
        FarmTool farmTool = (FarmTool) currentTool.GetComponent(typeof(FarmTool));
        farmTool.plot = gameObject;
        }
    }

    public void Till() {
        switch(tillLevel) {
            case 0:
                spriteRenderer.sprite = tilledSprite1;
                tillLevel = 1;
                break;
            case 1:
                spriteRenderer.sprite = tilledSprite2;
                tillLevel = 2;
                break;
            case 2:
                spriteRenderer.sprite = tilledSprite3;
                tillLevel = 3;
                break;
            default:
                break;
        }
    }

    public void Plant(GameObject seed) {
        if (tillLevel > 0) {
            Instantiate(seed, new Vector2(this.transform.position.x, this.transform.position.y), Quaternion.identity);
        }
    }

    public void Water() {

    }

    public void Harvest() {

    }

    void OnTriggerEnter2D(Collider2D col) {
        FarmTool farmTool = (FarmTool) col.GetComponent(typeof(FarmTool));
        if (farmTool != null) currentTool = col.gameObject;
    }

    void OnTriggerExit2D(Collider2D col) {
        if (col.tag != currentTool.tag) return;

        FarmTool farmTool = (FarmTool) col.GetComponent(typeof(FarmTool));
        if (farmTool != null) currentTool = null;
    }
}
