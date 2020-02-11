using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResources : MonoBehaviour
{
    private long[] resources = new long[128];
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void AddResource(int id, long amount)
    {
        resources[id] += amount;
    }

    public void RemoveResource(int id, long amount)
    {
        if (resources[id] - amount >= 0)
        {
            resources[id] -= amount;
        }
    }

    public long GetResource(int id)
    {
        return resources[id];
    }
}
