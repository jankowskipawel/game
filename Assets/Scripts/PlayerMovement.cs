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
        x.text = $"Path length: {CalculatePathLength(_navMeshAgent.destination)}, ETA: {(CalculatePathLength(_navMeshAgent.destination)) / (_navMeshAgent.speed)}, {IsPathCompleted()}";
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

    public Vector3 MovePlayerToObjPos(GameObject obj, float ObstacleRadius)
    {
        _navMeshAgent.ResetPath();
        var target = obj.transform;
        float a = transform.position.x - target.position.x;
        float b = transform.position.z - target.position.z;
        float AngleTowardUnit = Mathf.Atan(b/a);
        float xOffset = ObstacleRadius * Mathf.Cos(AngleTowardUnit);
        float zOffset = ObstacleRadius * Mathf.Sin(AngleTowardUnit);
        if (a < 0)
        {
            xOffset = -1 * xOffset;
            zOffset = -1 * zOffset;
        }
        var x = new Vector3(target.position.x + xOffset, target.position.y, target.position.z + zOffset);
        //_animator.SetTrigger("stopActionAnimation");
        _navMeshAgent.SetDestination(x);
        return x;
    }

    public NavMeshAgent GetNavMeshAgent()
    {
        return _navMeshAgent;
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
    
    public float CalculatePathLength(Vector3 targetPosition)
    {
        NavMeshPath path = _navMeshAgent.path;
        Vector3[] allWayPoints = new Vector3[path.corners.Length + 2];
        allWayPoints[0] = transform.position;
        allWayPoints[allWayPoints.Length - 1] = targetPosition;
    
        for(int i = 0; i < path.corners.Length; i++)
        {
            allWayPoints[i + 1] = path.corners[i];
        }
        float pathLength = 0;
        for(int i = 0; i < allWayPoints.Length - 1; i++)
        {
            pathLength += Vector3.Distance(allWayPoints[i], allWayPoints[i + 1]);
        }
        return pathLength;
    }
}
