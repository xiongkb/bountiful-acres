using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // array of plot game objects instead of strings
    string[,] plots = {
        {"plot1", "plot2", "plot3"},
        {"plot4", "plot5", "plot6"},
        {"plot7", "plot8", "plot9"}
    };

    // array of plant objs inside a plot (each plot has their own array of plant objs)
    // each plant obj keeps its own growth state
    string[,] plants = {
        {"strawberry", "strawberry", "strawberry"},
        {"kiwi", "strawberry", "papaya"},
        {"fruit", "veggie", "meat"}
    };
}
