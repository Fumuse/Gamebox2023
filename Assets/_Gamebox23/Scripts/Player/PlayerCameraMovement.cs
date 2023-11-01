using UnityEngine;

public class PlayerCameraMovement : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    private Vector3 _offset;

    private void Start()
    {
        _offset = transform.position - _playerTransform.position;
    }

    private void FixedUpdate()
    {
        transform.position = _playerTransform.position + _offset;
    }
}
