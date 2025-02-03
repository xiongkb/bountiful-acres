using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantGrowth : MonoBehaviour
{
    [SerializeField] private float speed = 0f; // plant growth speed

    public GameObject[] stages;  // add the plant here

    private float currTime = 0f;
    private float updateTime;
    private int stage = 0;

    // Start is called before the first frame update
    void Start()
    {
        updateTime = speed;
    }

    // Update is called once per frame
    void Update() {
        if (stage < stages.Length - 1) {
            currTime += Time.deltaTime;

            if (currTime >= updateTime) {
                stage += 1;
                updateTime += speed;
            }

            Debug.Log(currTime + ": " + stage);
        }
        
    }
}
