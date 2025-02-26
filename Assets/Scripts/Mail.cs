using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Mail : MonoBehaviour
{
    [SerializeField] TMP_Text tmpName;
    [SerializeField] TMP_Text tmpMessage;
    [SerializeField] Button acceptButton;
    [SerializeField] Button rejectButton;
    [SerializeField] Button shipButton;
    public Button leftButton;
    public Button rightButton;
    public Canvas mailCanvas;
    string crop;
    int num;
    public int letterNum;

    public void SetLetter(int newLetterNum, string newName, string newMessage, string newCrop, int newNum)
    {
        letterNum = newLetterNum;
        string generatedMessage = newMessage;
        generatedMessage = generatedMessage.Replace("<crop>", newCrop);
        generatedMessage = generatedMessage.Replace("<num>", newNum.ToString());

        tmpName.SetText(newName);

        tmpMessage.SetText(generatedMessage);

        if(letterNum == 0)
            leftButton.interactable = false;

        rightButton.interactable = false;
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

    public void PreviousLetter() {
        MailManager.instance.SetActiveLetter(letterNum - 1);
    }

    public void NextLetter() {
        MailManager.instance.SetActiveLetter(letterNum + 1);
    }
}
