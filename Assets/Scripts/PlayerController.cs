using Unity.VisualScripting;
using UnityEngine;
using WildBall.Manager;
using System.Linq;

namespace WildBall.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float _distanceRaycast = 2.5f;
        [HideInInspector] public GameObject ObjectRaycast;

        private Rigidbody _playerRigidbody;
        private PlayerMovment _playerMovment;
        private HintsController _PlayerHints;

        private void Awake()
        {
            _playerRigidbody = GetComponent<Rigidbody>();
            _playerMovment = GetComponent<PlayerMovment>();
            _PlayerHints = GetComponent<HintsController>();
        }

        void Update()
        {
        }

        public GameObject GetObjectRaucastForvard()
        {
            GameObject obj = null;
            if (Physics.Raycast(transform.position, _playerMovment._horizontalAxis, out RaycastHit hit, _distanceRaycast))
            {
                obj = hit.collider.GameObject();
            }
            return obj;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Death"))
            {
                ScensManager.ResetLavel();
            }

            if (other.CompareTag("Finish"))
            {
                ScensManager.NextLavel();
            }

            if (other.CompareTag("HintTrigger"))
            {
                var hint = _PlayerHints._hints.FirstOrDefault(h => h._hintTrigger == other);
                _PlayerHints.ShowHint(hint._hintText);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("HintTrigger"))
            {
                _PlayerHints.HideHint();
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.collider.CompareTag("Impulse"))
            {
                Vector3 direction_impuls = (transform.position - other.GetContact(0).point).normalized;
                _playerRigidbody.AddForce(direction_impuls * 10, ForceMode.Impulse);
            }
        }
    }
}

