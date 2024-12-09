using Unity.VisualScripting;
using UnityEngine;
using WildBall.Player;


public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Object _player;
    [SerializeField] private float smoothSpeed = 0.125f;
    
    private Transform _playerTransform = null;
    private PlayerMovment _playerMovment = null;

    private void Awake()
    {
        _playerTransform = _player.GetComponent<Transform>();
        _playerMovment = _player.GetComponent<PlayerMovment>();
    }

    private void FixedUpdate()
    {
        Vector3 desiredPosition = _playerTransform.position;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;


        transform.Rotate(Vector3.up, _playerMovment.Angle);
    }
}
