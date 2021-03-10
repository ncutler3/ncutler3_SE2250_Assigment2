using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class Enemy_2 : MonoBehaviour
{
    [Header("Set in Inspector: Enemy")]
    public float speed = 10f; //speed in m/s
    //public float fireRate = 0.3f; //unused?
    //public float health = 10; 
    public int score = 100; //points for killing

    private int ndx;
    public BoundsCheck bndCheck;

    //property: a method that acts like a field
    public Vector3 pos
    {
        get
        {
            return (this.transform.position);
        }
        set
        {
            this.transform.position = value;
        }
    }

    void Awake()
    {
        bndCheck = GetComponent<BoundsCheck>();
        ndx = Random.Range(1, 3);
        //print(ndx);
    }

    public void Move()
    {
        Vector3 tempPos = pos;
        tempPos.y -= speed * Time.deltaTime;
        if (ndx == 1)
        {
            tempPos.x -= speed * Time.deltaTime;
        }
        if (ndx == 2)
        {
            tempPos.x += speed * Time.deltaTime;
        }
        pos = tempPos;
    }

    void Update()
    {
        Move();

        if (bndCheck != null && bndCheck.offDown)
        {
            if (pos.y < bndCheck.camHeight - bndCheck.radius)
            {
                Destroy(this.gameObject);
            }
        }
        else if (bndCheck != null && bndCheck.offLeft)
        {
            if (pos.x < bndCheck.camWidth /*- bndCheck.radius*/)
            {
                Destroy(this.gameObject);
                //print("die");
            }
        }
        else if (bndCheck != null && bndCheck.offRight)
        {
            if (pos.x > bndCheck.camWidth /*- bndCheck.radius*/)
            {
                Destroy(this.gameObject);
                //print("die");
            }
        }
    }
}
