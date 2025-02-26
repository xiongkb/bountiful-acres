using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilities : MonoBehaviour
{
    public static Utilities instance;

    void Awake()
    {
        instance = this;
    }

    public Vector2 GetMousePos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public bool isOverlappingMouse(GameObject obj)
    {
        return obj.GetComponent<Collider2D>().OverlapPoint(GetMousePos());
    }
}
