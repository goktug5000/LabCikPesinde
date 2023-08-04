using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MoveWASD : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runMultiplier;
    public bool forcedToStop;
    private float currentSpeed;





    [Header("General")]
    [SerializeField] private Animator anim;
    private bool isFacingRight;
    public bool forcedToNotRotate;
    private bool isMoving;

    [Header("Dash")]
    private bool canDash=true;
    private bool isDashing;
    [SerializeField] private float dashCD;
    [SerializeField] private float dashingTime;
    [SerializeField] private float dashingPower;
    private Rigidbody2D rb;
    private TrailRenderer TrailRendererr;

    [Header("Jump")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float jumpingPower;
    [SerializeField] public bool isGrounded;
    [SerializeField] private bool isJumping;

    private float coyoteTime = 0.2f;
    [SerializeField] private float coyoteTimeCounter;

    private float jumpBufferTime = 0.2f;
    [SerializeField] private float jumpBufferCounter;

    [Header("Keyboard")]
    [SerializeField] private KeyCode KeyCode_Left;
    [SerializeField] private KeyCode KeyCode_Right;
    [SerializeField] private KeyCode KeyCode_Up;
    [SerializeField] public KeyCode KeyCode_Down;

    [SerializeField] private KeyCode KeyCode_Run;
    [SerializeField] private KeyCode KeyCode_Dash;

    [SerializeField] private KeyCode KeyCode_Jump;

    [SerializeField] private KeyCode KeyCode_Attack;
    [SerializeField] private KeyCode KeyCode_SecondAttack;

    [Header("Gamepad")]
    [SerializeField] private KeyCode KeyCode_joy_Left;
    [SerializeField] private KeyCode KeyCode_joy_Right;
    [SerializeField] private KeyCode KeyCode_joy_Up;
    [SerializeField] public KeyCode KeyCode_joy_Down;

    [SerializeField] private KeyCode KeyCode_joy_Run;
    [SerializeField] private KeyCode KeyCode_joy_Dash;

    [SerializeField] private KeyCode KeyCode_joy_Jump;

    [SerializeField] private KeyCode KeyCode_joy_Attack;
    [SerializeField] private KeyCode KeyCode_joy_SecondAttack;



    [Header("SFX")]
    [SerializeField] private GameObject SFX_Jump;
    private GameObject _sfx;



    private void Awake()
    {


        if (KeyCode_Left == KeyCode.None)
        {
            KeyCode_Left = KeyCode.A;
        }
        if (KeyCode_Right == KeyCode.None)
        {
            KeyCode_Right = KeyCode.D;
        }
        if (KeyCode_Down == KeyCode.None)
        {
            KeyCode_Down = KeyCode.S;
        }
        if (KeyCode_Run == KeyCode.None)
        {
            KeyCode_Run = KeyCode.LeftControl;
        }
        if (KeyCode_Dash == KeyCode.None)
        {
            KeyCode_Dash = KeyCode.LeftShift;
        }
        if (KeyCode_Jump == KeyCode.None)
        {
            KeyCode_Jump = KeyCode.W;
        }


        if (anim == null)
        {
            
            try
            {
                anim = gameObject.GetComponent<Animator>();
            }
            catch
            {
                Debug.Log("Animator koymamýþsýn");
            }
        }
        if (rb == null)
        {
            
            try
            {
                rb = this.gameObject.GetComponent<Rigidbody2D>();
            }
            catch
            {
                Debug.Log("Rigidbody2D koymamýþsýn");
            }
        }
        rb.gravityScale = rb.gravityScale * 2;
        if (TrailRendererr == null)
        {
            try
            {
                TrailRendererr = this.gameObject.GetComponent<TrailRenderer>();
            }
            catch
            {
                Debug.Log("TrailRenderer koymamýþsýn");
            }
            
        }
    }
    private void Update()
    {
        if (isDashing)
        {
            return;
        }




        if (forcedToStop)
        {
            currentSpeed = 0;
        }
        else
        {
            if (Input.GetKey(KeyCode_Run) || Input.GetKey(KeyCode_joy_Run))
            {
                currentSpeed = walkSpeed * runMultiplier;
            }
            else
            {
                currentSpeed = walkSpeed;
            }
        }

        if (Input.GetKey(KeyCode_Left) || Input.GetKey(KeyCode_joy_Left))
        {
            isMoving = true;
            transform.Translate(-1 * currentSpeed * Time.deltaTime, 0, 0);
            Flip(false);
        }
        if (Input.GetKey(KeyCode_Down) || Input.GetKey(KeyCode_joy_Down))
        {
            transform.Translate(0, - 2 * currentSpeed * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode_Right) || Input.GetKey(KeyCode_joy_Right))
        {
            isMoving = true;
            transform.Translate(currentSpeed * Time.deltaTime, 0, 0);
            Flip(true);
        }
        if (isMoving)
        {
            anim.SetBool("Run", true);
        }
        else
        {
            anim.SetBool("Run", false);
        }
        isMoving = false;

        if ((Input.GetKeyDown(KeyCode_Dash) || Input.GetKeyDown(KeyCode_joy_Dash))&&canDash)
        {
            if (Input.GetKey(KeyCode_Left) || Input.GetKey(KeyCode_joy_Left) || Input.GetKey(KeyCode_Right) || Input.GetKey(KeyCode_joy_Right))
            {
                StartCoroutine(Dash(-1));
            }
            else
            {
                StartCoroutine(Dash(1));
            }
            
        }

        //if ((Input.GetKey(KeyCode_Jump) || Input.GetKey(KeyCode_joy_Jump)))
        //{
        //    rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        //}
        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }
        if ((Input.GetKeyDown(KeyCode_Jump) || Input.GetKeyDown(KeyCode_joy_Jump)))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        if (coyoteTimeCounter > 0f && jumpBufferCounter > 0f && !isJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            StartCoroutine(playSFX(SFX_Jump));
            jumpBufferCounter = 0f;
            coyoteTimeCounter = 0;
            isGrounded = false;
            StartCoroutine(JumpCooldown());
        }
        if ((Input.GetKeyUp(KeyCode_Jump) && Input.GetKeyUp(KeyCode_joy_Jump)) && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);

            coyoteTimeCounter = 0f;
        }







        //foreach (keycode vkey in system.enum.getvalues(typeof(keycode)))
        //{
        //    if (ýnput.getkey(vkey))
        //    {
        //        debug.log("keycode down: " + vkey);

        //    }
        //}


    }

    private void Flip(bool FaceRight)
    {
        if (forcedToNotRotate)
            return;
        if (isFacingRight && FaceRight==true || !isFacingRight && FaceRight == false)
        {
            Vector3 localScale = transform.localScale;
            isFacingRight = !isFacingRight;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    public void dashPublic(int eksiArti, float anyCD)
    {
        StartCoroutine(Dash(eksiArti));
    }
    IEnumerator Dash(int eksiArti, float anyCD)
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0;
        rb.velocity = new Vector2(-1 * eksiArti * transform.localScale.x * dashingPower, 0f);
        TrailRendererr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        TrailRendererr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(dashCD);
        canDash = true;
    }
    IEnumerator Dash(int eksiArti)
    {
        StartCoroutine(Dash(eksiArti, dashCD));
        yield return null;
    }



    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag != "CamLocation" && col.gameObject.tag != "Icey")
        {
            isGrounded = true;
        }
        
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag != "CamLocation" && col.gameObject.tag != "Icey")
        {
            isGrounded = true;
        }

    }
    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag != "CamLocation" && col.gameObject.tag != "Icey")
        {
            isGrounded = false;
        }
    }
    private IEnumerator JumpCooldown()
    {
        isJumping = true;
        yield return new WaitForSeconds(0.4f);
        isJumping = false;
    }
    // Update is called once per frame
    IEnumerator playSFX(GameObject SFXX)
    {
        try
        {
            _sfx = Instantiate(SFXX);
        }
        catch
        {
            yield break;
        }
        yield return new WaitForSeconds(10);

        Destroy(_sfx);
        yield break;
    }
}
