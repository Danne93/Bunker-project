using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] Vector3 target;
    [SerializeField] private AnimationCurve curve;
    public GameObject explosion; 
    Vector3 startPos;
    public Bunker bunker;
    public float avgSpeed = 5f;
    float t;
    float elapsedTime;
    float travelTime;


    // Start is called before the first frame update
    void Start()
    {
        bunker = GameObject.Find("Bunker").GetComponent<Bunker>();
        startPos = transform.position;
        SetTarget(bunker.MouseWorldPos());
        SetTravelTime();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (transform.position == target)
        {
            Explode();
        }
    }

    public void Move()
    {
        elapsedTime += Time.deltaTime;
        t = elapsedTime / travelTime;
        transform.position = Vector3.Lerp(startPos, target, curve.Evaluate(t));
        
    }

    public void SetTravelTime()
    {
        travelTime = (target - startPos).magnitude / avgSpeed;
    }

    public void SetTarget(Vector3 v)
    {
        target = v;
    }

    private void Explode()
    {
            explosion.SetActive(true);
            explosion.transform.parent = null;
            Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            Explode();
        }
    }
}
