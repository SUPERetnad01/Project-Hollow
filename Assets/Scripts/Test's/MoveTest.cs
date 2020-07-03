using System.ComponentModel;
using UnityEngine;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

namespace Assets.Scripts
{
    /// <summary>
    /// Test Player script Will be rewritten and refactored after test purpose is completed
    /// 
    /// Bug: Dashing doesn't work
    /// 
    /// </summary>
    [AddComponentMenu("Mythirial Test/Move Test")]
    public class MoveTest : MonoBehaviour
    {
        [SerializeField] private Transform _wallChecker;

        [SerializeField] private float groundSpeed = 7.5f;
        [SerializeField] private float rotationSpeed = 15f;
        [SerializeField] private float jumpHeight = 6f;
        [SerializeField] private float groundDistance = 0.5f; 
        [SerializeField] private float dashDistance = 5f;
        [SerializeField] private float groundOffSet = 0;
        [SerializeField] private float fallMultiplier = 2.5f;
        [Range(0, 1)]
        public float AirControlPercent;
        
        private Vector3 _desiredMoveDirection;
        public bool BlockRotationPlayer;
        
        [Header("LayerMasks",order = 0)]
        public LayerMask Ground;
        public LayerMask Climbable;


        [Header("Debug",order = 1)] 
        [SerializeField] private bool _isGrounded;
        [SerializeField] private bool _isClimbable;

        //private Animator animator;
        private Camera _camera;
        private Rigidbody _body;
        private Vector3 _input = Vector3.zero;
        private float _movementSpeed;

        /// <summary>
        /// Called in when this enters the scene
        /// </summary>
        private void Awake()
        {
            //animator = GetComponentInChildren<Animator>();
            _camera = Camera.main;
            _body = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            ComputeInput();

            if (Input.GetKey(KeyCode.Escape))
                Cursor.lockState = CursorLockMode.None;
        }

        /// <summary>
        /// update meant for Physics related coding
        /// </summary>
        private void FixedUpdate()
        {
            CamLock();
            MovePlayer();
        }

        /// <summary>
        /// ComputeInput (Subject to change)
        /// Basis of the movement system
        /// </summary>
        private void ComputeInput()
        {
            //animator.SetFloat("Speed", movement.magnitude);

           _isGrounded = Physics.Raycast(new Vector3(transform.position.x, transform.position.y + groundOffSet, transform.position.z), Vector3.down, groundDistance + groundOffSet, Ground);

           Vector3 wallDirection = _wallChecker.transform.TransformDirection(Vector3.forward);
           float distance = Vector3.Distance(transform.position, _wallChecker.position);
            _isClimbable = Physics.Raycast(new Vector3(transform.position.x, transform.position.y + groundOffSet, transform.position.z), wallDirection,distance, Climbable);

            _input = Vector3.zero;
            _input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

            CamLock();

            if (!_isGrounded)
                _movementSpeed = groundSpeed * AirControlPercent;
            else
                _movementSpeed = groundSpeed;


            if (_input != Vector3.zero)
                transform.forward = _input;

            
            if (Input.GetButtonDown("Jump") && _isGrounded)
            {
                _body.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
            }

            if (_isClimbable)
            {

            }


            if (Input.GetButtonDown("Dash"))
            {
                Vector3 dashVelocity = Vector3.Scale(transform.forward, dashDistance * new Vector3((Mathf.Log(1f / ((Time.deltaTime * _body.drag) + 1)) / -Time.deltaTime), 0, (Mathf.Log(1f / (Time.deltaTime * _body.drag + 1)) / -Time.deltaTime)));
                _body.AddForce(dashVelocity, ForceMode.VelocityChange);
            }

            if (_desiredMoveDirection.magnitude > 0)
            {
                if (BlockRotationPlayer == false)
                {
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_desiredMoveDirection), rotationSpeed); //Points in direction relative to the camera
                }
            }
        }

        /// <summary>
        /// ComputeInput physics function
        /// </summary>

       private void OnDrawGizmos()
        {
            Vector3 fwd = _wallChecker.transform.TransformDirection(Vector3.forward);
            Gizmos.DrawRay(transform.position, fwd);
        }
        void MovePlayer()
        {
            _body.MovePosition(_body.position + _desiredMoveDirection * _movementSpeed * Time.fixedDeltaTime); //Moves us in the desiredWay
            _body.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }


        /// <summary>
        /// Calculate player rotation to be relative to camera rotation
        /// </summary>
        void CamLock()
        {
            var forward = _camera.transform.forward;
            var right = _camera.transform.right;

            forward.y = 0f;
            right.y = 0f;

            forward.Normalize();
            right.Normalize();

            _desiredMoveDirection = forward * _input.z + right * _input.x;
        }
    }
}