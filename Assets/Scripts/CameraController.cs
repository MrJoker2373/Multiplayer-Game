namespace Game1
{
    using UnityEngine;
    using Unity.Netcode;

    public class CameraController : NetworkBehaviour
    {
        [SerializeField]
        private float _speed;

        private Transform _target;

        private void Start()
        {
            gameObject.SetActive(IsOwner);
        }

        public void Initialize(Transform target)
        {
            _target = target;
        }

        public void ResetAll()
        {
            transform.position = new Vector3(_target.position.x, _target.position.y, transform.position.z);
        }

        private void LateUpdate()
        {
            if (_target != null)
            {
                var targetPosition = new Vector3(_target.position.x, _target.position.y, transform.position.z);
                transform.position = Vector3.Lerp(transform.position, targetPosition, _speed * Time.deltaTime);
            }
        }
    }
}