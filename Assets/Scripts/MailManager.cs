using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailManager : MonoBehaviour
{
    public static MailManager instance;
    [SerializeField] Mail mailLetterPrefab;
    public int numLetters;
    public Mail[] letters = { null, null, null, null, null };
    float currTime = 0f;
    float lastMailTime = 0f;
    [SerializeField] float newMailTime;

    List<Dictionary<string, string>> lvl1 = new List<Dictionary<string, string>> {
        new Dictionary<string, string> {
            {"name", "Johnathan"},
            {"msg", "Hey there, I heard you're the new owner now. I run a small shop in town and would like to buy 4 strawberries and 5 potatoes. Hope you're able to send me some soon!"},
            {"days", "-1"},
            {"strawberrie", "4"},
            {"potatoe", "5"},
            {"carrot", "0"}
        },
        new Dictionary<string, string> {
            {"name", "Janey"},
            {"msg", "Hello! Can I buy some carrots? I need 3 to order to feed my farm animals."},
            {"days", "2"},
            {"strawberrie", "0"},
            {"potatoe", "0"},
            {"carrot", "3"}
        },
        new Dictionary<string, string> {
            {"name", "Obbie"},
            {"msg", "I heard from your Pop that you taking over. Send me 2 of your finest strawberries! Let's see how yours taste like."},
            {"days", null},
            {"strawberrie", "2"},
            {"potatoe", "0"},
            {"carrot", "0"}
        },
        new Dictionary<string, string> {
            {"name", "Susie"},
            {"msg", "Do you sell potatoes? If so, I'll take 1. I have a restaurant using the local goods. Let's see how yours fare."},
            {"days", "1"},
            {"strawberrie", "0"},
            {"potatoe", "1"},
            {"carrot", "0"}
        }
    };

    void Awake()
    {
        // Debug.Log(lvl1[0]["msg"]);
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        letters = new Mail[numLetters];
        if (numLetters > 0) GenerateLetter();
    }

    // void Update() {Debug.Log(DaySystem.instance.dayCount);}

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
                Dictionary<string, string> task = lvl1[0];
                string name = task["name"];
                string message = task["msg"];
                Vector2 mailPos = Camera.main.ViewportToWorldPoint(new Vector2(0.5f, 0.5f));
                Mail mail = Instantiate(mailLetterPrefab, mailPos, Quaternion.identity) as Mail;
                letters[i] = mail;
                int numDays = int.Parse(task["days"]);

                Dictionary<string, int> taskCrops = new Dictionary<string, int> {
                    {"strawberrie", int.Parse(task["strawberrie"])},
                    {"potatoe", int.Parse(task["potatoe"])},
                    {"carrot", int.Parse(task["carrot"])}
                };
                
                mail.SetLetter(i, name, message, taskCrops, numDays);
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
