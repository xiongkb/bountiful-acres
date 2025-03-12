using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Experience : MonoBehaviour
{
    [SerializeField] int experienceMultiplier;
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
    }
}
