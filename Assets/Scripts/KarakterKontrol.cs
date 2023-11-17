using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class KarakterKontrol : MonoBehaviour
{
    public static KarakterKontrol Instance;
    public float speed;
    public float playerRunSpeed;
    public bool runActive;
    public float playerCrouchSpeed;
    public bool CrouchActive;

    [Header("Health")]
    public int health;
    public int maxHealth = 100;
    public HealthBar healthBar;

    [Header("Stamina")]
    public float stamina;
    public float maxStamina = 100;
    public bool StaminaResetle;
    public float staminaReset;
    public float staminaResetTime = 6;

    public event System.Action Jumped;


    [Header("keyCodes")]
    public KeyCode jumpkey = KeyCode.Space;
    public KeyCode RunKey = KeyCode.LeftShift;
    public KeyCode CrouchKey = KeyCode.LeftControl;

    [Header("Jump Movement")]

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    public bool jumpReady;

    [Header("Crouch Movement")]
    public Camera _camera;
    [SerializeField]private float defaultCameraHeight = 0.85f;
    [SerializeField]private float defaultColliderHeight = 2f;
    [SerializeField]private float defaultcolliderPositionY = 0f;

    public float crouchCameraHeight = 0f;
    public float crouchColliderHeight = 1f;
    public float crouchColliderPositionY = -0.5f;


    Rigidbody _rigidbody;
    CapsuleCollider _collider;
    public float vxspeed;


    /// <summary> Functions to override movement speed. Will use the last added override. </summary>
    public List<System.Func<float>> speedOverrides = new List<System.Func<float>>();

    public bool groundedPlayer;

    void Awake()
    {
        Instance = Instance != null ? Instance : this;
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<CapsuleCollider>();
        _camera = GetComponentInChildren<Camera>();
    }

    private void Start() {
        health = maxHealth;
        stamina = maxStamina;
    }

    void FixedUpdate()
    {
        MovePlayer();
        vxspeed = _rigidbody.velocity.magnitude;
    }

    void Update() 
    {
        SpeedControl();
        groundedPlayer = GetComponentInChildren<GroundedChechk>().isGrounded;

        if(stamina >= maxStamina)
        {
            stamina = maxStamina;
            staminaReset = 0;
            StaminaResetle = false;
        }
        if (stamina <= 0)
            stamina = 0;

        if(StaminaResetle)
            staminaReset += Time.deltaTime;
            else
            staminaReset = 0;
        
        if (staminaReset >= staminaResetTime && stamina < maxStamina)
            stamina ++;
    }
    


    void MovePlayer() 
    {
        float targetMovingSpeed = speed;
        if (speedOverrides.Count > 0)
        {
            targetMovingSpeed = speedOverrides[speedOverrides.Count - 1]();
        }

        // Get targetVelocity from input.
        Vector3 targetVelocity =new Vector3( Input.GetAxis("Horizontal") * targetMovingSpeed, Input.GetAxis("Vertical") * targetMovingSpeed);

        // Apply movement.
        _rigidbody.velocity = transform.rotation * new Vector3(targetVelocity.x, _rigidbody.velocity.y, targetVelocity.y);
        


        if(Input.GetKey(CrouchKey))
        {
            speed = playerCrouchSpeed;
            CrouchActive = true;
            _camera.transform.localPosition = new Vector3(0, crouchCameraHeight, 0.353f);
            _collider.height = crouchColliderHeight;
            _collider.center = new Vector3(0, crouchColliderPositionY, 0);
        }else if (!Input.GetKey(RunKey))
        {
            speed = 4;
            CrouchActive = false;
            _camera.transform.localPosition = new Vector3(0, defaultCameraHeight, 0.353f);
            _collider.height = defaultColliderHeight;
            _collider.center = new Vector3(0, defaultcolliderPositionY, 0);
        }

        
        if(Input.GetKey(RunKey) && stamina >= 1)
        {
            runActive = true;
            speed = playerRunSpeed;
            stamina --;
        }else if (!Input.GetKey(CrouchKey))
        {
            speed = 4;
            runActive = false;
            StaminaResetle = true;
        }

        //when to jump 
        if(Input.GetKey(jumpkey) && jumpReady && groundedPlayer)
        {
            jumpReady = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    
    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(_rigidbody.velocity.x, 0, _rigidbody.velocity.z);

        //limit velocity if needed
        if(flatVel.magnitude > speed)
        {
            Vector3 limitedVel = flatVel.normalized * speed;
            _rigidbody.velocity = new Vector3(limitedVel.x, _rigidbody.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        //reset y velocity
        _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0f , _rigidbody.velocity.z);

        _rigidbody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        jumpReady = true;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        health = Mathf.Clamp(health, 0, 100); // 0 ile 100 arasında sınırlandır
        healthBar.SetHealth(health);
        if (health <= 0)
        {
            Die();
        }
    }

    
    public void Die()
    {
        Debug.Log("Öldünüz");
    }
}