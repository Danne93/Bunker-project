using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jeep : Unit
{
    private const int numberOfUnits = 4;
    public GameObject dropOffUnit;
    bool hasDropped = false;
    Vector3 dropOffOffset = new Vector3(1,0,0);
    
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        rb = GetComponent<Rigidbody>();
        target = GameObject.Find("Bunker");
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (rb.velocity.magnitude == 0f && !hasDropped)
        {
            hasDropped = true;
            StartCoroutine(DropUnits(numberOfUnits));
        }
    }

    public override void Move()
    {
        //Move and then break when close
        float distanceToPlayer = (target.transform.position - transform.position).magnitude;
        if (distanceToPlayer > 10)
        {
            base.Move();
        }
    }
    
    private IEnumerator DropUnits(int n)
    {
        for (int i = 0; i < n; i++)
        { 
            Instantiate(dropOffUnit, (transform.position + dropOffOffset), transform.rotation);
            dropOffOffset *= -1;
            yield return new WaitForSeconds(0.4f);
        }
    }
}
