using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExperience : MonoBehaviour
{
    private ulong[] experience = new ulong[8];
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddExp(int expID, ulong amount)
    {
        experience[expID] += amount;
    }
}
