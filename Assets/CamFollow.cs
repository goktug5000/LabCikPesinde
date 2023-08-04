using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{

    [Tooltip("BU KODU CinemachineVirtualCamera kurup Confiner atadýðýn kamaranýn bulunduðu Room'a at\nmyCam ise o odanýn içindeki kamera")][SerializeField] private GameObject myCam;
    
    private void Awake()
    {

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            myCam.SetActive(true);

        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            myCam.SetActive(false);

        }
    }
}
