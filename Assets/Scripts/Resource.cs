using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Resource : MonoBehaviour
{
    private GameObject _player;
    private PlayerMovement _playerMovement;
    private NavMeshAgent _playerNavMeshAgent;
    private Mesh originalMesh;
    public Mesh depletedMesh;
    private MeshFilter _meshFilter;
    private RespawnResource _respawnResource;
    
    void Start()
    {
        _player = GameObject.Find("Player");
        _playerMovement = _player.GetComponent<PlayerMovement>();
        _playerNavMeshAgent = _player.GetComponent<NavMeshAgent>();
        originalMesh = gameObject.GetComponent<Mesh>();
        _meshFilter = gameObject.GetComponent<MeshFilter>();
        _respawnResource = transform.parent.GetComponent<RespawnResource>();
    }

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
            var targetPos = _playerMovement.MovePlayerToObjPos(gameObject, 0.8f);
            var coroutine = WaitAndCut(targetPos);
            StartCoroutine(coroutine);
        }
        else
        {
            Gather();
        }
        
    }
    
    IEnumerator WaitAndCut(Vector3 targetpos)
    {
        float timeToReach = (_playerMovement.CalculatePathLength(targetpos)) / (_playerNavMeshAgent.speed-1);
        yield return new WaitForSeconds(timeToReach);
        if (_playerMovement.IsPathCompleted())
        {
            Gather();
        }
    }
    
    

    private void Gather()
    {
        gameObject.SetActive(false);
        _playerNavMeshAgent.ResetPath();
        _respawnResource.StartRespawn();
    }

    
    
}
