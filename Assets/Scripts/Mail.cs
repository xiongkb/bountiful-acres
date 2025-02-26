using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Mail : MonoBehaviour
{
    [SerializeField] TMP_Text tmpName;
    [SerializeField] TMP_Text tmpMessage;
    string crop;
    int num;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetLetter(string newName, string newMessage, string newCrop, int newNum)
    {
        string generatedMessage = newMessage;
        generatedMessage = generatedMessage.Replace("<crop>", newCrop);
        generatedMessage = generatedMessage.Replace("<num>", newNum.ToString());

        tmpName.SetText(newName);

        tmpMessage.SetText(generatedMessage);
    }
}
