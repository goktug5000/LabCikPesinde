using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TurtleEnemyRun : StateMachineBehaviour
{

	[SerializeField] private float speed = 2.5f;
	[SerializeField] private float followRange = 10f;
	[SerializeField] private float defenceRange = 7f;

	Transform player;
	Rigidbody2D rb;

	EnemyLookAtPlayer EnemyLookAtPlayerr;

	public LayerMask cantPassThrough;
	public float cantPassRange = 1;

	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		player = GameObject.FindGameObjectWithTag("Player").transform;
		rb = animator.GetComponent<Rigidbody2D>();

		EnemyLookAtPlayerr = animator.GetComponent<EnemyLookAtPlayer>();

	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		EnemyLookAtPlayerr.LookAtPlayer();




		if (Vector2.Distance(player.position, rb.position) <= defenceRange)
		{
			animator.SetTrigger("Defence");
		}
		
		if (Vector2.Distance(player.position, rb.position) <= followRange)
		{
			Vector2 target = new Vector2(player.position.x, rb.position.y);
			Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * checkSpeedPass(animator.gameObject) * Time.fixedDeltaTime);
			rb.MovePosition(newPos);
		}
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		animator.ResetTrigger("Defence");
	}
	float checkSpeedPass(GameObject myGameObj)
	{

		Vector3 pos = myGameObj.transform.position;

		Collider2D[] hits = Physics2D.OverlapBoxAll(new Vector2(myGameObj.transform.position.x + (myGameObj.GetComponent<EnemyLookAtPlayer>().rotasyonArtiEksi() * 1), myGameObj.transform.position.y), new Vector2(cantPassRange, cantPassRange), 0f, cantPassThrough);
		foreach (Collider2D a in hits)
		{
			if (a.gameObject != myGameObj)
			{
				return 0.1f;
			}
		}
		return 1;

	}
}
