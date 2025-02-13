using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmPlot : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite tilledSprite1;
    [SerializeField] private Sprite tilledSprite2;
    [SerializeField] private Sprite tilledSprite3;
    // starting status of the plot
    private int tillLevel = 0;
    private int waterLevel = 0;
    private bool isPlanted = false;
    private bool isWatered = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // if (currentTool != null && !gameObject.GetComponent<Collider2D>().OverlapPoint(GetMousePos())) {
        //     FarmTool farmTool = (FarmTool) currentTool.GetComponent(typeof(FarmTool));
        //     farmTool.plot = null;
        //     currentTool = null;
        // }
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
        if (tillLevel > 0 && waterLevel < 3) {
            waterLevel++;
            float tint = 1 - waterLevel * .2f;
            spriteRenderer.color = new Color(tint, tint, tint);
        }
    }

    public void Harvest() {

    }

    void OnTriggerStay2D(Collider2D col) {
        FarmTool farmTool = (FarmTool) col.GetComponent(typeof(FarmTool));

        if (farmTool != null) {
            // Debug.Log(farmTool.plot.name);
            if (gameObject.GetComponent<Collider2D>().OverlapPoint(GetMousePos())) farmTool.plot = gameObject;
                else if (farmTool.plot.name == gameObject.name) farmTool.plot = null;
        }
    }

    Vector2 GetMousePos() {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    // void OnTriggerEnter2D(Collider2D col) {
    //     FarmTool farmTool = (FarmTool) col.GetComponent(typeof(FarmTool));

    //     if (farmTool != null) {
    //         if (gameObject.GetComponent<Collider2D>().OverlapPoint(GetMousePos())) farmTool.plot = gameObject;
    //     }
    // }

    // void OnTriggerExit2D(Collider2D col) {
    //     Debug.Log("TEST100");
    //     FarmTool farmTool = (FarmTool) col.GetComponent(typeof(FarmTool));

    //     if (farmTool != null && farmTool.plot.name == gameObject.name) farmTool.plot = null;
    // }
}
