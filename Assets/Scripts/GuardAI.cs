using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardAI : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _waitTimer;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private AI_State _state = AI_State.Idle;
    [SerializeField] private Transform _waypointPath;

    private Transform _currentWaypoint;
    private Transform _LastWaypoint;
    private List<Transform> _waypoints = new List<Transform>();
    private Transform _playerPosition;

    private float _currentWaitTimer;
    private bool _IsSearchingForWaypoint = false;
    
    private enum AI_State
    { 
        Idle,
        Walk,
        SeenPlayer
    }

    private void Start()
    {
        _agent.speed = _moveSpeed;
        _currentWaitTimer = _waitTimer;

        _waypoints.Clear();
        foreach (Transform waypoint in _waypointPath.transform)
        {
            _waypoints.Add(waypoint);
        }
    }

    private void Update()
    {
        CheckState();
    }

    private void CheckState()
    {
        switch(_state)
        {
            case AI_State.Idle:
                _currentWaitTimer -= Time.deltaTime;

                if (_currentWaitTimer <= 0)
                {
                    _state = AI_State.Walk;
                }
                break;
            case AI_State.Walk:
                if (_waypoints.Count <= 0)
                {
                    Debug.LogWarning("There are no waypoints in the array");
                    return;
                }

                if (!_IsSearchingForWaypoint)
                {
                    _IsSearchingForWaypoint = true;
                    _currentWaypoint = _waypoints[Random.Range(0,_waypoints.Count)].transform;
                    if (_LastWaypoint == _currentWaypoint)
                    {
                        foreach (Transform waypoint in _waypoints)
                        {
                            if (waypoint != _LastWaypoint)
                            {
                                _currentWaypoint = waypoint;
                                break;
                            }
                        }
                    }
                    _LastWaypoint = _currentWaypoint;
                }

                _agent.SetDestination(_currentWaypoint.position);

                if (Vector3.Distance(transform.position, _currentWaypoint.position) < _agent.stoppingDistance)
                {
                    _IsSearchingForWaypoint = false;
                    _currentWaitTimer = _waitTimer;
                    _state = AI_State.Idle;
                }
                break;
            case AI_State.SeenPlayer:
                _agent.stoppingDistance = 2f;
                _agent.SetDestination(_playerPosition.position);
                break;
        }         
    }

    public void ChangeAIStateToSeenPlayer(Transform player)
    {
        _playerPosition = player;
        _state = AI_State.SeenPlayer;
    }
}
