using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;
    private GameObject _player;
    private Animator _animator;

    private bool _isRunning = false;
    private static readonly int IsRunning = Animator.StringToHash("isRunning");


    // Start is called before the first frame update
    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _player = GameObject.Find("Player");
        _animator = _player.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
        MovePlayer(ray);
    }

    public void MovePlayer(Ray ray)
    {
        RaycastHit hit;
        if (Input.GetMouseButton(0))
        {
            if (Physics.Raycast(ray, out hit, 100))
            {
                _navMeshAgent.destination = hit.point;
            }
        }
        if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
        {
            _isRunning = false;
        }
        else
        {
            _isRunning = true;
        }
        _animator.SetBool(IsRunning,_isRunning);
    }
}
