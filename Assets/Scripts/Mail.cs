using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Mail : MonoBehaviour
{
    [SerializeField] TMP_Text tmpName;
    [SerializeField] TMP_Text tmpMessage;
    [SerializeField] TMP_Text tmpExpiration;
    [SerializeField] Button acceptButton;
    [SerializeField] Button rejectButton;
    [SerializeField] Button shipButton;
    public Button leftButton;
    public Button rightButton;
    Dictionary<string, int> crops;
    int daysLeft;
    public int letterNum;

    void Update()
    {
        if (
            Inventory.instance.strawberryCount >= crops["strawberrie"] &&
            Inventory.instance.carrotCount >= crops["carrot"] &&
            Inventory.instance.potatoCount >= crops["potatoe"]
        ) shipButton.interactable = true;
        else shipButton.interactable = false;

        if(letterNum == 0) leftButton.interactable = false;
    }

    public void SetLetter(int newLetterNum, string newName, string newMessage, Dictionary<string, int> newCrops, int days)
    {
        letterNum = newLetterNum;
        crops = newCrops;

        tmpName.SetText(newName);

        tmpMessage.SetText(newMessage);

        SetDays(days);

        if(letterNum == 0)
            leftButton.interactable = false;

        rightButton.interactable = false;
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void Accept()
    {
        acceptButton.gameObject.SetActive(false);
        rejectButton.gameObject.SetActive(false);
        shipButton.gameObject.SetActive(true);
    }

    public void Reject()
    {
       MailManager.instance.RemoveLetter(letterNum);
    }

    public void Ship()
    {
        Inventory.instance.addStrawberry(-crops["strawberrie"]);
        Inventory.instance.addCarrot(-crops["carrot"]);
        Inventory.instance.addPotato(-crops["potatoe"]);

        Experience.instance.AddExperience(crops["strawberrie"] + crops["carrot"] + crops["potatoe"]);
        MailManager.instance.RemoveLetter(letterNum);
    }

    public void PreviousLetter() {
        MailManager.instance.SetActiveLetter(letterNum - 1);
    }

    public void NextLetter() {
        MailManager.instance.SetActiveLetter(letterNum + 1);
    }

    void SetDays(int days) {
        daysLeft = days;

        if (days != -1) tmpExpiration.SetText(daysLeft.ToString() + " Days Left");
        else tmpExpiration.SetText("∞ Days Left");
    }

    public bool NewDay() {
        if (daysLeft == -1) return true;

        daysLeft--;

        if (daysLeft > 0) {
            tmpExpiration.SetText(daysLeft.ToString() + " Days Left");
            return true;
        }
            else return false;
    }
}
