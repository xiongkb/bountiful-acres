using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Land : MonoBehaviour
{
    [SerializeField] TMP_Text tmpCost;
    [SerializeField] GameObject farmPlot;
    int cost;
    // Start is called before the first frame update
    void Start()
    {
        cost = int.Parse(tmpCost.text.Remove(0, 1));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Utilities.instance.isOverlappingMouse(gameObject)) {
            if (Money.instance.money >= cost) {
                Money.instance.SubtractMoney(cost);
                farmPlot.SetActive(true);
                gameObject.SetActive(false);
            }
        }
    }
}
