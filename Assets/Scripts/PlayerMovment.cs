using System.Collections;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;
using WildBall.Player.Inputs;

namespace WildBall.Player
{
    public class PlayerMovment : MonoBehaviour
    {
        [Header("Movement Settings")]
        [SerializeField] private float _rotationSpeed = 100f;
        [SerializeField] private float _forwardTorque = 8f;
        [SerializeField] private float _sideTorque = 8f;

        [Header("Jump Settings")]
        [SerializeField] private float _jumpTorque = 5f;

        [Header("Leap Settings")]
        [SerializeField] private float _leapTorque = 4f;
        [SerializeField] private float _leapCooldown = 1f;

        [Header("Particle effects")]
        [SerializeField] private ParticleSystem _movementEffect = null;

        [HideInInspector] public Vector3 _verticalAxis = new(1, 0, 0);
        [HideInInspector] public Vector3 _horizontalAxis = new(0, 0, 1);
        [HideInInspector] public Vector3 _turnAxis = Vector3.up;
        [HideInInspector] public float Angle;

        private Rigidbody _playerRigidbody;
        private PlayerInput _playerInput;
        private SphereCollider _playerCollider;

        private bool _canLeap = true;
        private bool _isGrounded = true;

        private void Awake()
        {
            _playerCollider = GetComponent<SphereCollider>();
            _playerInput = GetComponent<PlayerInput>();
            _playerRigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            MoveCharacter();
            CheckGrounded();
        }

        /// <summary>
        /// Основная логика управления движением.
        /// </summary>
        private void MoveCharacter()
        {
            ApplyTorque(_verticalAxis, _playerInput.vertical, _forwardTorque);
            ApplyTorque(_horizontalAxis, _playerInput.horizontal, _sideTorque);
            UpdateAxes(1);

            if (_movementEffect != null)
            {
                _movementEffect.transform.position = transform.position - _playerCollider.radius * Vector3.up;

                ParticleSystem.EmissionModule emission = _movementEffect.emission;

                if (_isGrounded)
                    emission.enabled = true;
                else 
                    emission.enabled = false;

            }

            if (_playerInput.jump && _isGrounded)
            {
                Jump();
            }
            if (_playerInput.leap && _canLeap) 
            {
                Leap();
            }
            ResetInputFlags();      
        }

        /// <summary>
        /// Применяет вращающий момент к телу игрока.
        /// </summary>
        private void ApplyTorque(Vector3 axis, float input, float torque)
        {
            if (Mathf.Approximately(input, 0)) return;

            Vector3 torqueVector = axis * input * torque;
            _playerRigidbody.AddTorque(torqueVector);
        }

        private void Jump()
        {
            _playerRigidbody.AddForce(_turnAxis * _jumpTorque, ForceMode.Impulse);
            _isGrounded = false; // Игрок в прыжке
            _playerInput.jump = false;
        }

        private void Leap()
        {
            _playerRigidbody.AddForce(_horizontalAxis * _leapTorque, ForceMode.Impulse);
            _canLeap = false;
            _playerInput.leap = false;

            // Запускаем корутину восстановления рывка
            StartCoroutine(LeapCooldownCoroutine());
        }

        private void ResetInputFlags()
        {
            _playerInput.jump = false;
            _playerInput.leap = false;
        }

        private IEnumerator LeapCooldownCoroutine()
        {
            yield return new WaitForSeconds(_leapCooldown);
            _canLeap = true;
        }

        private void CheckGrounded()
        {
            _isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.2f * _playerCollider.radius);
        }

        /// <summary>
        /// Выберите метод обновления осей: через кватернионы или вручную
        /// </summary>
        /// <param name="mode"> 0 - кватернион / 1 - вручную </param>
        private void UpdateAxes(int mode)
        {
            Angle = _playerInput.angl * _rotationSpeed * Time.deltaTime;
            switch (mode)
            {
                case 0:
                    UpdateAxesUsingQuaternion(Angle);
                    break;
                case 1:
                    UpdateAxesManually(Angle);
                    break;
                default:
                    UpdateAxesUsingQuaternion(Angle);
                    break;
            }
        }

        private void UpdateAxesUsingQuaternion(float angle)
        {
            Quaternion rotation = Quaternion.AngleAxis(angle, _turnAxis);

            _verticalAxis = rotation * _verticalAxis;
            _horizontalAxis = rotation * _horizontalAxis;

            transform.Rotate(rotation.eulerAngles, Space.World);
        }

        private void UpdateAxesManually(float angle)
        {
            UpdateAxisByFormula(ref _verticalAxis);
            UpdateAxisByFormula(ref _horizontalAxis);
            transform.Rotate(_turnAxis, angle, Space.World);
        }

        private void UpdateAxisByFormula(ref Vector3 axis)
        {
            float radians = - Mathf.Deg2Rad * (_playerInput.angl * _rotationSpeed * Time.deltaTime);

            float x = axis.x * Mathf.Cos(radians) - axis.z * Mathf.Sin(radians);
            float z = axis.x * Mathf.Sin(radians) + axis.z * Mathf.Cos(radians);

            axis = new Vector3(x, 0, z).normalized;
        }

#if UNITY_EDITOR
        [ContextMenu("Reset values")]
        public void ResetValues()
        {
            _rotationSpeed = 100f;
            _forwardTorque = 8f;
            _sideTorque = 8f;
            _jumpTorque = 5f;
            _leapTorque = 4f;
            _leapCooldown = 1f;
        }
#endif
    }
}