using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mailbox : MonoBehaviour
{
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && Utilities.instance.isOverlappingMouse(gameObject) && MailManager.instance.letters[0] != null)
            MailManager.instance.letters[0].gameObject.SetActive(true);
    }
}
