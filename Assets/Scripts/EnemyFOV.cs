using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        else
        {
            // hier moet iets als de speler gezien word
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
                Debug.Log("uwdugdwug");
            }
        }
    }
}
