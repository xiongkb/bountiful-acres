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

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        crops = Manager.instance.crops;
        letters = new Mail[numLetters];
    }

    void Update()
    {
        currTime += Time.deltaTime;

        if(currTime - newMailTime >= lastMailTime)
        {
            lastMailTime += newMailTime;
            
            if(letters[letters.Length - 1] == null)
                GenerateLetter();
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

                mail.SetLetter(i, name, message, crop, num);
                mail.gameObject.SetActive(false);

                if (i > 0)
                {
                    letters[i - 1].rightButton.interactable = true;
                    letters[i - 1].gameObject.SetActive(false);
                }

                break;
            }
        }
    }

    public void RemoveLetter(int letterNum)
    {
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
            letters[letterNum].gameObject.SetActive(true);

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
            letters[letterNum - 1].gameObject.SetActive(true);

            letters[letterNum - 1].rightButton.interactable = false;
        }
    }

    public void SetActiveLetter(int letterNum)
    {
        for(int i = 0; i < letters.Length; i++)
        {
            // Debug.Log(i);
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
