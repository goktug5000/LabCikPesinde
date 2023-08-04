using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyAttack : MonoBehaviour
{

	[SerializeField] private int attackDamage = 20;
	[SerializeField] private int enragedAttackDamage = 40;

	private Vector3 attackOffset;
	[SerializeField] private float attackRange = 1f;
	[SerializeField] private LayerMask attackMask;
	[SerializeField] private GameObject attackHitBox;

	public void Attack()
	{


		Vector3 pos = attackHitBox.transform.position;
		pos += attackHitBox.transform.right * attackOffset.x;
		pos += attackHitBox.transform.up * attackOffset.y;

		Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
        if (colInfo != null)
        {
			if (colInfo.tag == "Player")
			{
				colInfo.GetComponent<PlayerHP>().TakeDamage(attackDamage);
			}
		}

	}

}
