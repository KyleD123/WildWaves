using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bulletPref;

    public float bulletSpeed;
    private float lastFire;
    public float fireDelay;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalShot = Input.GetAxisRaw("Horizontal");
        float verticalShot = Input.GetAxisRaw("Vertical");

        if ((horizontalShot != 0 || verticalShot != 0) && Time.time > lastFire + fireDelay)
        {
            Shoot(horizontalShot, verticalShot);
            lastFire = Time.time;
        }

    }

    public void Shoot(float x, float y)
    {
        Debug.Log("Shot");
        GameObject bullet = Instantiate(bulletPref, transform.position, transform.rotation);

        if(Mathf.Abs(x) > Mathf.Abs(y))
        {
            y = 0;
        }
        else
        {
            x = 0;
        }

        bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(x,y).normalized * bulletSpeed;
    }

}

