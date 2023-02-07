using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _RotateSpeed;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _SmoothAnimationTime;
    [SerializeField] private Rigidbody _rb;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        HandleMovement();
        HandleRotation();
    }

    private void HandleMovement()
    {
        float moveX = 0f;
        float moveZ = 0f;

        if (Input.GetKey(KeyCode.W))
        {
            moveZ = +1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveZ = -1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveX = +1f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveX = -1f;
        }

        Vector3 moveDir = transform.forward * moveZ + transform.right * moveX;
        //transform.position += moveDir * _moveSpeed * Time.deltaTime;
        _rb.velocity = moveDir.normalized * _moveSpeed;

        HandleAnimations(moveDir, moveZ, moveX);
    }

    private void HandleRotation()
    {
        float mouseX = Input.GetAxis("Mouse X");

        Vector3 rotationDir = new Vector3(0,mouseX,0);
        transform.eulerAngles += rotationDir * _RotateSpeed * Time.deltaTime;

    }

    private void HandleAnimations(Vector3 moveDir, float moveZ, float moveX)
    {
        bool isIdle = moveX == 0 && moveZ == 0;
        if (isIdle)
        {
            _animator.SetBool("IsWalking", false);
        }
        else
        {
            _animator.SetBool("IsWalking", true);
        }


        if (moveZ > 0 || moveZ < 0)
        {
            _animator.SetFloat("VerticalMovement", moveDir.z, _SmoothAnimationTime, Time.deltaTime);
        }
        else
        {
            _animator.SetFloat("VerticalMovement", moveDir.z);
        }
        if (moveX > 0 || moveX < 0)
        {
            _animator.SetFloat("HorizontalMovement", moveDir.x, _SmoothAnimationTime, Time.deltaTime);
        }
        else
        {
            _animator.SetFloat("HorizontalMovement", moveDir.x);
        }

    }
}
