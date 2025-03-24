using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailManager : MonoBehaviour
{
    public static MailManager instance;
    [SerializeField] Mail mailLetterPrefab;
    public int numLetters;
    [SerializeField] int minNum;
    [SerializeField] int maxNum;
    [SerializeField] string[] names;
    [SerializeField] string[] messages;
    string[] crops;
    public Mail[] letters = { null, null, null, null, null };
    float currTime = 0f;
    float lastMailTime = 0f;
    [SerializeField] float newMailTime;
    [SerializeField] int minDays;
    [SerializeField] int maxDays;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        crops = Manager.instance.crops;
        letters = new Mail[numLetters];
        if (numLetters > 0) GenerateLetter();
    }

    public void NewDay() {
        float mailChance = 20f + (float)DaySystem.instance.dayCount + ((float)Experience.instance.experience / 1000f);

        int letterNum = 0;

        while (letterNum < letters.Length) {
            if (letters[letterNum] != null) {
                if (!letters[letterNum].NewDay()) {
                    RemoveLetter(letterNum);
                    letterNum--;
                }
            }

            letterNum++;
        }

        // for (int i = 0; i < letters.Length; i++) {
        //     if (letters[i] != null) {
        //         if (!letters[i].NewDay();)
        //     }
        // }

        if (letters[0] == null) GenerateLetter();

        for (int i = 1; i < letters.Length; i++) {
            if (letters[i] == null) {
                float randNum = Random.Range(0f, 100f);

                if (100f - randNum <= mailChance) GenerateLetter();
            }
        }
    }

    void GenerateLetter()
    {
        for (int i = 0; i < letters.Length; i++)
        {
            if (letters[i] == null)
            {
                string name = names[Random.Range(0, names.Length)];
                string message = messages[Random.Range(0, messages.Length)];
                string crop = crops[Random.Range(0, crops.Length)];
                int num = Random.Range(minNum, maxNum);
                Vector2 mailPos = Camera.main.ViewportToWorldPoint(new Vector2(0.5f, 0.5f));
                Mail mail = Instantiate(mailLetterPrefab, mailPos, Quaternion.identity) as Mail;
                letters[i] = mail;
                int numDays = Random.Range(minDays, maxDays + 1);

                mail.SetLetter(i, name, message, crop, num, numDays);
                mail.gameObject.SetActive(false);

                if (i > 0)
                    letters[i - 1].rightButton.interactable = true;

                break;
            }
        }
    }

    public void RemoveLetter(int letterNum)
    {
        bool wasActive = letters[letterNum].gameObject.activeSelf;
        Destroy(letters[letterNum].gameObject);
        letters[letterNum] = null;

        for(int i = letterNum; i < letters.Length - 1; i++)
        {
            letters[i] = letters[i + 1];
            
            if(letters[i] == null)
                break;
            else
            {
                letters[i].letterNum = i;

                if(i == letters.Length - 2)
                    letters[letters.Length - 1] = null;
            }
        }

        if(letters[letterNum] != null)
        {
            if (wasActive) letters[letterNum].gameObject.SetActive(true);

            for(int i = letters.Length - 1; i >= 0; i--)
            {
                if(letters[i] != null)
                {
                    letters[i].rightButton.interactable = false;

                    if(i == 0)
                        letters[0].leftButton.interactable = false;

                    break;
                }
            }
        }
        else if(letterNum > 0)
        {
            if (wasActive) letters[letterNum - 1].gameObject.SetActive(true);

            letters[letterNum - 1].rightButton.interactable = false;
        }
    }

    public void SetActiveLetter(int letterNum)
    {
        for(int i = 0; i < letters.Length; i++)
        {
            if(letters[i] != null)
            {
                
                if(i == letterNum)
                {
                    letters[i].gameObject.SetActive(true);
                }
                else
                {
                    letters[i].gameObject.SetActive(false);
                }
            }
        }
    }
}
