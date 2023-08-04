using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float dmg;
    [SerializeField] private float lifeTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(speed * Time.deltaTime, 0, 0);
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<PlayerHP>().TakeDamage(dmg);
        }
        if(col.gameObject.tag != "Room")
        {
            Debug.Log("çarptým: " + col.gameObject.name.ToString());
            Destroy(this.gameObject);
        }

    }
}
