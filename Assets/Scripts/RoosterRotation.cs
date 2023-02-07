using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoosterRotation : MonoBehaviour
{
    [SerializeField] private float _RotateSpeed;
    [SerializeField] private float _heightDifference;
    [SerializeField] private float _frequency;
    private Vector3 tempPos;
    private Vector3 posOffset;

    private void Start()
    {
        posOffset = transform.position;
    }

    private void Update()
    {
        HandleRotation();
        HandleBounce();
    }

    private void HandleRotation()
    {
        transform.eulerAngles += new Vector3(0, _RotateSpeed * Time.deltaTime, 0);
    }

    private void HandleBounce()
    {
        tempPos = posOffset;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * _frequency) * _heightDifference;

        transform.position = tempPos;
    }
}
