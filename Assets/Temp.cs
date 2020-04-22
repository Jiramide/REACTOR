using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temp : MonoBehaviour
{

    private LevelLayout grid;

    private void Start()
    {
        grid = LevelLayout.GetInstance();

        foreach (Transform child in transform)
        {
            grid.AddEntity(child.gameObject);
        }
    }

}
