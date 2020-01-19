using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tree : MonoBehaviour
{
    private GameObject _player;

    private PlayerMovement _playerMovement;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player");
        _playerMovement = _player.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        CutTree();
    }

    private void CutTree()
    {
        Collider[] hitColliders = Physics.OverlapSphere(_player.transform.position, 2f);
        bool isNearObject = false;
        foreach (var collider in hitColliders)
        {
            if (collider == gameObject.GetComponent<Collider>())
            {
                isNearObject = true;
            }
        }
        
        if(isNearObject)
        {
            Destroy(gameObject);
        }
        else
        {
            _playerMovement.MovePlayerDestination(transform.position);
        }
    }
}
