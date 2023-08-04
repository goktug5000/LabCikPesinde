using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AttackCode : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private float dmg;
    public bool canAttack = true;


    [Header("General")]
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject attackHitBox;


    [Header("Keyboard")]
    [SerializeField] public KeyCode KeyCode_Attack;
    [SerializeField] public KeyCode KeyCode_SecondAttack;

    [Header("Gamepad")]
    [SerializeField] public KeyCode KeyCode_joy_Attack;
    [SerializeField] public KeyCode KeyCode_joy_SecondAttack;
    // Start is called before the first frame update
    void Start()
    {
        if (anim == null)
        {
            anim = gameObject.GetComponent<Animator>();
        }
        if (KeyCode_Attack == KeyCode.None)
        {
            KeyCode_Attack = KeyCode.LeftArrow;
        }
        if (KeyCode_SecondAttack == KeyCode.None)
        {
            KeyCode_SecondAttack = KeyCode.UpArrow;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode_Attack) || Input.GetKeyDown(KeyCode_joy_Attack))
        {
            Attack();
        }
        if (Input.GetKeyDown(KeyCode_SecondAttack) || Input.GetKeyDown(KeyCode_joy_SecondAttack))
        {
            SecondAttack();
        }
    }

    Vector3 attackOffset;
    float attackRange = 1f;
    public LayerMask attackMask;//EÞEK GÝBÝ ATAMAYI UNUTMA KAFAYI YERSÝN SONRA

    void Attack()
    {
        if (!gameObject.GetComponent<MoveWASD>().isGrounded)
            return;
        Debug.Log("Attack()");

        anim.SetTrigger("Attack");

        //checkDmgAble();

    }
    void SecondAttack()
    {
        if (!gameObject.GetComponent<MoveWASD>().isGrounded)
            return;
        Debug.Log("SecondAttack()");
        anim.SetTrigger("SecondAttack");

        //checkDmgAble();
    }

    public void checkDmgAble()
    {
        checkDmgAble(1);
    }
    public void checkDmgAble(float dmgMulti)
    {
        if (canAttack == false)
            return;

        Vector3 pos = attackHitBox.transform.position;
        pos += attackHitBox.transform.right * attackOffset.x;
        pos += attackHitBox.transform.up * attackOffset.y;

        Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);

        if (colInfo != null)
        {
            //Debug.Log(colInfo.gameObject.name);
            if (colInfo.GetComponent<EnemyHP>() != null)
            {
                colInfo.GetComponent<EnemyHP>().TakeDamage(dmg * dmgMulti);
                canAttack = false;
                return;
            }
            if (colInfo.GetComponent<ObjHP>() != null)
            {

                colInfo.GetComponent<ObjHP>().takeDMG(dmg * dmgMulti);
                canAttack = false;
                return;
            }



        }
    }

}
