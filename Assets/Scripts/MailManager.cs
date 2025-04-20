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
   public bool mailActive = false;


   List<Dictionary<string, string>> availableTasks = new List<Dictionary<string, string>>();


   // level 1, first few days
   List<Dictionary<string, string>> lvl1 = new List<Dictionary<string, string>> {
       new Dictionary<string, string> {
           {"name", "Johnathan"},
           {"msg", "Hey there, I heard you're the new owner now. I used to be best pals with your Pa and loved the berries he grew. They were always the best in town. I run a small shop in town and would love to buy a batch of 10 strawberries and 20 potatoes. I have high hopes that your crops will be just as good, if not better than your Pa’s!"},
           {"days", "-1"},
           {"strawberrie", "10"},
           {"potatoe", "20"},
           {"carrot", "0"}
       },
       new Dictionary<string, string> {
           {"name", "Janey"},
           {"msg", "Hello neighbor! It's nice to see the farm running again. I run an equestrian ranch if you ever have the need to ride a horse. If you're able to send me 20 carrots, I'll pay you well! Your dad told me all about you, so I have high expectations."},
           {"days", "-1"},
           {"strawberrie", "0"},
           {"potatoe", "0"},
           {"carrot", "20"}
       },
       new Dictionary<string, string> {
           {"name", "Obbie"},
           {"msg", "I heard from your father that you are taking over the land now. Finally coming back to your roots eh? I'll be one of your first clients and buy 10 of each crop. I'll cook them up in my restaurant, so stop by sometimes."},
           {"days", "-1"},
           {"strawberrie", "10"},
           {"potatoe", "10"},
           {"carrot", "10"}
       }
   };


   // level 2, possibly middle days, when stars are over 33%
   List<Dictionary<string, string>> lvl2 = new List<Dictionary<string, string>> {
       new Dictionary<string, string> {
           {"name", "Tasha Hex"},
           {"msg", "Hello, I’ve been hearing you’re an up and coming new farm. I’m rather new here in Ha’waru so it would be nice to get to know you. I’m opening a small pastry shop in 3 days next to Obbie’s Restaurant and heard you sold really juicy strawberries. Would you be able to sell me 20? Maybe we can build a nice partnership :)"},
           {"days", "3"},
           {"strawberrie", "20"},
           {"potatoe", "0"},
           {"carrot", "0"}
       },
       new Dictionary<string, string> {
           {"name", "Rian"},
           {"msg", "Dear neighbor, I have heard great things about you and am in need of 10 potatoes and 15 carrots immediately. I am trying a new experiment and time is of the essence. Is this too much to ask?"},
           {"days", "1"},
           {"strawberrie", "0"},
           {"potatoe", "10"},
           {"carrot", "15"}
       },
       new Dictionary<string, string> {
           {"name", "Janey"},
           {"msg", "Hey neighbor! My horses loved eating your carrots. I have a group of new equestrians coming to town and need more carrots soon. I’m thinking 30 this time around will do. Thanks a bunch, neighbor!"},
           {"days", "4"},
           {"strawberrie", "0"},
           {"potatoe", "0"},
           {"carrot", "30"}
       },
       new Dictionary<string, string> {
           {"name", "Obbie"},
           {"msg", "How are you doing Farmer? Is everything going as you expected? I hope you’re adjusting well now and are able to ship 15 each of your best crops. I’ll need them in 3 days as the mayor is throwing a party in town. When you get the time you should stop by."},
           {"days", "3"},
           {"strawberrie", "15"},
           {"potatoe", "15"},
           {"carrot", "15"}
       },
   };


   List<Dictionary<string, string>> lvl3 = new List<Dictionary<string, string>> {
       new Dictionary<string, string> {
           {"name", "Johnathan"},
           {"msg", "Wow, your crops have been a huge hit in my shop. My customers keep coming back asking why they have been tasting so much better than the ones before. Just between you and me, since your Pa retired I have been buying from Acomm Farms. They’re cheap since they can be found all over, but I don’t get many customers coming back for them. Yours are a hit and I’d love to order a batch of 50 strawberries and potatoes. Maybe you can add in a stash of 20 carrots."},
           {"days", "3"},
           {"strawberrie", "50"},
           {"potatoe", "50"},
           {"carrot", "20"}
       },
       new Dictionary<string, string> {
           {"name", "Rian"},
           {"msg", "Hi neighbor. My experiment is in need of an additional 15 carrots. I am also buying 15 strawberries and need them pronto. I am almost done developing a new hybrid seed. Is this okay?"},
           {"days", "1"},
           {"strawberrie", "15"},
           {"potatoe", "0"},
           {"carrot", "15"}
       },
       new Dictionary<string, string> {
           {"name", "Pa"},
           {"msg", "Hey kiddo, how is everything going? Your Ma needs 15 potatoes and carrots for a big soup she’s making. The town has been talking left and right about your crops. Your Ma and I are so proud of you and I hope you know that we love you. Keep it up kiddo."},
           {"days", "2"},
           {"strawberrie", "0"},
           {"potatoe", "15"},
           {"carrot", "15"}
       },
       new Dictionary<string, string> {
           {"name", "Miichi"},
           {"msg", "Hello fellow farmer, I have been hearing great things about what you grow, especially your strawberries. I run a seasonal competition showcasing our local farmers and their goods. This year, we are looking for the best and most delicious strawberries. If you’re interested in entering, please send in a batch of 10. You will be compensated of course. Thank you and hope to see you soon."},
           {"days", "1"},
           {"strawberrie", "10"},
           {"potatoe", "0"},
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
       float mailChance = 30f + (float)DaySystem.instance.dayCount + ((float)Experience.instance.experience / 1000f);


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
       } else mailActive = false;


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
                   mailActive = true;
               }
               else
               {
                   letters[i].gameObject.SetActive(false);
               }
           }
       }
   }
}



