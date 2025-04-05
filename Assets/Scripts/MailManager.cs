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

    List<Dictionary<string, string>> availableTasks = new List<Dictionary<string, string>>();

    List<Dictionary<string, string>> lvl1 = new List<Dictionary<string, string>> {
        new Dictionary<string, string> {
            {"name", "Johnathan"},
            {"msg", "Hey there, I heard you're the new owner now. I used to be best pal with your Pa and loved the produce he grew. They were always the best in town. I run a small shop in town and would love to buy a batch of 50 strawberries and 20 potatoes. I have high hopes that your crops will be just as good if not better than your Pa!"},
            {"days", "-1"},
            {"strawberrie", "50"},
            {"potatoe", "20"},
            {"carrot", "0"}
        },
        new Dictionary<string, string> {
            {"name", "Janey"},
            {"msg", "Hello neighbor! It's nice to see the farm starting up again. If you're able to send me 20 carrots, I'll pay you well! My horses could use some nice home grown treats!"},
            {"days", "-1"},
            {"strawberrie", "0"},
            {"potatoe", "0"},
            {"carrot", "20"}
        },
        new Dictionary<string, string> {
            {"name", "Obbie"},
            {"msg", "I heard from your father that you taking over. Finally coming back to your roots eh? I'll be one of your first client and buy 10 of each crops. I'll cook them up in my restaurant so stop by some times."},
            {"days", "-1"},
            {"strawberrie", "10"},
            {"potatoe", "10"},
            {"carrot", "10"}
        }
    };

    List<Dictionary<string, string>> lvl2 = new List<Dictionary<string, string>> {
        new Dictionary<string, string> {
            {"name", "Susie B"},
            {"msg", "Nice to see a new face in town. I've heard you been selling really delicious strawberries. Can you send me 5? I'd like to sample them soon and see what the hype is all about."},
            {"days", "3"},
            {"strawberrie", "5"},
            {"potatoe", "0"},
            {"carrot", "0"}
        },
        new Dictionary<string, string> {
            {"name", "Brock"},
            {"msg", "I was told you sold carrots. Can you send me some immediately? I'm low on some stocks for my farm animals. Just 50 would be great! Thanks."},
            {"days", "1"},
            {"strawberrie", "0"},
            {"potatoe", "0"},
            {"carrot", "50"}
        },
        new Dictionary<string, string> {
            {"name", "Wils"},
            {"msg", "Dear farmer, after hearing how dilicious your crops were, I'd love to purchase a stock of 20 strawberries and carrots. I will send a thank you cake once my bakery is doing better."},
            {"days", "-1"},
            {"strawberrie", "20"},
            {"potatoe", "0"},
            {"carrot", "20"}
        },
        new Dictionary<string, string> {
            {"name", "Heathes"},
            {"msg", "Do you sell potatoes? I need 15 of them... You should stop by my shop some time. I would love to get to know you better over some fries."},
            {"days", "-1"},
            {"strawberrie", "0"},
            {"potatoe", "15"},
            {"carrot", "0"}
        },
    };

    List<Dictionary<string, string>> lvl3 = new List<Dictionary<string, string>> {
        new Dictionary<string, string> {
            {"name", "Johnathan3"},
            {"msg", "Hey there, I heard you're the new owner now. I run a small shop in town and would like to buy 1 strawberries and 2 potatoes. Hope you're able to send me some soon!"},
            {"days", "-1"},
            {"strawberrie", "1"},
            {"potatoe", "2"},
            {"carrot", "0"}
        },
        new Dictionary<string, string> {
            {"name", "Janey3"},
            {"msg", "Hello! Can I buy some carrots? I need 3 to order to feed my farm animals."},
            {"days", "1"},
            {"strawberrie", "0"},
            {"potatoe", "0"},
            {"carrot", "3"}
        },
        new Dictionary<string, string> {
            {"name", "Obbie3"},
            {"msg", "I heard from your Pop that you taking over. Send me 3 of your finest strawberries! Let's see how yours taste like."},
            {"days", "2"},
            {"strawberrie", "3"},
            {"potatoe", "0"},
            {"carrot", "0"}
        },
        new Dictionary<string, string> {
            {"name", "Susie3"},
            {"msg", "Do you sell potatoes? If so, I'll take 3. I have a restaurant using the local goods. Let's see how yours fare."},
            {"days", "3"},
            {"strawberrie", "0"},
            {"potatoe", "3"},
            {"carrot", "0"}
        }
    };

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        availableTasks.AddRange(lvl1);
        letters = new Mail[numLetters];
        if (numLetters > 0) GenerateLetter();
    }

    public void Level2() {
        availableTasks.AddRange(lvl2);
    }

    public void Level3() {
        availableTasks.AddRange(lvl3);
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
        if (availableTasks.Count < 1) return;

        for (int i = 0; i < letters.Length; i++)
        {
            if (letters[i] == null)
            {
                int taskIndex = Random.Range(0, availableTasks.Count);
                Dictionary<string, string> task = availableTasks[taskIndex];
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

                availableTasks.RemoveAt(taskIndex);

                break;
            }
        }

        Mailbox.instance.SetFull();
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

        if (letters[0] == null) Mailbox.instance.SetEmpty();
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
