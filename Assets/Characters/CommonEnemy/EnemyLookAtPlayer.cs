using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class EnemyLookAtPlayer : MonoBehaviour
{

	[SerializeField] private Transform player;
	[SerializeField][Tooltip("kodu roota atarsan buraya görselliyi koy")] private GameObject meToRotate;

	private bool isFlipped = false;

	public int rotasyonArtiEksi()
    {
		if (isFlipped)
			return -1;
		if (!isFlipped)
			return 1;

		return 0;
	}
	public int benNerendeyim()
    {
		findPlayerTrans();

		if (transform.position.x < player.position.x)
		{
			return 1;
		}
		else if (transform.position.x > player.position.x)
		{
			return -1;
		}
		return 0;
		
	}
    private void Awake()
    {

		findPlayerTrans();
        if (meToRotate == null)
        {
			meToRotate = this.gameObject;
        }

	}
	void findPlayerTrans()
    {
		if (player == null)
		{
			player = GameObject.FindGameObjectWithTag("Player").transform;
		}
	}
    public void LookAtPlayer()
	{
		findPlayerTrans();

		Vector3 flipped = meToRotate.transform.localScale;
		flipped.z *= -1f;

		if (transform.position.x < player.position.x && isFlipped)
		{
			meToRotate.transform.localScale = flipped;
			meToRotate.transform.Rotate(0f, 180f, 0f);
			isFlipped = false;
		}
		else if (transform.position.x > player.position.x && !isFlipped)
		{
			meToRotate.transform.localScale = flipped;
			meToRotate.transform.Rotate(0f, 180f, 0f);
			isFlipped = true;
		}
	}

    private void Update()
    {
		//LookAtPlayer();

	}
}
