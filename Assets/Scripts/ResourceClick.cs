using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceClick : MonoBehaviour
{
    private Resource parentScript;
    // Start is called before the first frame update
    void Start()
    {
        parentScript = transform.parent.GetComponent<Resource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        parentScript.OnMouseDown();
    }
}
