using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerInputs : MonoBehaviour
{
    #region Player Inputs 
    
    public const string HorizontalAxis = "Horizontal";
    public const string VerticalAxis = "Vertical";
    public const KeyCode ActionButton = KeyCode.E;

    #endregion
    
    private Vector3 _movement;
    public Vector3 Movement
    {
        get { return _movement; }
    }
    
    private Quaternion _rotation;
    public Quaternion Rotation
    {
        get { return _rotation; }
    }
    
    void Update() 
    {
        float horizontal = Input.GetAxis(HorizontalAxis);
        float vertical = Input.GetAxis(VerticalAxis);
        _movement = new Vector3(horizontal, 0, vertical).normalized;

        if (_movement != Vector3.zero)
            _rotation = Quaternion.LookRotation(_movement);
    }
}