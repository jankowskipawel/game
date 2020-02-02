using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public enum ResourceType
{
    woodBrown,
    woodGrey,
    stone,
    copper,
        
}
public class Resource : MonoBehaviour
{
    private GameObject _player;
    private PlayerMovement _playerMovement;
    private NavMeshAgent _playerNavMeshAgent;
    public GameObject resource;
    public GameObject depletedResource;
    public float respawnTime = 1f;
    public ResourceType resourceType;

    void Start()
    {
        _player = GameObject.Find("Player");
        _playerMovement = _player.GetComponent<PlayerMovement>();
        _playerNavMeshAgent = _player.GetComponent<NavMeshAgent>();
    }

    void Update()
    {
    }

    public void OnMouseDown()
    {
        Collider[] hitColliders = Physics.OverlapSphere(_player.transform.position, 3f);
        bool isNearObject = false;
        foreach (var collider in hitColliders)
        {
            if (collider == resource.GetComponent<Collider>())
            {
                isNearObject = true;
            }
        }

        if (!isNearObject)
        {
            var targetPos = _playerMovement.MovePlayerToObjPos(gameObject, 0.8f);
            var coroutine = WaitAndGather(targetPos);
            StartCoroutine(coroutine);
        }
        else
        {
            var q = Quaternion.LookRotation(transform.position - _player.transform.position);
            _player.transform.rotation = Quaternion.RotateTowards(_player.transform.rotation, q, 200);

            Gather();
        }
    }
    
    IEnumerator WaitAndGather(Vector3 targetpos)
    {
        float timeToReach = (_playerMovement.CalculatePathLength(targetpos)) / (_playerNavMeshAgent.speed);
        yield return new WaitForSeconds(timeToReach);
        Collider[] hitColliders = Physics.OverlapSphere(_player.transform.position, 3f);
        bool isNearObject = false;
        foreach (var collider in hitColliders)
        {
            if (collider == resource.GetComponent<Collider>())
            {
                isNearObject = true;
            }
        }

        if (_playerMovement.IsPathCompleted() & isNearObject)
        {
            Gather();
        }
    }
    
    private void Gather()
    {
        resource.SetActive(false);
        depletedResource.SetActive(true);
        _playerNavMeshAgent.ResetPath();
        StartRespawn();
    }

    IEnumerator WaitForRespawn()
    {
        yield return new WaitForSeconds(respawnTime);
        resource.SetActive(true);
        depletedResource.SetActive(false);
    }
    
    //
    public void StartRespawn()
    {
        var coroutine = WaitForRespawn();
        StartCoroutine(coroutine);

    }
    
}
