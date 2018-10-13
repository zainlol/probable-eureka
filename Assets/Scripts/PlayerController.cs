﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class PlayerController : MonoBehaviour {

    public float _speed = 1.0f;
    public float x = 0;
    public float y = 0;

    public bool actionA = false;
    public bool actionB = false;
    public bool actionX = false;
    public bool actionY = false;

    
    private InputDevice _userDevice;
    private PlayerActions _playerActions;

    private Rigidbody2D _rb;

	// Use this for initialization
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void bindGamepad(InputDevice device)
    {
        _userDevice = device;
        _playerActions = new PlayerActions();
        _playerActions.bindToGamepad(device);
    }

    public void bindKeyboard()
    {
        _playerActions = new PlayerActions();
        _playerActions.bindToKeyboard();
    }

    private void FixedUpdate()
    {
        if (_playerActions == null) return;

        var moveHorizontal = _playerActions.Move.X;
        var moveVertical = _playerActions.Move.Y;
        x = moveHorizontal;
        y = moveVertical;

        Vector3 movement = new Vector2(moveHorizontal, moveVertical);
        
        _rb.AddForce(movement * _speed * Oscillator());

        // Check actions
        actionA = _playerActions.ActionA.IsPressed;
        actionB = _playerActions.ActionB.IsPressed;
        actionX = _playerActions.ActionX.IsPressed;
        actionY = _playerActions.ActionY.IsPressed;
    }

    private float Oscillator()
    {
        return 0.7f + Mathf.Sin(Time.time * 2.0f * 2.0f * Mathf.PI);
    }

    private void updateCurrentCountry()
    {
        /*
        _currentCountry = null;

        var layerMask = LayerMask.GetMask("Countries");


        RaycastHit hit;
        // Does the ray intersect any country object
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, layerMask))
        {
            _currentCountry = hit.transform.GetComponent<Country>();

            if (_currentCountry != _lastCountry)
            {
                if (_lastCountry != null) onExitedCountry(_lastCountry);

                if (_currentCountry != null) onEnteredCountry(_lastCountry, _currentCountry);

                _lastCountry = _currentCountry;
            }
        }
        else
        {
            _currentCountry = null;
            if (_lastCountry != null)
            {
                onExitedCountry(_lastCountry);
                _lastCountry = null;
            }
        }
        */
    }

}