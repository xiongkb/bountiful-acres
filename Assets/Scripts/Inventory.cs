using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    public TMP_Text strawberryText;
    public int strawberryCount = 0;

    public TMP_Text carrotText;
    public int carrotCount = 0;

    public TMP_Text potatoText;
    public int potatoCount = 0;


    private void Awake() {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        strawberryText.SetText(strawberryCount.ToString());
        carrotText.SetText(carrotCount.ToString());
        potatoText.SetText(potatoCount.ToString());
    }

    public void addStrawberry(int num) {
        strawberryCount += num;

        strawberryText.SetText(strawberryCount.ToString());
    }

    public void addCarrot(int num) {
        carrotCount += num;

        carrotText.SetText(carrotCount.ToString());
    }

    public void addPotato(int num) {
        potatoCount += num;

        potatoText.SetText(potatoCount.ToString());
    }
}
