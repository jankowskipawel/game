using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C4mera : MonoBehaviour
{
    public GameObject player;
    public float cameraHeight = 20.0f;
    public float cameraDistance = 20.0f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = player.transform.position;
        pos.y += cameraHeight;
        pos.z -= cameraDistance;
        transform.position = pos;
    }
}
