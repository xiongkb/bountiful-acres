using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantGrowth : MonoBehaviour
{
    [SerializeField] private float speed = 0f; // plant growth speed

    public SpriteRenderer spriteRenderer;
    public Sprite[] stages;  // add the plant here
    public int stage = 0;

    private float currTime = 0f;
    private float updateTime;

    // Start is called before the first frame update
    void Start()
    {
        updateTime = speed;

        if (stages.Length > 0) {
            spriteRenderer.sprite = stages[0];
        }
    }

    // Update is called once per frame
    void Update() {
        if (stage < stages.Length - 1) {
            currTime += Time.deltaTime;

            if (currTime >= updateTime) {
                stage += 1;
                spriteRenderer.sprite = stages[stage];
                updateTime += speed;
            }
        }
        
    }
}
