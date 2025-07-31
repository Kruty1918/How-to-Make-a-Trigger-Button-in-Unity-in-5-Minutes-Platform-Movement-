using UnityEngine;

[RequireComponent(typeof(UnityEngine.CharacterController))]
public class CharacterControllerTDD : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _walkForwardSpeed = 3f;
    [SerializeField] private float _walkBackwardSpeed = 1f;

    [Header("Animations")] [SerializeField]
    private float _blendAnimationSpeed = 1;
    
    [SerializeField] private Animator _animator;
    [SerializeField] private string _animatorParamMoveYName = "MoveY";

    private UnityEngine.CharacterController _characterController;
    private Vector2 _input;
    private Vector3 _moveDirection;

    private void Awake()
    {
        _characterController = GetComponent<UnityEngine.CharacterController>();
    }

    private void Update()
    {
        ReadInput();
        Move();
        Animate();
    }

    private void ReadInput()
    {
        _input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
    }

    private void Move()
    {
        float currentSpeed = _input.y < 0 ? _walkBackwardSpeed : _walkForwardSpeed;

        _moveDirection = new Vector3(_input.x, 0f, _input.y) * currentSpeed;
        _characterController.Move(_moveDirection * Time.deltaTime);
    }

    private void Animate()
    {
        if (_animator != null)
        {
            _animator.SetFloat(_animatorParamMoveYName, _input.y, _blendAnimationSpeed, Time.deltaTime);
        }
    }
}