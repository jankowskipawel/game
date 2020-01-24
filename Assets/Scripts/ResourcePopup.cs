using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcePopup : MonoBehaviour
{
    private float t;
    private float timer = 0;
    private Vector3 startingPos;
    private Vector3 endPos;
    private Camera camera;
    public GameObject popupPrefab;
    void Start()
    {
        startingPos = Camera.main.WorldToScreenPoint(transform.position);
        endPos = new Vector3(startingPos.x, startingPos.y + 1, startingPos.z);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        t += Time.deltaTime/0.2f;
        transform.position = Vector3.Lerp(startingPos, endPos, t);
        if (transform.position == endPos && timer > 0.5f)
        {
            Destroy(gameObject);
        }
    }

    public void SetText(float damage)
    {
        //gameObject.GetComponent<TextMeshPro>().text = $"{damage}";
    }
}
