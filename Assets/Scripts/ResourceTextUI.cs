using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceTextUI : MonoBehaviour
{
    private PlayerResources _playerResources;
    private TextMeshProUGUI resText;
    public int resourceID;
    // Start is called before the first frame update
    void Start()
    {
        _playerResources = GameObject.Find("Player").GetComponent<PlayerResources>();
        resText = gameObject.GetComponent<TextMeshProUGUI>();
        InvokeRepeating("UpdateText", 0, 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void UpdateText()
    {
        resText.text = _playerResources.GetResource(resourceID).ToString();
    }
}
