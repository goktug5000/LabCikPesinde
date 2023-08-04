using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RoomCode : MonoBehaviour
{
    [SerializeField] private bool haveDies;
    [SerializeField] private GameObject[] mustDies;
    [SerializeField] private GameObject[] roomBlocks;
    private bool died=false;
    [SerializeField] private GameObject SFX;
    private GameObject _sfx;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (haveDies)
        {
            checkAllDied();
        }

    }
    public void checkAllDied()
    {
        foreach(GameObject mustDie in mustDies)
        {
            if(mustDie != null)
            {
                return;
            }
        }
        Debug.Log("checkAllDied bitti");
        openBLocks();
    }
    public void openBLocks()
    {
        Debug.Log("openBLocks");
        if (died)
        {
            return;
        }

        foreach(GameObject roomBlock in roomBlocks)
        {
            roomBlock.SetActive(false);
        }
        died = true;
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

        }
        yield return new WaitForSeconds(3);

        Destroy(this);
        yield break;
    }
}
