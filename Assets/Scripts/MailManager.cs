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
    Mail[] letters = { null, null, null, null, null };

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

    // Update is called once per frame
    void Update()
    {
        Debug.Log(letters[0]);
    }

    public void GenerateLetter()
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

                break;
            }
        }
    }

    public void RemoveLetter(int letterNum)
    {
        
    }
}
