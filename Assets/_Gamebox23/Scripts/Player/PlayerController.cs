using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerInputs))]
public class PlayerController : MonoBehaviour
{
    [SerializeField, Range(0, 10)] private float _speed = 4f;
    [SerializeField, Range(100, 1000)] private float _rotateSpeed = 500f;

    private Rigidbody _playerRB;
    //private Animator _playerAnimator;
    private PlayerInputs _playerMovement;

    private void Awake()
    {
        _playerRB = GetComponent<Rigidbody>();
        //_playerAnimator = GetComponent<Animator>();
        _playerMovement = GetComponent<PlayerInputs>();
    }

    private void Update()
    {
        //_playerAnimator.SetFloat("Velocity", _playerRB.velocity.magnitude);
    }

    private void FixedUpdate()
    {
        Move();
    }

    /// <summary>
    /// Перемещение игрока
    /// </summary>
    private void Move()
    {
        Quaternion rotation = Quaternion.RotateTowards(transform.rotation, _playerMovement.Rotation, 
            _rotateSpeed * Time.fixedDeltaTime);
        
        _playerRB.AddForce(_playerMovement.Movement * _speed);
        _playerRB.MoveRotation(rotation);
    }

#if UNITY_EDITOR
    [ContextMenu("Reset values")]
    public void ResetValues()
    {
        _speed = 4f;
        _rotateSpeed = 500f;
    }
#endif
}