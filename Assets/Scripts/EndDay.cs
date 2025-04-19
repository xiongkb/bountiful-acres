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
        if (!MailManager.instance.mailActive && Input.GetMouseButtonDown(0) && Utilities.instance.isOverlappingMouse(gameObject) && DaySystem.instance.NewDay()) {
            FarmPlot[] farmPlots = FindObjectsOfType(typeof(FarmPlot)) as FarmPlot[];
            for (int i = 0; i < farmPlots.Length; i++) farmPlots[i].NewDay();

            PlantGrowth[] plantGrowths = FindObjectsOfType(typeof(PlantGrowth)) as PlantGrowth[];
            for (int i = 0; i < plantGrowths.Length; i++) plantGrowths[i].NewDay();

            Manager.instance.NewDay();

            MailManager.instance.NewDay();
        }
    }

    Vector2 GetMousePos() {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
