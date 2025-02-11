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

    // Start is called before the first frame update
    void Start()
    {
       plotPos = (Vector2)this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

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
        if (tillLevel > 0) Instantiate(seed, new Vector2(this.transform.position.x, this.transform.position.y), Quaternion.identity);
    }
}
