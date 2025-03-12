using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DaySystem : MonoBehaviour
{
    public static DaySystem instance;
    public TMP_Text dayCountText;
    public int maxDayCount;
    int dayCount = 1;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        dayCountText.SetText(dayCount.ToString());
    }

    public bool NewDay() {
        if (dayCount == maxDayCount) return false;

        dayCount++;
        dayCountText.SetText(dayCount.ToString());
        return true;
    }
}
