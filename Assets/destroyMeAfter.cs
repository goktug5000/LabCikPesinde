using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyMeAfter : MonoBehaviour
{
    [SerializeField] private float myLifetime;

    void Update()
    {
        myLifetime -= 1*Time.deltaTime;
        if (myLifetime < 0)
        {
            Destroy(this.gameObject);
        }
    }
}
