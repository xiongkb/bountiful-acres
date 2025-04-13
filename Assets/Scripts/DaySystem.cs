using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DaySystem : MonoBehaviour
{
    public static DaySystem instance;
    public TMP_Text dayCountText;
    public int maxDayCount;
    public int dayCount = 1;


    private void Awake() {
        instance = this;
    }

    private void Start() {
        dayCountText.SetText(dayCount.ToString());
    }

    public bool NewDay() {
        if (dayCount == maxDayCount) {
            if (Experience.instance.experience >= Experience.instance.maxExp) SceneManager.LoadScene(4);
            else SceneManager.LoadScene(5);
            return false;
        }

        StartCoroutine(GameSceneManager.instance.FadeOut());
        dayCount++;
        dayCountText.SetText(dayCount.ToString());
        StartCoroutine(GameSceneManager.instance.FadeIn());
        return true;
    }
}
