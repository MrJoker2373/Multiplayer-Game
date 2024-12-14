namespace Game1
{
    using UnityEngine;
    using UnityEngine.InputSystem;
    using Unity.Netcode;

    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Animator))]
    public class PlayerController : NetworkBehaviour
    {
        private const string IDLE_KEY = "Idle";
        private const string RUN_KEY = "Run";

        [SerializeField]
        private CameraController _camera;

        [SerializeField]
        private InputAction _movementInput;

        [SerializeField]
        private float _speed;

        private Rigidbody2D _rigidbody;
        private Animator _animator;
        private Vector2 _direction;

        public CameraController Camera => _camera;

        public void Initialize()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _camera.Initialize(transform);
        }

        public void Enable()
        {
            _movementInput.Enable();
            _movementInput.performed += context => SetDirection(context.ReadValue<Vector2>());
        }

        public void Disable()
        {
            _movementInput.Disable();
            _movementInput.performed -= context => SetDirection(context.ReadValue<Vector2>());
        }

        public void ResetAll()
        {
            SetDirection(Vector2.zero);
        }

        private void Start() => Initialize();

        private void OnEnable() => Enable();

        private void OnDisable() => Disable();

        private void FixedUpdate()
        {
            if (_direction != Vector2.zero)
                _rigidbody.velocity += _direction * _speed * Time.deltaTime;
        }

        private void SetDirection(Vector2 direction)
        {
            if (IsOwner == false)
                return;
            _direction = direction;
            if (_direction == Vector2.zero)
                _animator.Play(IDLE_KEY);
            else
            {
                _animator.Play(RUN_KEY);
                var scale = _rigidbody.transform.localScale;
                if (_direction.x > 0 && scale.x < 0 || _direction.x < 0 && scale.x > 0)
                {
                    scale.x = -scale.x;
                    _rigidbody.transform.localScale = scale;
                }
            }
        }
    }
}