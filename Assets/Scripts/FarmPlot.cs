using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmPlot : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    // starting status of the plot
    private bool isTilled = false;
    private bool isPlanted = false;
    private bool isWatered = false;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Till() {
        if (isTilled) return;

        spriteRenderer.color = new Color(0.65f, .16f, .16f, 1f);
        isTilled = true;
    }
}
