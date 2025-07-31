using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("移动参数")]

    [SerializeField] float hurtForce = 1f;
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float jumpForce = 3f;
    [SerializeField] float wallJumpForce = 3f;
    [SerializeField] public float jumpTimer = 0.5f;
    [SerializeField] Vector3 flippedScale = new Vector3(-1, 1, 1);
    [SerializeField] int moveChangeAni;
    [SerializeField] float maxGravityVelocity;
    [SerializeField] float groundedGravityScale;
    [SerializeField] float jumpGravityScale;
    [SerializeField] float fallGravityScale;
    [SerializeField] float slideGravityScale;
    [SerializeField] float wallSlideJumpForce;

    float moveX;
    float moveY;
    private Vector2 vectorInput;

    [Header("引用组件")]
    private CharacterEffect characterEffect;

    private CharacterAudio characterAudio;

    private CharacterData data;

    private Rigidbody2D rigi;
    private Animator animator;
    private CinemaShaking cinemaShaking;
    private Attack attack;
    private GameManager gameManager;
    private AudioSource audio;

    [Header("状态判断")]

    [SerializeField] bool isFacingRight;
    [SerializeField] bool isOnGround;
    [SerializeField] bool canMove;
    [SerializeField] bool isSliding;
    [SerializeField] bool isFalling;
    [SerializeField] bool isJumping;
    [SerializeField] bool jumpInput;
    [SerializeField] bool enabledGravity;
    [SerializeField] bool firstLanding;

    [Header("攻击参数")]

    float lastSlashTime;
    [SerializeField] float slashIntervalTime = 0.2f;
    [SerializeField] float maxComboTime = 0.4f;
    [SerializeField] float recoilForce = 5f;
    [SerializeField] int slashCount;
    [SerializeField] int slashDamage = 1;
    [SerializeField] float downRecoilForce = 10f;

    private void OnEnable()
    {
        InputManager.InputController.Player.Move.performed += ctx => vectorInput = ctx.ReadValue<Vector2>();
        InputManager.InputController.Player.Jump.started += Jump_started;
        InputManager.InputController.Player.Jump.performed += Jump_performed;
        InputManager.InputController.Player.Jump.canceled += Jump_canceled;

        InputManager.InputController.Player.Attack.started += Attack_started;
        InputManager.InputController.Player.Attack.performed += Attack_performed;
        InputManager.InputController.Player.Attack.canceled += Attack_canceled;

    }
    private void OnDisable()
    {
        InputManager.InputController.Player.Jump.started -= Jump_started;
        InputManager.InputController.Player.Jump.performed -= Jump_performed;
        InputManager.InputController.Player.Jump.canceled -= Jump_canceled;

        InputManager.InputController.Player.Attack.started -= Attack_started;
        InputManager.InputController.Player.Attack.performed -= Attack_performed;
        InputManager.InputController.Player.Attack.canceled -= Attack_canceled;

    }
    // Start is called before the first frame update
    void Start()
    {
        characterEffect = FindAnyObjectByType<CharacterEffect>();
        characterAudio = FindObjectOfType<CharacterAudio>();
        data = FindObjectOfType<CharacterData>();
        attack = FindObjectOfType<Attack>();
        rigi = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        gameManager = FindObjectOfType<GameManager>();
        cinemaShaking = FindObjectOfType<CinemaShaking>();
        audio = GetComponent<AudioSource>();

        enabledGravity = true;
        canMove = true;
        animator.SetBool("FirstLanding", firstLanding);

    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        UpdateJump();
        UpdateVelocity();
        UpdateDirection();
        UpdateGravityScale();
    }

    void Update()
    {

        ResetComboTime();
        // Jump();
        // Movement();
        // PlayerAttack();
        // Direction();
    }

    #region Move

    private void UpdateVelocity()
    {
        if (!data.GetDeadStatement())
        {
            Vector2 velocity = rigi.velocity;
            if (isSliding && vectorInput.x != 0)
            {
                velocity.y = Mathf.Clamp(velocity.y, -maxGravityVelocity / 2, maxGravityVelocity / 2);
            }
            else
            {
                velocity.y = Mathf.Clamp(velocity.y, -maxGravityVelocity, maxGravityVelocity);
            }
            animator.SetFloat("VelocityY", rigi.velocity.y);

            if (canMove && gameManager.IsEnableInput())
            {
                rigi.velocity = new Vector2(vectorInput.x * moveSpeed, velocity.y);
                animator.SetInteger("movement", (int)vectorInput.x);
            }
        }
        else
        {
            Vector2 velocity = rigi.velocity;
            velocity.x = 0;
            velocity.y = Mathf.Clamp(velocity.y, -maxGravityVelocity, maxGravityVelocity);
            rigi.velocity = velocity;
        }
    }

    private void UpdateDirection()
    {
        if (canMove&&!data.GetDeadStatement())
        {
            if (rigi.velocity.x>1 && !isFacingRight)
            {
                transform.localScale = flippedScale;
                isFacingRight = true;
            }
            else if (rigi.velocity.x<-1 && isFacingRight)
            {
                transform.localScale = Vector3.one;
                isFacingRight = false;
            }
        }

    }
  
    private void UpdateGravityScale()
    {
        float gravityScale = groundedGravityScale;
        if (!isOnGround)
        {
            if (isSliding && vectorInput.x != 0)
                gravityScale = slideGravityScale;
            else
            {
                gravityScale = rigi.velocity.y > 0.0f ? jumpGravityScale : fallGravityScale;
            }
        }
        if (!enabledGravity)
            gravityScale = 0;

        rigi.gravityScale = gravityScale;
    }
    IEnumerator GrabWallJump()
    {
        gameManager.SetEnableInput(false);
        enabledGravity = false;
        animator.SetTrigger("slideJump");
        rigi.velocity = new Vector2(transform.lossyScale.x * wallSlideJumpForce, wallSlideJumpForce);
        yield return new WaitForSeconds(0.4f);
        enabledGravity = true;
        gameManager.SetEnableInput(true);
        animator.ResetTrigger("slideJump");
    }

    #endregion
    #region Jump
    private void Jump_started(InputAction.CallbackContext ctx)
    {
        if (data.GetDeadStatement())
            return;
        if (isSliding && !isOnGround)
        {
            //TODO GrabWallJump()
            StartCoroutine(GrabWallJump());
            
        }
        else
        {
            if (!gameManager.IsEnableInput())
                return;
            animator.SetTrigger("jump");
            characterAudio.Play(CharacterAudio.AudioType.Jump, true);
        }
        jumpInput = true;
    }
    private void Jump_performed(InputAction.CallbackContext ctx)
    {
        JumpCancle();
    }
    private void Jump_canceled(InputAction.CallbackContext ctx)
    {
        JumpCancle();
    }

    private void JumpCancle()
    {
        jumpInput = false;
        isJumping = false;
        animator.ResetTrigger("jump");
    }

    private void UpdateJump()
    {
        if (isJumping && rigi.velocity.y < 0)
            isFalling = true;
        if (jumpInput && gameManager.IsEnableInput())
        {
            rigi.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);

            isJumping = true;

            characterEffect.DoEffect(CharacterEffect.EffectType.FallTrail, false);
        }

    }


    #endregion
    #region Grounding
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Grounding(collision, false);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        Grounding(collision, false);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        Grounding(collision, true);
    }

    private void Grounding(Collision2D col, bool exitState)
    {
        if (exitState) //离开为真
        {
            if (col.gameObject.layer == LayerMask.NameToLayer("Terrain"))
            {
                isOnGround = false;
                isSliding = false;
            }
                
        }
        else
        {
            if (col.gameObject.layer == LayerMask.NameToLayer("Terrain") && !isOnGround && col.contacts[0].normal == Vector2.up) //从上往下
            {
                characterEffect.DoEffect(CharacterEffect.EffectType.FallTrail, true);

                isOnGround = true;

                isJumping = false;
                isFalling = false;
                SetIsSliding(false);
            }
            else if (col.gameObject.layer == LayerMask.NameToLayer("Terrain") && isJumping && col.contacts[0].normal == Vector2.down)
            {
                JumpCancle();
            }


        }
        animator.SetBool("isOnGround", isOnGround);

    }
    #endregion
    #region Attack And Damage
    private void Attack_started(InputAction.CallbackContext ctx)
    {
        if (gameManager.IsEnableInput() && !data.GetDeadStatement())
        {
            if (Time.time >= lastSlashTime + slashIntervalTime)
            {
                lastSlashTime = Time.time;
                if (vectorInput.y > 0)
                {
                    SlashAndDetect(Attack.AttackType.UpSlash);
                    animator.Play("UpSlash");

                }
                else if (!isOnGround && vectorInput.y < 0)
                {
                    SlashAndDetect(Attack.AttackType.DownSlash);
                    animator.Play("DownSlash");
                }
                else
                {
                    slashCount++;
                    switch (slashCount)
                    {
                        case 1:
                            //Slash
                            SlashAndDetect(Attack.AttackType.Slash);
                            animator.Play("Slash");
                            break;
                        case 2:
                            SlashAndDetect(Attack.AttackType.AltSlash);
                            animator.Play("AltSlash");
                            slashCount = 0;
                            break;
                    }
                }
            }
        }
    }
    private void Attack_performed(InputAction.CallbackContext ctx)
    {

    }
    private void Attack_canceled(InputAction.CallbackContext ctx)
    {

    }
    private void SlashAndDetect(Attack.AttackType attackType)
    {
        List<Collider2D> colliders = new List<Collider2D>();
        attack.Play(attackType, ref colliders);

        bool hasEnemy = false;
        bool hasTrap = false;

        //检测是否是敌人
        foreach (Collider2D col in colliders)
        {
            if (col.gameObject.layer == LayerMask.NameToLayer("EnemyDetector"))
            {
                hasEnemy = true;

                break;
            }
        }
        foreach (Collider2D col in colliders)
        {
            if (col.gameObject.layer == LayerMask.NameToLayer("DamagePlayer"))
            {
                hasTrap = true;

                break;
            }

        }
        if (hasEnemy)
        {
            //Recoil
            if (attackType == Attack.AttackType.DownSlash)
            {
                AddDownRecoilForce();
            }
            else
            {
                StartCoroutine(AddRecoilForce());
            }

        }
        if (hasTrap)
        {
            if (attackType == Attack.AttackType.DownSlash)
            {
                characterAudio.Play(CharacterAudio.AudioType.RejectHit, true);
                AddDownRecoilForce();
            }
        }

        foreach (Collider2D col in colliders)
        {
            Breakable breakable = col.GetComponent<Breakable>();
            if (breakable != null)
            {
                breakable.Hurt(slashDamage, transform);
            }

        }


    }
    private void ResetComboTime()
    {
        if (Time.time >= lastSlashTime + maxComboTime && slashCount != 0)
        {
            slashCount = 0;
        }

    }
    private void AddDownRecoilForce()
    {
        rigi.velocity.Set(rigi.velocity.x, 0);
        rigi.AddForce(Vector2.up * downRecoilForce, ForceMode2D.Impulse);
    }
    IEnumerator AddRecoilForce()
    {
        canMove = false;
        if (isFacingRight)
        {
            rigi.AddForce(Vector2.left * recoilForce, ForceMode2D.Impulse);

        }
        else
        {
            rigi.AddForce(Vector2.right * recoilForce, ForceMode2D.Impulse);
        }
        yield return new WaitForSeconds(0.2f);
        canMove = true;
    }
    public void TakeDamage()
    {
        cinemaShaking.CinemaShake();
        gameManager.SetEnableInput(false);

        FindObjectOfType<Health>().Hurt();
        if (!data.GetDeadStatement())
        {
            StartCoroutine(FindObjectOfType<Invincibility>().SetInvincibility());
            if (isFacingRight)
            {
                rigi.velocity = new Vector2(1, 1) * hurtForce;
            }
            else
                rigi.velocity = new Vector2(-1, 1) * hurtForce;
        }


        animator.Play("TakeDamage");

        characterAudio.Play(CharacterAudio.AudioType.TakeDamage, true);

    }

    #endregion
    #region Others
    public void PlayHitParticals()
    {
        characterEffect.DoEffect(CharacterEffect.EffectType.HitL, true);
        characterEffect.DoEffect(CharacterEffect.EffectType.HitR, true);

    }
    public void FirstLand()
    {
        StopInput();
        characterEffect.DoEffect(CharacterEffect.EffectType.BurstRocks, true);
    }
    public void StopInput()
    {
        gameManager.SetEnableInput(false);
        StopHorizontalMovement();
    }
    public void ResumeInput()
    {
        gameManager.SetEnableInput(true);
    }
    public void StopHorizontalMovement()
    {
        Vector2 velocity = rigi.velocity;
        velocity.x = 0;
        rigi.velocity = velocity;
        animator.SetInteger("movement", 0);
    }
    public void PlayMusic(AudioClip clip)
    {
        audio.PlayOneShot(clip);
    }

    public void ResetFallDistance()
    {
        animator.GetBehaviour<FallingBehaviour>().ResetAllParams();
    }

    public void SetIsOnGrounded(bool state)
    {
        isOnGround = state;
        if (!data.GetDeadStatement())
        {
            animator.SetBool("isOnGround", isOnGround);
        }
    }

    public void SetIsSliding(bool state)
    {
        isSliding = state;
        if (!data.GetDeadStatement())
        {
            animator.SetBool("sliding", isSliding);
        }
    }

    #endregion

}
