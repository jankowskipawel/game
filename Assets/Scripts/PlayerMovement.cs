/*
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
*/
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;
    private GameObject _player;
    private Animator _animator;

    private bool _isRunning = false;
    private static readonly int IsRunning = Animator.StringToHash("isRunning");
    private bool isBusy = false;
    public Text x;

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
        MovePlayerRay(ray);
        var xd = PathLength();
        x.text = $"Path length: {xd}";
    }

    public void MovePlayerRay(Ray ray)
    {
        RaycastHit hit;
        if (Input.GetMouseButton(1))
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

    public void OLDMovePlayerDestination(Vector3 destination)
    {
        _navMeshAgent.SetDestination(destination);
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

    public void MovePlayerToObjPos(GameObject obj)
    {
        var target = obj.transform;
        float a = transform.position.x - target.position.x;
        float b = transform.position.z - target.position.z;
        float AngleTowardUnit = Mathf.Atan(b/a);
        float ObstacleRadius = 0.8f;
        float xOffset = ObstacleRadius * Mathf.Cos(AngleTowardUnit);
        float zOffset = ObstacleRadius * Mathf.Sin(AngleTowardUnit);
        if (a < 0)
        {
            xOffset = -1 * xOffset;
            zOffset = -1 * zOffset;
        }
        var x = new Vector3(target.position.x + xOffset, target.position.y, target.position.z + zOffset);
        _navMeshAgent.SetDestination(x); 
    }

    public NavMeshAgent GetNavMeshAgent()
    {
        return _navMeshAgent;
    }

    public bool GetIsBusy()
    {
        return isBusy;
    }

    public void SetIsBusy(bool x)
    {
        isBusy = x;
    }
    
    public bool IsPathCompleted()
    {
        if (!_navMeshAgent.pathPending)
        {
            if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
            {
                if (!_navMeshAgent.hasPath || _navMeshAgent.velocity.sqrMagnitude == 0f)
                {
                    return true;
                }
            }
        }

        return false;
    }
    
    public float PathLength()
    {
        var path = _navMeshAgent.path;
        if (path.corners.Length < 2)
            return 0;
        
        Vector3 previousCorner = path.corners[0];
        float lengthSoFar = 0.0F;
        int i = 1;
        while (i < path.corners.Length) {
            Vector3 currentCorner = path.corners[i];
            lengthSoFar += Vector3.Distance(previousCorner, currentCorner);
            previousCorner = currentCorner;
            i++;
        }
        return lengthSoFar;
    }
}
