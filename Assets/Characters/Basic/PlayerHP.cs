using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerHP : MonoBehaviour
{
	[Header("Life")]
	[SerializeField] public static int Life = 3;
	[Header("HP")]
	[SerializeField] private float MaxHealth = 50;
	[SerializeField] private float health;

	[SerializeField] private GameObject deathEffect;

	[SerializeField] private GameObject HPbar;
	public bool isInvulnerable = false;
	private void Awake()
	{
		HPbar.transform.localScale = new Vector3(1f, 1, 1f);
		health = MaxHealth;
	}
	public void TakeDamage(float damage)
	{
		if (isInvulnerable)
			return;

		health -= damage;
		HPbar.transform.localScale = new Vector3((health / MaxHealth), 1, 1f);


		if (health <= 0)
		{
			Die();
		}
	}

	void Die()
	{
		Instantiate(deathEffect, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}

}
