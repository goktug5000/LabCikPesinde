using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjHP : MonoBehaviour
{
    [SerializeField] private float myHP;
    [SerializeField] private GameObject SFX;
    private GameObject _sfx;
    public void takeDMG(float dmg)
    {
        Debug.Log("takeing DMG: " + dmg);
        myHP -= dmg;
        if (myHP <= 0)
        {
            Destroy(this.gameObject);
        }
        StartCoroutine(playSFX());
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
}
