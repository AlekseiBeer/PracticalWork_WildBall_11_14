using Unity.VisualScripting;
using UnityEngine;
using WildBall.Manager;
using System.Linq;
using System.Collections;

namespace WildBall.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float _distanceRaycast = 2.5f;
        [Header("Particle effects")]
        [SerializeField] private ParticleSystem _repulsionEffect = null;
        [SerializeField] private ParticleSystem _deathEffect = null;

        [HideInInspector] public GameObject ObjectRaycast;

        private Rigidbody _playerRigidbody;
        private PlayerMovment _playerMovment;
        private HintsController _PlayerHints;
        private Collider _playerCollider;

        private void Awake()
        {
            _playerRigidbody = GetComponent<Rigidbody>();
            _playerMovment = GetComponent<PlayerMovment>();
            _PlayerHints = GetComponent<HintsController>();
            _playerCollider = GetComponent<Collider>();
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
                _deathEffect.transform.position = transform.position;
                _playerCollider.enabled = false;
                _playerRigidbody.isKinematic = true;
                Destroy(transform.GetChild(0).gameObject);
                _deathEffect.Play();
                StartCoroutine(HandleDeath());
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

        private IEnumerator HandleDeath()
        {
            yield return new WaitForSeconds(2.5f);
            ScensManager.ResetLavel();
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
                if (_repulsionEffect != null)
                {
                    _repulsionEffect.transform.position = other.GetContact(0).point;
                    _repulsionEffect.Play();
                }
            }
        }
    }
}

