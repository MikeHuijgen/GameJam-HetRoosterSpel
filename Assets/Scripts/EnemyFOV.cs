using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyFOV : MonoBehaviour
{
    [SerializeField] private float _fovAngle;
    [SerializeField] private float _range;

    [SerializeField] private Transform _fovPoint;
    [SerializeField] private Transform _target;
    [SerializeField] private Light _light;

    private bool seenThePlayer = false;

    private void Start()
    {
        _light.range = _range;
    }

    private void Update()
    {
        if (!seenThePlayer)
        {
            CheckFOV();
        }
    }

    private void CheckFOV()
    {
        Vector3 dir = _target.position - transform.position;
        float angle = Vector3.Angle(dir, _fovPoint.forward);
        RaycastHit hit;

        if (Physics.Raycast(_fovPoint.position, dir, out hit, _range) && angle < _fovAngle / 2)
        {
            if (hit.transform.tag == "Player")
            {
                seenThePlayer = true;

                GuardAI guardAI = GetComponent<GuardAI>();
                guardAI.ChangeAIStateToSeenPlayer(hit.transform);

                _light.color = Color.red;
                GameHandler.Instance._startReset = true;
            }
        }
    }

}
