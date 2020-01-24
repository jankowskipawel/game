using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnResource : MonoBehaviour
{
    public GameObject resource;
    public float respawnTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    IEnumerator WaitForRespawn()
    {
        yield return new WaitForSeconds(respawnTime);
        resource.SetActive(true);
    }
    
    //
    public void StartRespawn()
    {
        var coroutine = WaitForRespawn();
        StartCoroutine(coroutine);

    }
}
