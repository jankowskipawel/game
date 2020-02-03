using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    // Start is called before the first frame update
    private bool isActive = false;
    public GameObject inventoryUI;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (isActive)
            {
                inventoryUI.SetActive(false);
                isActive = false;
            }
            else
            {
                inventoryUI.SetActive(true);
                isActive = true;
            } 
        }
    }
}
