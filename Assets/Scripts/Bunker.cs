using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bunker : MonoBehaviour
{
    public int maxHealth;
    public int health;
    
    public int damage = 1;
    public float fireRate = 0.25f;
    public float fireRange = 80;
    public float hitForce = 10;
    private float nextFire;
    private WaitForSeconds shotDuration = new WaitForSeconds(0.05f);
    public LayerMask mouseLayer;
    public LineRenderer shotLine;
    public Transform muzzle;

    public GameObject rocket;
    private float nextRocket;
    private float rocketCD = 3f;

    private AudioSource audioSource;
    public AudioClip gunSound;
    public AudioClip impactSound;

    private GameManager gameManager;


    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        shotLine = GetComponent<LineRenderer>();
        audioSource = GetComponent<AudioSource>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && Time.time > nextFire && !gameManager.isGamePaused)
        {
            FireWeapon();
            nextFire = Time.time + fireRate;
        }

        if (Input.GetKeyDown(KeyCode.Q) && Time.time > nextRocket && !gameManager.isGamePaused)
        {
            RocketLauncher();
            nextRocket = Time.time + rocketCD;
        }
    }

    public void Damage(int d)
    {
        health -= d;
    }

    public void Heal(int h)
    {
        health += h;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    public Vector3 MouseWorldPos()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, Mathf.Infinity, mouseLayer))
        {
            return raycastHit.point;
        }
        else
        {
            Debug.Log("Mouse of of bounds");
            return Vector3.zero;
        }
    }

    public void FireWeapon()
    {
        //Coroutine for shoteffects
        StartCoroutine(ShotEffect());

        //Vector for raycasting
        Vector3 shotVector = (MouseWorldPos() - muzzle.position).normalized;

        //Setting the first end of shotLine
        shotLine.SetPosition(0, muzzle.position);

        //Checking if the raycast hit something
        if (Physics.Raycast(muzzle.position, shotVector, out RaycastHit hit, fireRange))
        {
            shotLine.SetPosition(1, hit.point);

            audioSource.PlayOneShot(impactSound, 0.5f);

            //If the target hit has a Unit script then use the damage method
            Unit unit = hit.collider.GetComponent<Unit>();
            if (unit != null)
            {
                unit.Damage(damage);
            }
            
            //If the target hit has a Rigidbody then apply force
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(shotVector * hitForce, ForceMode.Impulse);
            }
        }
        else
        {
            shotLine.SetPosition(1, shotVector * fireRange);
        }
    }

    private IEnumerator ShotEffect()
    {
        audioSource.PlayOneShot(gunSound);
        shotLine.enabled = true;        
        yield return shotDuration;
        shotLine.enabled = false;
    } 
    
    public void RocketLauncher()
    {
        Vector3 direction = MouseWorldPos() - muzzle.position;
        Instantiate(rocket, muzzle.position, Quaternion.LookRotation(direction));
    }
}
