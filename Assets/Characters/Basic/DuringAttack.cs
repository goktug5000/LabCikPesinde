using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DuringAttack : StateMachineBehaviour
{

	MoveWASD moveCode;
	GameObject meGameObj;
	[Header("Have Combo Attack")]
	[SerializeField] private bool firstAttackB, secondAttackB;
	[Header("Attack Dmg multip")]
	[SerializeField] private float dmgMulti;
	private float inAnimTime;

	[SerializeField] private GameObject SFX;
	private GameObject _sfx;


	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		try
		{
			_sfx = Instantiate(SFX);
		}
		catch
		{

		}


		meGameObj = animator.gameObject.transform.parent.gameObject;
		moveCode = meGameObj.GetComponent<MoveWASD>();
		moveCode.forcedToStop = true;
		moveCode.forcedToNotRotate = true;
		meGameObj.GetComponent<AttackCode>().canAttack = true;

		animator.SetBool("FirstAttack", false);
		animator.SetBool("SecondAttack", false);


		if (rb == null)
		{

			try
			{
				rb = this.meGameObj.GetComponent<Rigidbody2D>();
			}
			catch
			{
				Debug.Log("Rigidbody2D koymamýþsýn");
			}
		}
		if (TrailRendererr == null)
		{
			try
			{
				TrailRendererr = this.meGameObj.GetComponent<TrailRenderer>();
			}
			catch
			{
				Debug.Log("TrailRenderer koymamýþsýn");
			}

		}
		
		TrailRendererr.emitting = true;
		Dash(-1);
		
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		inAnimTime += 1 * Time.deltaTime;
		if(inAnimTime>= (stateInfo.length / 2))
        {
			meGameObj.GetComponent<AttackCode>().checkDmgAble(dmgMulti);
		}



        if (firstAttackB)
        {
			if (Input.GetKeyDown(meGameObj.GetComponent<AttackCode>().KeyCode_Attack) || Input.GetKeyDown(meGameObj.GetComponent<AttackCode>().KeyCode_joy_Attack))
			{
				animator.SetBool("FirstAttack", true);
			}
		}
        if (secondAttackB)
        {
			if (Input.GetKeyDown(meGameObj.GetComponent<AttackCode>().KeyCode_SecondAttack) || Input.GetKeyDown(meGameObj.GetComponent<AttackCode>().KeyCode_joy_SecondAttack))
			{
				animator.SetBool("SecondAttack", true);
			}
		}
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		try
		{
			Destroy(_sfx);
		}
		catch
		{

		}

		TrailRendererr.emitting = false;
		moveCode.forcedToStop = false;
		moveCode.forcedToNotRotate = false;

		TrailRendererr.emitting = false;
		rb.velocity = Vector2.zero;
	}

	[Header("Dash Attack")]
	[SerializeField] private float dashingPower;
	private Rigidbody2D rb;
	private TrailRenderer TrailRendererr;


	void Dash(int eksiArti)
	{
		rb.velocity = new Vector2(-1 * eksiArti * meGameObj.transform.localScale.x * dashingPower, 0f);
		TrailRendererr.emitting = true;

	}
}
