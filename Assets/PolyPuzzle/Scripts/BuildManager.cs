using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    ModelView[] models;
    void Start()
    {
        for (int i = 0; i < models.Length; i++)
        {
            models[i].isBuildable = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
