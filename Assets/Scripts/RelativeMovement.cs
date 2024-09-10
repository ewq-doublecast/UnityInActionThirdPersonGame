using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class RelativeMovement : MonoBehaviour
{
    private const string Horizontal = "Horizontal";
    private const string Vertical = "Vertical";
    private const string Jump = "Jump";

    private static readonly int JumpingHash = Animator.StringToHash("IsJumping");
    private static readonly int SpeedHash = Animator.StringToHash("Speed");

    private const float Gravity = -9.8f;

    [SerializeField]
    private Transform _target;

    [SerializeField]
    private float _rotationSpeed = 1.5f;

    [SerializeField]
    private float _moveSpeed = 6.0f;

    [SerializeField]
    private float _jumpForce = 15.0f;

    [SerializeField]
    private float _terminalVelocity = -10.0f;

    [SerializeField]
    private float _minimalFallSpeed = -1.5f;

    [SerializeField]
    private float _gravityScale = 5.0f;

    [SerializeField]
    private float _characterHeightDivider = 1.9f;

    [SerializeField]
    private float _pushForce = 3.0f;

    private float _verticalSpeed;
    private CharacterController _characterController;
    private ControllerColliderHit _contact;
    private Animator _animator;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();

        _verticalSpeed = _minimalFallSpeed;
    }

    private void Update()
    {
        Vector3 movement = Vector3.zero;

        float horizontalInput = Input.GetAxis(Horizontal);
        float verticalInput = Input.GetAxis(Vertical);

        if (horizontalInput != 0 || verticalInput != 0)
        {
            movement.x = horizontalInput * _moveSpeed;
            movement.z = verticalInput * _moveSpeed;
            movement = Vector3.ClampMagnitude(movement, _moveSpeed);

            Quaternion targetRotation = _target.rotation;

            _target.eulerAngles = new Vector3(0, _target.eulerAngles.y, 0);
            movement = _target.TransformDirection(movement);

            _target.rotation = targetRotation;

            Quaternion direction = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Lerp(transform.rotation, direction, _rotationSpeed * Time.deltaTime);
        }

        _animator.SetFloat(SpeedHash, movement.sqrMagnitude);

        bool isGrounded = false;

        if (_verticalSpeed < 0 && Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit))
        {
            float cutCharacterHeight = (_characterController.height + _characterController.radius) / _characterHeightDivider;
            isGrounded = hit.distance <= cutCharacterHeight;
            Debug.DrawRay(transform.position, Vector3.down);
        }

        if (isGrounded) 
        {
            if (Input.GetButtonDown(Jump))
            {
                _verticalSpeed = _jumpForce;
            }
            else
            {
                _verticalSpeed = _minimalFallSpeed;
                _animator.SetBool(JumpingHash, false);
            }
        }
        else
        {
            _verticalSpeed += Gravity * _gravityScale * Time.deltaTime;

            if (_verticalSpeed < _terminalVelocity) 
            {
                _verticalSpeed = _terminalVelocity;
            }

            if (_contact != null)
            {
                _animator.SetBool(JumpingHash, true);
            }

            if (_characterController.isGrounded)
            {
                if (Vector3.Dot(movement, _contact.normal) < 0)
                {
                    movement = _contact.normal * _moveSpeed;
                }
                else
                {
                    movement += _contact.normal * _moveSpeed;
                }
            }
        }

        movement.y = _verticalSpeed;
        movement *= Time.deltaTime;

        _characterController.Move(movement);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        _contact = hit;

        Rigidbody rigidbody = hit.collider.attachedRigidbody;

        if (rigidbody != null && rigidbody.isKinematic == false) 
        {
            rigidbody.velocity = hit.moveDirection * _pushForce;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector3.down);
    }
}
