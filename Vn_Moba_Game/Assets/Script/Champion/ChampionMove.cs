using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChampionMove : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;
    private Animator _animator;
    private bool _isRunning = false;
    private Camera _camPlayer;
    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _camPlayer = gameObject.transform.parent.Find("CameraPlayer").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = _camPlayer.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Input.GetMouseButtonDown(1))
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.collider.gameObject.tag.Equals("Floor"))
                {
                    _navMeshAgent.destination = hit.point;
                }
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
        _animator.SetBool("isRunning", _isRunning);
    }
}
