﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;
using InControl;

public class PlayerController : MonoBehaviour
{
    public AudioEvent JumpSound;
    
    public const float Speed = 12.0f;
    public static readonly float[] Jump = {0, 15.0f, 10.0f};
    public const float JumpGravity = 3.0f;
    public const float FallGravity = 6.0f;
    public const float AirControl = 1.0f;

    public Transform View;

    public PlayerActions PlayerActions;

    private Rigidbody2D _rb;
    private int _jumped;

    // Use this for initialization
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void BindGamepad(InputDevice device)
    {
        PlayerActions = new PlayerActions();
        PlayerActions.bindToGamepad(device);
    }

    public void BindKeyboard()
    {
        PlayerActions = new PlayerActions();
        PlayerActions.bindToKeyboard();
    }

    private void FixedUpdate()
    {
        if (PlayerActions == null) return;

        //Apply movement
        var input = new Vector2(PlayerActions.Move.X, 0);
        //_rb.AddForce(input * Speed *(_jumped == 0 ? 1.0f : AirControl));

        var vel = _rb.velocity;
        vel.x = PlayerActions.Move.X * Speed * (_jumped==0 ? 1: AirControl);
        _rb.velocity = Filter.FIR(_rb.velocity, vel, 0.8f);

            
        //Jump
        if (PlayerActions.ActionA.WasPressed)
        {
            if (_jumped++ < 2)
            {
                JumpSound.Play(GetComponent<AudioSource>());
                
                var vel2 = _rb.velocity;
                if (vel2.y < 0)
                {
                    vel2.y = 0;
                }
                vel2.y += Jump[_jumped];
                
                _rb.velocity = vel2;
            }
        }

        //Jumping up is softer than falling down.
        if (_jumped > 0)
        {
            _rb.gravityScale = _rb.velocity.y > 0 ? JumpGravity : FallGravity;            
        }
        else
        {
            _rb.gravityScale = JumpGravity;
        }
        
        //Apply directional scaling
        View.transform.localScale = new Vector3(Mathf.Sign(PlayerActions.Move.X) < 0 ? -1 : 1, 1, 1);
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //If we hit something that is reasonably straight below us, we can jump again.
        for (var i = 0; i < other.contactCount; i++)
        {
            var contact = other.contacts[i];
            if (Vector2.Dot(contact.normal, Vector2.up) > 0.1)
            {
                _jumped = 0;
            }
        }
    }

}