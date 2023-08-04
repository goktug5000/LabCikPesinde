using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TurtleEnemyShield : StateMachineBehaviour
{
	[SerializeField] private float speed = 2.5f;
	[SerializeField] private float attackRange = 3f;
	[SerializeField] private float followRange = 10f;
	[SerializeField] private float defenceRange = 7f;

	[SerializeField] private float PasaGonlumCD;
	[SerializeField] [Range(0,10)] [Tooltip("1/x þeklinde az ise çok vurur yani")] private int vurmaSikligi;
	private float PasaGonlum;
	private bool PasaGonlumVurur=false;

	Transform player;
	Rigidbody2D rb;

	EnemyLookAtPlayer EnemyLookAtPlayerr;

	public LayerMask cantPassThrough;
	public float cantPassRange = 2;


	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		player = GameObject.FindGameObjectWithTag("Player").transform;
		rb = animator.GetComponent<Rigidbody2D>();

		EnemyLookAtPlayerr = animator.GetComponent<EnemyLookAtPlayer>();

		animator.gameObject.GetComponent<EnemyHP>().isInvulnerable = true;

		PasaGonlumVurur = false;
		PasaGonlum = PasaGonlumCD;
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		EnemyLookAtPlayerr.LookAtPlayer();


		if ((Vector2.Distance(player.position, rb.position) <= attackRange) && PasaGonlumVurur)
		{
			
			animator.SetTrigger("Attack");
			
		}
		else if (Vector2.Distance(player.position, rb.position) <= defenceRange)
		{
			animator.SetTrigger("Defence");
		}

		if (Vector2.Distance(player.position, rb.position) <= followRange)
		{
			Vector2 target = new Vector2(player.position.x, rb.position.y);
			Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * checkSpeedPass(animator.gameObject) * Time.fixedDeltaTime);
			rb.MovePosition(newPos);
		}


		PasaGonlum -= Time.deltaTime;
        if (PasaGonlum <= 0)
        {
			PasaGonlum = PasaGonlumCD;
			int sans = Random.Range(0, vurmaSikligi);
			//Debug.Log(sans);
            if (sans == 1)
            {
				PasaGonlumVurur = true;

            }
            else
            {
				PasaGonlumVurur = false;
			}
		}
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		animator.ResetTrigger("Defence");
		animator.gameObject.GetComponent<EnemyHP>().isInvulnerable = false;
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
