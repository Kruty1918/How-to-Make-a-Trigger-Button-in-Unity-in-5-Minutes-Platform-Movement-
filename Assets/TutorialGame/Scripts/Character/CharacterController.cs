using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RigidbodyCharacterController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _walkForwardSpeed = 3f;
    [SerializeField] private float _walkBackwardSpeed = 1f;

    [Header("Animations")]
    [SerializeField] private float _blendAnimationSpeed = 1f;
    [SerializeField] private Animator _animator;
    [SerializeField] private string _animatorParamMoveYName = "MoveY";

    private Rigidbody _rigidbody;
    private Vector2 _input;
    private Vector3 _moveDirection;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.constraints = RigidbodyConstraints.FreezeRotation; // Щоб не падало
    }

    private void Update()
    {
        ReadInput();
        Animate();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void ReadInput()
    {
        _input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
    }

    private void Move()
    {
        float currentSpeed = _input.y < 0 ? _walkBackwardSpeed : _walkForwardSpeed;
        _moveDirection = new Vector3(_input.x, 0f, _input.y) * currentSpeed;

        Vector3 newVelocity = _moveDirection;
        newVelocity.y = _rigidbody.linearVelocity.y; // залишити гравітацію

        _rigidbody.linearVelocity = newVelocity;
    }

    private void Animate()
    {
        if (_animator != null)
        {
            _animator.SetFloat(_animatorParamMoveYName, _input.y, _blendAnimationSpeed, Time.deltaTime);
        }
    }
}