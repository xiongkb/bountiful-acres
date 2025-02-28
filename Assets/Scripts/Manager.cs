using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Manager : MonoBehaviour
{
    // player info
    public static Manager instance;
    public string[] crops;
    public TMP_Text staminaText;
    public int maxStamina;
    int stamina;

    private void Awake() {
        instance = this;
    }

    public bool UseStamina(int num) {
        if (stamina - num < 0) return false;

        stamina -= num;

        updateStaminaText();

        return true;
    }

    void updateStaminaText() {
        staminaText.SetText(stamina.ToString() + "/" + maxStamina.ToString());
    }

    // Start is called before the first frame update
    void Start()
    {
        stamina = maxStamina;

        updateStaminaText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
