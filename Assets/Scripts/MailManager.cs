using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailManager : MonoBehaviour
{
    public static MailManager instance;
    [SerializeField] Mail mailLetterPrefab;
    [SerializeField] int minNum;
    [SerializeField] int maxNum;
    [SerializeField] string[] names;
    [SerializeField] string[] messages;
    string[] crops;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        crops = Manager.instance.crops;
        Debug.Log(crops[0]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateLetter()
    {
        string name = names[Random.Range(0, names.Length)];
        string message = messages[Random.Range(0, messages.Length)];
        string crop = crops[Random.Range(0, crops.Length)];
        int num = Random.Range(minNum, maxNum);
        Vector2 mailPos = Camera.main.ViewportToWorldPoint(new Vector2(0.5f, 0.5f));
        Mail mail = Instantiate(mailLetterPrefab, mailPos, Quaternion.identity) as Mail;

        mail.SetLetter(name, message, crop, num);
    }
}
