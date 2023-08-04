using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class OneWayToTwoWayPlatform : MonoBehaviour
{
    float timerMax=0.5f;
    float timerCurrent;

    [Header("Keyboard")]
    [SerializeField] private KeyCode KeyCode_Down;

    [Header("Gamepad")]
    [SerializeField] private KeyCode KeyCode_joy_Down;

    void Start()
    {
        if (KeyCode_Down == KeyCode.None)
        {
            try
            {
                KeyCode_Down = GameObject.FindGameObjectWithTag("Player").GetComponent<MoveWASD>().KeyCode_Down;
            }
            catch
            {
                KeyCode_Down = KeyCode.S;
            }
            
        }

    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKey(KeyCode_Down) || Input.GetKey(KeyCode_joy_Down))
        {
            this.gameObject.GetComponent<PlatformEffector2D>().surfaceArc = 0;
            timerCurrent = timerMax;
        }
        else
        {
            timerCurrent -= Time.deltaTime;

        }
        if (timerCurrent < 0)
        {
            timerCurrent = -1;
            this.gameObject.GetComponent<PlatformEffector2D>().surfaceArc = 100;
        }

    }
}
