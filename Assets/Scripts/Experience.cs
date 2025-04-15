using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Experience : MonoBehaviour
{
    [SerializeField] int experienceMultiplier;
    [SerializeField] Image expBarFill;
    public int maxExp;
    public static Experience instance;
    public TMP_Text experienceDisplay;
    public int experience = 0;
    bool lvl2 = false;
    bool lvl3 = false;

    void Awake()
    {
        instance = this;
    }

    public int calcExp(int num) {
        return num * experienceMultiplier;
    }
    
    public void AddExperience(int exp)
    {
        experience += calcExp(exp);
        experienceDisplay.SetText(experience.ToString());
        expBarFill.fillAmount = (float)experience / (float)maxExp;

        if (!lvl2 && (float)experience / (float)maxExp > 0.33f) {
            lvl2 = true;
            MailManager.instance.Level2();
        }
        if (!lvl3 && (float)experience / (float)maxExp > 0.66f) {
            lvl3 = true;
            MailManager.instance.Level3();
        }
    }
}
