using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class anim_EnemyTakeDMG : StateMachineBehaviour
{
	GameObject meGameObj;
	private Rigidbody2D rb;
	[SerializeField] private GameObject SFX;
	private GameObject _sfx;

	// Start is called before the first frame update
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		try
		{
			_sfx= Instantiate(SFX);
		}
		catch
		{
			
		}
		meGameObj = animator.gameObject;

		if (rb == null)
		{

			try
			{
				rb = meGameObj.GetComponent<Rigidbody2D>();
			}
			catch
			{
				Debug.Log("Rigidbody2D koymamýþsýn");
			}
		}

		Debug.LogWarning(meGameObj.GetComponent<EnemyLookAtPlayer>().benNerendeyim());

		Dash(-1 * meGameObj.GetComponent<EnemyLookAtPlayer>().benNerendeyim(), meGameObj.GetComponent<EnemyHP>().getLastDMG());

	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{

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
		rb.velocity = Vector2.zero;
	}




	void Dash(int eksiArti,float geriSekme)
	{
		
		rb.velocity = new Vector2(eksiArti * meGameObj.transform.localScale.x * geriSekme, 0f);

	}
}
