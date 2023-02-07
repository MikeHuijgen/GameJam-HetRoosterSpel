using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    public static GameHandler Instance { get; private set; }
    [SerializeField] private float _resetTime;
    [NonSerialized] public bool _startReset = false;

    private float _currentResetTime;

    private void Awake()
    {
        if (Instance != null)
        {
            // Dit is een check voor als er meer dan 1 UnitActionSystem is. 
            Debug.LogError("There's more than one GameHandler!" + transform + "-" + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        _currentResetTime = _resetTime;
    }

    private void Update()
    {
        if (_startReset)
        {
            _currentResetTime -= Time.deltaTime;
            if(_currentResetTime <= 0)
            {
                SceneManager.LoadScene(0);
            }
        }
    }

}
