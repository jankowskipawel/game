using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = System.Random;


public class Resource : MonoBehaviour
{
    private GameObject _player;
    private PlayerMovement _playerMovement;
    private PlayerResources _playerResources;
    private NavMeshAgent _playerNavMeshAgent;
    public GameObject resource;
    public GameObject depletedResource;
    private Animator _animator;
    public float respawnTime = 1f;
    public int resourceID;
    private int resourceQuantity;
    public int maxQuantity = 5;
    private int toolEfficiency = 1;
    private CustomAudioManager audioManager;
    public GameObject resourcePopup;
    public Texture icon;
    public int ExpID;
    private PlayerExperience _playerExperience;
    
    void Start()
    {
        _player = GameObject.Find("Player");
        _playerMovement = _player.GetComponent<PlayerMovement>();
        _playerNavMeshAgent = _player.GetComponent<NavMeshAgent>();
        _playerResources = _player.GetComponent<PlayerResources>();
        _playerExperience = _player.GetComponent<PlayerExperience>();
        resourceQuantity = maxQuantity + Convert.ToInt32(UnityEngine.Random.Range(-maxQuantity*0.3f, maxQuantity*0.3f));
        _animator = _player.GetComponent<Animator>();
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<CustomAudioManager>();
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
            //var coroutine = WaitAndGather(targetPos);
            //StartCoroutine(coroutine);
        }
        else
        {
            _playerNavMeshAgent.ResetPath();
            var q = Quaternion.LookRotation(transform.position - _player.transform.position);
            _player.transform.rotation = Quaternion.RotateTowards(_player.transform.rotation, q, 200);
            Gather();
        }
    }
    
    

    //go and activate
    /*IEnumerator WaitAndGather(Vector3 targetpos)
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
    }*/
    
    private void Gather()
    {
        _animator.SetTrigger("chop");
        _playerResources.AddResource(resourceID, toolEfficiency);
        resourceQuantity -= toolEfficiency;
        _playerExperience.AddExp(ExpID, Convert.ToUInt64(toolEfficiency));
        var popup = Instantiate(resourcePopup, transform.position, Quaternion.identity);
        popup.GetComponent<ResourcePopup>().SetText(toolEfficiency);
        popup.GetComponent<ResourcePopup>().SetIcon(icon);
        if (resourceQuantity <= 0)
        {
            audioManager.Play(GenerateSoundName(true, 1));
            resource.SetActive(false);
            depletedResource.SetActive(true);
            _playerNavMeshAgent.ResetPath();
            StartRespawn();
        }
        else
        {
            audioManager.Play(GenerateSoundName(false, 3));
        }
    }

    IEnumerator WaitForRespawn()
    {
        yield return new WaitForSeconds(respawnTime);
        resource.SetActive(true);
        depletedResource.SetActive(false);
        resourceQuantity = maxQuantity + Convert.ToInt32(UnityEngine.Random.Range(-maxQuantity*0.3f, maxQuantity*0.3f)); 
    }
    
    public void StartRespawn()
    {
        var coroutine = WaitForRespawn();
        StartCoroutine(coroutine);
    }

    private string GenerateSoundName(bool isBreaking, int numberOfSounds)
    {
        string soundname = "";
        if (resourceID < 16)
        {
            soundname += "Wood";
        }
        if (resourceID > 15 & resourceID < 32)
        {
            soundname += "Stone";
        }

        if (isBreaking)
        {
            soundname += "Break";
        }
        else
        {
            soundname += "Hit";
        }
        soundname += UnityEngine.Random.Range(1, numberOfSounds+1).ToString();
    
        return soundname;
    }
}
