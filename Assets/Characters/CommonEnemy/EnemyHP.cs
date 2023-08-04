using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyHP : MonoBehaviour
{
	[Header("HP")]
	[SerializeField] private float MaxTemp;
	[SerializeField] private float TempHP;
	[SerializeField] private float MaxHealth;
	[SerializeField] private float health;

	[SerializeField] private GameObject deathEffect;

	[SerializeField] private GameObject HPbar;
	[SerializeField] private GameObject TempHPbar;

	[Header("SFX")]
	[SerializeField] private GameObject SFX;
	private GameObject _sfx;
	private float lastDMG;
	public float getLastDMG()
    {
		return lastDMG;
    }

	public bool isInvulnerable = false;
    private void Awake()
    {
		TempHPbar.transform.localScale = new Vector3(0, 1, 1f);
		if (MaxTemp!=0)
        {
			TempHPbar.transform.localScale = new Vector3(1f, 1, 1f);
		}
		
		HPbar.transform.localScale = new Vector3(1f, 1, 1f);
		health = MaxHealth;
	}
    public void TakeDamage(float damage)
	{

		if (isInvulnerable)
        {
			lastDMG = 0;
			return;
		}


		lastDMG = damage;//geri sekme kýsmý için koydum


		gameObject.GetComponent<Animator>().SetTrigger("takeDMG");
		StartCoroutine(playSFX());
		if (TempHP > 0)
        {
			TempHP -= damage;
			TempHPbar.transform.localScale = new Vector3((TempHP / MaxTemp), 1, 1f);
            if (TempHP < 0)
            {
				damage = -TempHP;
				TempHP = 0;
			}
            else
            {
				damage = 0;
            }
        }
        else
        {
			TempHP = 0;

		}
		health -= damage;

		HPbar.transform.localScale = new Vector3((health/MaxHealth), 1, 1f);
        if (MaxTemp != 0)
        {
			TempHPbar.transform.localScale = new Vector3((TempHP / MaxTemp), 1, 1f);
        }
        else
		{
			TempHPbar.transform.localScale = new Vector3(0, 1, 1f);

		}


		if (health <= -100)
		{
			GetComponent<Animator>().SetBool("IsEnraged", true);
		}

		if (health <= 0)
		{
			Die();
		}
	}
	IEnumerator playSFX()
	{
		try
		{
			_sfx = Instantiate(SFX);
		}
		catch
		{
			yield break;
		}
		yield return new WaitForSeconds(10);

		try
		{
			Destroy(_sfx);
		}
		catch
		{

		}

		yield break;
	}

	void Die()
	{
		//Instantiate(deathEffect, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}
}
