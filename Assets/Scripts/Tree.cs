using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Tree : MonoBehaviour
{
    private GameObject _player;
    private PlayerMovement _playerMovement;
    private NavMeshAgent _playerNavMeshAgent;
    
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player");
        _playerMovement = _player.GetComponent<PlayerMovement>();
        _playerNavMeshAgent = _player.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
    }
    
    private void OnMouseDown()
    {
        Collider[] hitColliders = Physics.OverlapSphere(_player.transform.position, 3f);
        bool isNearObject = false;
        foreach (var collider in hitColliders)
        {
            if (collider == gameObject.GetComponent<Collider>())
            {
                isNearObject = true;
            }
        }

        if (!isNearObject)
        {
            _playerMovement.MovePlayerToObjPos(gameObject);
            /*var coroutine = Wait();
            StartCoroutine(coroutine);*/
        }
        else
        {
            CutTree();
        }
        
    }
    
    /*IEnumerator Wait()
    {
        float x = (_playerMovement.PathLength()-1.3f) / _playerNavMeshAgent.speed + 0.5f;
        yield return new WaitForSeconds(x);
        if (_playerMovement.IsPathCompleted())
        {
            CutTree();
        }
    }*/

    private void CutTree()
    {
        
        Destroy(gameObject);
        
    }

    
    
}
