using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndDay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && gameObject.GetComponent<Collider2D>().OverlapPoint(GetMousePos())) {
            FarmPlot[] farmPlots = FindObjectsOfType(typeof(FarmPlot)) as FarmPlot[];
            for (int i = 0; i < farmPlots.Length; i++) farmPlots[i].NewDay();

            PlantGrowth[] plantGrowths = FindObjectsOfType(typeof(PlantGrowth)) as PlantGrowth[];
            for (int i = 0; i < plantGrowths.Length; i++) plantGrowths[i].NewDay();
        }
    }

    Vector2 GetMousePos() {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
