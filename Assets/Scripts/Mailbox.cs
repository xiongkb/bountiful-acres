using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mailbox : MonoBehaviour
{
    public static Mailbox instance;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite spriteEmpty;
    [SerializeField] Sprite spriteFull;

    void Awake() {
        instance = this;
        SetEmpty();
    }

    public void SetEmpty() {
        spriteRenderer.sprite = spriteEmpty;
    }

    public void SetFull() {
        spriteRenderer.sprite = spriteFull;
    }

    void Update()
    {
        if(!MailManager.instance.mailActive && Input.GetMouseButtonDown(0) && Utilities.instance.isOverlappingMouse(gameObject) && MailManager.instance.letters[0] != null) {
            MailManager.instance.letters[0].gameObject.SetActive(true);
            MailManager.instance.mailActive = true;
        }
    }
}
