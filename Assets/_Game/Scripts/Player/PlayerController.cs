using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : GameUnit, IHit
{
    [SerializeField] Animator anim;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] CapsuleCollider2D capsuleCollider2D;
    public SpriteRenderer sr;
    bool isDead;
    bool extraLife;

    [Header("Knockback info")]
    [SerializeField] Vector2 knockbackDir;
    bool isKnocked;
    bool canBeKnocked;

    [Header("Speed info")]
    [SerializeField] float moveSpeed;
    [SerializeField] float maxSpeed;
    [SerializeField] float speedMutiplier;
     float defaultSpeed;
    [Space]
    [SerializeField] float milestoneIncreaser;
    float defaultMilestoneIncrease;
    float speedMilestone;
    
    [Header("Jump info")]
    [SerializeField] float jumpForce;
    [SerializeField] float doubleJumpForce;
    bool isCanDouleJump;
    

    [Header("Slide info")]
    [SerializeField] float sildeSpeed;
    [SerializeField] float sildeTime;
    [SerializeField] float sildeCooldown;
    float silderCoolDownCounter;
    bool isSilding;
    float slideTimeCounter;

    
    [Header("Collision Check")]
    [SerializeField] float groundCheckDistant;
    [SerializeField] LayerMask groundLayerMask;
    [SerializeField] Transform wallCheck;
    [SerializeField] Vector2 wallCheckSize;
    [SerializeField] float cellCheckDistant;

    [Header("Ledge Check")]
    [SerializeField] float radius;
    [SerializeField] LegdeDetected ledgeDetected;
    [SerializeField] Vector2 offsetBeforeClimp;
    [SerializeField] Vector2 offsetAfterClimp;
    Vector2 climbBegunPosition;
    Vector2 climbOverPosition;
    bool canGrapLedge;
    bool canClimb;

    int coin;
    float distance;


    private void Update() 
    { 
        
        if(!GameManager.Instance.IsState(GameState.GamePlay))
        {
            return;
        }
            
        
        AnimatorController();
        slideTimeCounter -= Time.deltaTime;
        silderCoolDownCounter -= Time.deltaTime;
        extraLife = moveSpeed >= maxSpeed;


        if(isKnocked || isDead)
            return;

        Moving();
        

        if(isGrounded())
            isCanDouleJump = true;
        

        SpeedController();
        CheckForLedge();
        CheckForSilde();

        CheckInput();

        if(TF.position.x > distance)
        {
            distance = TF.position.x;
        }
        
    }

    public void OnInit()
    {

        isSilding = isCanDouleJump = canClimb = isKnocked = isDead = false;
        canGrapLedge  = canBeKnocked = true;

        speedMilestone = milestoneIncreaser = 50f ;
        defaultSpeed = moveSpeed = 12f;
        defaultMilestoneIncrease = milestoneIncreaser = 50f;

        coin = 0;
        distance = 0;
        TF.position =  LevelManager.Instance.GetStartPoint();
        AnimatorController();
        anim.SetBool("isDead", false);
    }

    public int GetCoin() => coin;
    public bool GetExtraLife() => extraLife;
    public float GetDistance() => distance;
    IEnumerator OnDeath()
    {
        isDead = true;
        canBeKnocked = false;
        rb.velocity = knockbackDir;
        anim.SetBool("isDead", isDead);

        yield return new WaitForSeconds(1f);

        rb.velocity = new Vector2(0, 0);
        yield return new WaitForSeconds(1f);
        UserData.Ins.SaveInfo(coin, distance);
        LevelManager.Instance.FinishGame();
    }

    #region Knockback
    private IEnumerator Invincibility()
    {
        Color originalColor = sr.color;
        Color darkenColor = new Color(sr.color.r, sr.color.g, sr.color.b, .5f);

        canBeKnocked = false;
        sr.color = darkenColor;
        yield return new WaitForSeconds(.1f);

        sr.color = originalColor;
        yield return new WaitForSeconds(.1f);

        sr.color = darkenColor;
        yield return new WaitForSeconds(.15f);

        sr.color = originalColor;
        yield return new WaitForSeconds(.15f);

        sr.color = darkenColor;
        yield return new WaitForSeconds(.25f);

        sr.color = originalColor;
        yield return new WaitForSeconds(.25f);

        sr.color = darkenColor;
        yield return new WaitForSeconds(.3f);

        sr.color = originalColor;
        yield return new WaitForSeconds(.35f);

        sr.color = darkenColor;
        yield return new WaitForSeconds(.4f);

        sr.color = originalColor;
        canBeKnocked = true;
    }

    private void Knockback()
    {
        if (!canBeKnocked)
            return;

        SpeedReset();
        StartCoroutine(Invincibility());
        isKnocked = true;
        rb.velocity = knockbackDir;
    }

    public void CancelKnockBack() => isKnocked = false;
    #endregion

    #region SpeedControl
    private void SpeedReset()
    {
        if (isSilding)
            return;

        moveSpeed = defaultSpeed;
        milestoneIncreaser = defaultMilestoneIncrease;
    }

    private void SpeedController()
    {
        if(moveSpeed == maxSpeed)
            return;

        if(transform.position.x > speedMilestone)
        {
            speedMilestone += milestoneIncreaser;
            moveSpeed *= speedMutiplier;
            milestoneIncreaser *= speedMutiplier;

            if(moveSpeed > maxSpeed)
                moveSpeed = maxSpeed;
        }
    }
    #endregion

    #region LedgeClimb
    private bool isLegdeDetected()
    {
        if(ledgeDetected.IsCanDetected && !isSilding)
        {
            return Physics2D.OverlapCircle(ledgeDetected.transform.position, radius, groundLayerMask);
        }
        return false;
    }

    private void CheckForLedge()
    {
        if(isLegdeDetected() && canGrapLedge)
        {
            SpeedReset();
            canGrapLedge = false;
            rb.gravityScale = 0;
            Vector2 ledgePosition = ledgeDetected.transform.position;

            climbBegunPosition = ledgePosition + offsetBeforeClimp;
            climbOverPosition = ledgePosition + offsetAfterClimp;

            canClimb = true;
        }

        if(canClimb)
        {
            transform.position = climbBegunPosition;
            
        }
    }

    public void LedgeClimbOver()
    {
        canClimb = false;
        rb.gravityScale = 5;
        transform.position = climbOverPosition;
        Invoke(nameof(AllowLedgeGrap),0.1f);

    }

    private void AllowLedgeGrap() 
    {
        canGrapLedge = true;
    }


        
    #endregion

    #region Slide
    private bool isCellDetected()
    {
        return Physics2D.Raycast(transform.position, Vector2.up, cellCheckDistant, groundLayerMask);
    }

    private void CheckForSilde()
    {
        if(slideTimeCounter < 0 && !isCellDetected())
        {
            isSilding = false;
        }
    }

    public void SildeButton()
    {
        if (silderCoolDownCounter < 0)
        {
            isSilding = true;
            slideTimeCounter = sildeTime;
            silderCoolDownCounter = sildeCooldown;
        }
    }

    #endregion

    #region Jumping_And_Moving
    private bool isGrounded()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistant, groundLayerMask);
    }

    private bool isWallDetected()
    {
        RaycastHit2D hit = Physics2D.BoxCast(wallCheck.position, wallCheckSize, 0, Vector2.zero, groundLayerMask);

        if (hit.collider != null)
        {
            Debug.Log(hit.collider.gameObject.name);
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                return true;
            }
        }
        return false;
    }
    public void Jump()
    {
        if (isSilding || isDead)
            return;
            
        RollAnimFinish();

        if(isGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        else if(isCanDouleJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, doubleJumpForce);
            isCanDouleJump = false;
        }
        
    }
    private void Moving()
    {
        if(isSilding)
            rb.velocity = new Vector2(sildeSpeed, rb.velocity.y);

        if(!isWallDetected())
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        else
            SpeedReset();
        
    }

    #endregion

    #region Animation
    private void AnimatorController()
    {
        anim.SetBool("isCanClimb", canClimb);
        anim.SetBool("isCanDoubleJump", isCanDouleJump);
        anim.SetBool("isGrounded", isGrounded());
        anim.SetFloat("yVelocity", rb.velocity.y);
        anim.SetFloat("xVelocity", rb.velocity.x);
        anim.SetBool("isSilding",isSilding);
        anim.SetBool("isKnocked", isKnocked);
        if(rb.velocity.y < -20)
            anim.SetBool("isCanRoll", true);
    }

    public void RollAnimFinish() => anim.SetBool("isCanRoll", false);

    public void StartRun()
    {
        capsuleCollider2D.offset = new Vector2(-0.0438609123f,-0.0325704813f);
        capsuleCollider2D.size = new Vector2(0.918493271f,2.58142209f);
    }

    public void StartSilde()
    {
        capsuleCollider2D.offset = new Vector2(-0.0438609123f,-0.673478127f);
        capsuleCollider2D.size = new Vector2(0.918493271f,1.2996068f);
    }
    #endregion

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y - groundCheckDistant));
        Gizmos.DrawWireCube(wallCheck.position, wallCheckSize);
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y + cellCheckDistant));
        Gizmos.DrawWireSphere(ledgeDetected.transform.position, radius);
        
    }

    private void CheckInput()
    { 

        // if(Input.GetMouseButtonDown(0))
        // {
        //     Jump();
        // }

        // if(Input.GetKeyDown(KeyCode.S))
        // {
        //     Debug.Log(10);
        //     SildeButton();
        // }
    }

    public void Coin()
    {
        coin++;
    }

    public void OnHit()
    {
        if(extraLife)
            Knockback();
        else
            StartCoroutine(OnDeath());
    }




}


