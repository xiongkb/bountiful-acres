using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    public TMP_Text strawberryText;
    int strawberryCount = 0;

    private void Awake() {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        strawberryText.SetText(strawberryCount.ToString());
    }

    public void addStrawberry() {
        strawberryCount++;

        strawberryText.SetText(strawberryCount.ToString());
    }
}
