using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmPlot : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite tilledSprite1;
    [SerializeField] private Sprite tilledSprite2;
    [SerializeField] private Sprite tilledSprite3;
    [SerializeField] int sproutTime;
    // starting status of the plot
    private int tillLevel = 0;
    private int waterLevel = 0;
    private bool isPlanted = false;
    private bool isGrowing = false;
    private GameObject plantedSeed;
    private GameObject seedCrop;
    private GameObject plantedCrop;
    bool sprouting = false;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (plantedCrop != null && Input.GetMouseButtonDown(0) && Utilities.instance.isOverlappingMouse(gameObject)) {
            PlantGrowth plantGrowth = ((PlantGrowth) plantedCrop.GetComponent(typeof(PlantGrowth)));
            if(!MailManager.instance.mailActive) plantGrowth.CheckBug(Utilities.instance.GetMousePos());
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

    public void Plant(GameObject seed, GameObject crop) {
        if (tillLevel > 0 && plantedSeed == null && plantedCrop == null) {
            plantedSeed = Instantiate(seed, new Vector2(this.transform.position.x, this.transform.position.y), Quaternion.identity);
            seedCrop = crop;

            isPlanted = true;

            if (waterLevel > 0 && !sprouting && !isGrowing) StartCoroutine(Grow());
        }
    }

    public void Water() {
        if (tillLevel > 0 && waterLevel < 3) {
            
            waterLevel++;
            float tint = 1 - waterLevel * .2f;
            spriteRenderer.color = new Color(tint, tint, tint);

            if (isPlanted && !sprouting && !isGrowing) StartCoroutine(Grow());
        }
    }

    IEnumerator Grow() {
        sprouting = true;

        yield return new WaitForSeconds(sproutTime);

        Destroy(plantedSeed, 0);

        sprouting = false;

        plantedCrop = Instantiate(seedCrop, new Vector2(this.transform.position.x, this.transform.position.y + .7f), Quaternion.identity);
        isGrowing = true;
    }

    public void Harvest() {
        if (!isPlanted || plantedCrop == null) return;
        
        PlantGrowth plantGrowth = ((PlantGrowth) plantedCrop.GetComponent(typeof(PlantGrowth)));
        int growthStages = plantGrowth.stages.Length;
        int growthStage = plantGrowth.stage;

        if (growthStage == growthStages - 1) {
            plantGrowth.SelfDestruct(tillLevel, waterLevel);

            waterLevel = 0;
            spriteRenderer.color = new Color(1, 1, 1);
            isPlanted = false;
            isGrowing = false;
            plantedSeed = null;
            seedCrop = null;
            plantedCrop = null;
        }
    }

    void OnTriggerStay2D(Collider2D col) {
        FarmTool farmTool = (FarmTool) col.GetComponent(typeof(FarmTool));

        if (farmTool != null) {
            // Debug.Log(farmTool.plot.name);
            if (gameObject.GetComponent<Collider2D>().OverlapPoint(GetMousePos())) farmTool.plot = gameObject;
                else if (farmTool.plot != null) {
                    if (farmTool.plot.name == gameObject.name) farmTool.plot = null;
                }
        }
    }

    Vector2 GetMousePos() {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    
    public void NewDay() {
        waterLevel = 0;
        spriteRenderer.color = new Color(1, 1, 1);
    }
}
