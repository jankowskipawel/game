using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourcePopup : MonoBehaviour
{
    private float t;
        private float timer = 0;
        private Vector3 startingPos;
        private Vector3 endPos;
        public TextMeshProUGUI text;
        public RawImage imageObj;
        private GameObject ui;
        private float offset;

        // Start is called before the first frame update
        void Start()
        {
            offset = Random.Range(-10, 10);
            ui = GameObject.FindGameObjectWithTag("UI");
            transform.SetParent(ui.transform);
            startingPos = new Vector3(Input.mousePosition.x+40, Input.mousePosition.y, 10);
            endPos = new Vector3(startingPos.x+offset, startingPos.y + 50, startingPos.z);
        }
    
        // Update is called once per frame
        void Update()
        {
            timer += Time.deltaTime;
            t += Time.deltaTime/0.2f;
            transform.position = Vector3.Lerp(startingPos, endPos, t);
            if (timer > 0.3f)
            {
                Destroy(gameObject);
            }
        }
    
        public void SetText(float amount)
        {
            text.text = $"+{amount}";
        }

        public void SetIcon(Texture icon)
        {
            imageObj.texture = icon;
        }
}
