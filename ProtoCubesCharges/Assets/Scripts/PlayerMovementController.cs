using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovementController : MonoBehaviour
{
    [Serializable]
    public class MovementSettings
    {
        public float ForwardSpeed = 65;   // Speed when walking forward
        public float StrafeSpeed = 45;    // Speed when walking sideways or backwards
        public float JumpForce = 150;


        [HideInInspector] public float CurrentTargetSpeed;

        public void UpdateDesiredTargetSpeed(Vector2 input)
        {
            if (input == Vector2.zero) return;
            if (input.x > 0 || input.x < 0) //strafe
            {
                CurrentTargetSpeed = StrafeSpeed;
            }
            if (input.y < 0) //backwards
            {
                CurrentTargetSpeed = StrafeSpeed;
            }
            if (input.y > 0) //forwards
            {
                //handled last as if strafing and moving forward at the same time forwards speed should take precedence
                CurrentTargetSpeed = ForwardSpeed;
            }
        }
    }

    public Camera cam;
    public LayerMask walkableLayers;
    public MovementSettings movementSettings = new MovementSettings();
    public MouseLook mouseLook = new MouseLook();

    private Rigidbody m_RigidBody;
    private bool m_Jump, m_Jumping, m_IsGrounded;
    private CapsuleCollider m_Capsule;

    [SerializeField, Range(0f, 1f)] private float decelerationPercentage = 0.1f;


    private void Start()
    {
        m_RigidBody = GetComponent<Rigidbody>();
        m_Capsule = GetComponent<CapsuleCollider>();
    }

    private void OnEnable()
    {
        mouseLook.Init(transform, cam.transform);
        //UIManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
    }


    private void Update()
    {
        RotateView();

        if (Input.GetButtonDown("Jump") && !m_Jump)
        {
            m_Jump = true;
        }

    }

    private void FixedUpdate()
    {
        GroundCheck();
        Vector2 input = GetInput();

        movementSettings.UpdateDesiredTargetSpeed(input);

        if ((Mathf.Abs(input.x) > float.Epsilon || Mathf.Abs(input.y) > float.Epsilon))
        {
            // always move along the camera forward as it is the direction that it being aimed at
            Vector3 desiredMove = cam.transform.forward * input.y + cam.transform.right * input.x;

            desiredMove = Vector3.ProjectOnPlane(desiredMove, Vector3.up).normalized;
            desiredMove *= movementSettings.CurrentTargetSpeed;

            if (m_RigidBody.velocity.sqrMagnitude <
                (movementSettings.CurrentTargetSpeed * movementSettings.CurrentTargetSpeed))
            {
                m_RigidBody.AddForce(desiredMove, ForceMode.Impulse);
            }
        }

        if (m_IsGrounded && m_Jump)
        {
            m_RigidBody.velocity = new Vector3(m_RigidBody.velocity.x, 0f, m_RigidBody.velocity.z);
            m_RigidBody.AddForce(new Vector3(0f, movementSettings.JumpForce, 0f), ForceMode.Impulse);
        }

        m_Jump = false;
        ApplyDeceleration(); //Application de la resistance au sol et a l'air
    }


    private Vector2 GetInput()
    {
        Vector2 input = new Vector2
        {
            x = Input.GetAxisRaw("Horizontal"),
            y = Input.GetAxisRaw("Vertical")
        };
        return input;
    }

    private void RotateView()
    {
        //avoids the mouse looking if the game is effectively paused
        if (Mathf.Abs(Time.timeScale) < float.Epsilon) return;

        // get the rotation before it's changed
        float oldYRotation = transform.eulerAngles.y;

        mouseLook.LookRotation(transform, cam.transform);

        // Rotate the rigidbody velocity to match the new direction that the character is looking
        Quaternion velRotation = Quaternion.AngleAxis(transform.eulerAngles.y - oldYRotation, Vector3.up);
        m_RigidBody.velocity = velRotation * m_RigidBody.velocity;

    }

    private void ApplyDeceleration()
    {
        if (m_IsGrounded)
            m_RigidBody.velocity *= decelerationPercentage;
        else
        {
            float yAxisVelocity = m_RigidBody.velocity.y;
            m_RigidBody.velocity = new Vector3(m_RigidBody.velocity.x * decelerationPercentage, yAxisVelocity, m_RigidBody.velocity.z * decelerationPercentage);
        }

    }

    private void GroundCheck()
    {
        RaycastHit hitInfo;
        if (Physics.SphereCast(transform.position, m_Capsule.radius * (0.9f), Vector3.down, out hitInfo,
                               ((m_Capsule.height / 2f) - m_Capsule.radius) + 0.2f, walkableLayers, QueryTriggerInteraction.Ignore))
        {
            m_IsGrounded = true;
        }
        else
        {
            m_IsGrounded = false;
        }
    }

}
