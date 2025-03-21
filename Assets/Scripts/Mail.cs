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
    string crop;
    int num;
    int daysLeft;
    public int letterNum;

    void Update()
    {
        int currentCropNum = 0;

        switch (crop) {
            case "strawberrie":
                currentCropNum = Inventory.instance.strawberryCount;
                break;
            case "carrot":
                currentCropNum = Inventory.instance.carrotCount;
                break;
            case "potatoe":
                currentCropNum = Inventory.instance.potatoCount;
                break;
            default:
                break;
        }

        if(currentCropNum >= num)
            shipButton.interactable = true;
        else
            shipButton.interactable = false;

        if(letterNum == 0)
            leftButton.interactable = false;
    }

    public void SetLetter(int newLetterNum, string newName, string newMessage, string newCrop, int newNum, int days)
    {
        letterNum = newLetterNum;
        crop = newCrop;
        string generatedMessage = newMessage;
        generatedMessage = generatedMessage.Replace("<crop>", newCrop);
        generatedMessage = generatedMessage.Replace("<num>", newNum.ToString());
        num = newNum;

        tmpName.SetText(newName);

        tmpMessage.SetText(generatedMessage);

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
        switch (crop) {
            case "strawberrie":
                Inventory.instance.addStrawberry(-num);
                break;
            case "carrot":
                Inventory.instance.addCarrot(-num);
                break;
            case "potatoe":
                Inventory.instance.addPotato(-num);
                break;
            default:
                break;
        }

        Experience.instance.AddExperience(num);
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
        tmpExpiration.SetText(daysLeft.ToString() + " Days Left");
    }

    public bool NewDay() {
        daysLeft--;

        if (daysLeft > 0) {
            tmpExpiration.SetText(daysLeft.ToString() + " Days Left");
            return true;
        }
            else return false;
    }
}
