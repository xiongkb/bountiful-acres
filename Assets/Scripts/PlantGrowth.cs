using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantGrowth : MonoBehaviour
{
    [SerializeField] private float speed = 0f; // plant growth speed

    public SpriteRenderer spriteRenderer;
    public Sprite[] stages;  // add the plant here
    public int stage = 0;

    private float currTime = 0f;
    private float updateTime;

    [SerializeField] private GameObject[] bugs;
    [SerializeField] private float bugCheckTime;
    private float lastBugCheckTime = 0f;
    private List<GameObject> spawnedBugs = new List<GameObject>();
    private float newBugZ = 0f;

    // Start is called before the first frame update
    void Start()
    {
        updateTime = speed;

        if (stages.Length > 0) {
            spriteRenderer.sprite = stages[0];
        }
    }

    // Update is called once per frame
    void Update() {
        currTime += Time.deltaTime;

        if (stage < stages.Length - 1 && currTime >= updateTime) {
            if (stage == 0) lastBugCheckTime = currTime - bugCheckTime + 1;

            stage += 1;
            spriteRenderer.sprite = stages[stage];
            updateTime += speed;
        }

        if (stage > 0 && currTime - lastBugCheckTime >= bugCheckTime) {
            for (int i = 0; i < bugs.Length; i++) {
                Bug bug = (Bug) bugs[i].GetComponent(typeof(Bug));

                float randNum = Random.Range(0f, 1f);

                if (1 - randNum >= bug.bugChance) {
                    GameObject newBug = Instantiate(bugs[i], new Vector3(this.transform.position.x, this.transform.position.y, newBugZ), Quaternion.identity);
                    newBugZ = newBugZ - 0.01f;
                }
            }

            lastBugCheckTime = currTime;
        }

        if (Input.GetMouseButtonDown(0) && gameObject.GetComponent<Collider2D>().OverlapPoint(GetMousePos())) {
            Vector2 mousePos = GetMousePos();
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if(hit.collider.tag == "Bug") Destroy(hit.collider.gameObject);
        }
        
    }

    Vector2 GetMousePos() {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

}
