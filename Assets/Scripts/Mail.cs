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
    string crop;
    int num;
    public int letterNum;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetLetter(int newLetterNum, string newName, string newMessage, string newCrop, int newNum)
    {
        letterNum = newLetterNum;
        string generatedMessage = newMessage;
        generatedMessage = generatedMessage.Replace("<crop>", newCrop);
        generatedMessage = generatedMessage.Replace("<num>", newNum.ToString());

        tmpName.SetText(newName);

        tmpMessage.SetText(generatedMessage);
    }

    public void Accept()
    {
        acceptButton.gameObject.SetActive(false);
        rejectButton.gameObject.SetActive(false);
        shipButton.gameObject.SetActive(true);
    }
}
