using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AreaEnemy : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject SFX;
    private GameObject _sfx;
    [SerializeField] private Transform bulletBarell;
    [SerializeField] private int bulletCD;
    private float CD;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CD -= 1 * Time.deltaTime;
        if (CD <= 0)
        {
            CD = bulletCD;
            Instantiate(bullet, bulletBarell);
            StartCoroutine(playSFX());
        }
    }
    IEnumerator playSFX()
    {
        try
        {
            _sfx = Instantiate(SFX, bulletBarell);
        }
        catch
        {
            yield break;
        }

        yield break;
    }
}
