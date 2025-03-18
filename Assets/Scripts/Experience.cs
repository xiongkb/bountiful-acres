using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Experience : MonoBehaviour
{
    [SerializeField] int experienceMultiplier;
    [SerializeField] Image expBarFill;
    [SerializeField] int maxExp;
    public static Experience instance;
    public TMP_Text experienceDisplay;
    public int experience = 0;

    void Awake()
    {
        instance = this;
    }
    
    public void AddExperience(int exp)
    {
        experience += exp * experienceMultiplier;
        experienceDisplay.SetText(experience.ToString());
        expBarFill.fillAmount = (float)experience / (float)maxExp;
    }
}
