using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeoStone : Breakable
{
    [SerializeField] GameObject coin;
    [SerializeField] int minSpawnCoins;
    [SerializeField] int maxSpawnCoins;
    [SerializeField] float maxBumpYForce;
    [SerializeField]float minBumpYForce;
    [SerializeField]float maxBumpXForce;

    private Animator animator;
    private AudioSource audioSource;

    private void Awake()
    {

        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        CheackIsDead();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Attack"))
        {
            Hurt(1, FindObjectOfType<Attack>().transform);
        }

    }


    public override void Hurt(int damage, Transform attackPosition)
    {
        base.Hurt(damage, attackPosition);
        Vector2 vector = attackPosition.position - transform.position;
        if (vector.x > 0)
        {
            //向左特效

        }
        else
        {
            //向右特效
        }
        SpawnCoins();
        animator.SetTrigger("Hurt");
    }

    protected override void Dead()
    {
        base.Dead();
        //特效
        animator.SetTrigger("Dead");
    }
    private void SpawnCoins()
    {
        int randomCount = Random.Range(minSpawnCoins, maxSpawnCoins);
        Debug.Log("生成金币数量: " + randomCount);
        for (int i = 0; i < randomCount; i++)
        {
            Vector3 spawnPos = transform.position;
            Debug.Log("金币生成位置: " + spawnPos);

            //GameObject geo = Instantiate(coin, transform.position, Quaternion.identity, transform) as GameObject;
            //GameObject geo = Instantiate(coin, transform.position, Quaternion.identity);
            GameObject geo = Instantiate(coin, transform.position + Vector3.up * 0.5f, Quaternion.identity);

            Debug.Log("生成金币：" + geo.name + " 位置：" + geo.transform.position);

            

            Vector2 force = new Vector2(Random.Range(-maxBumpXForce, maxBumpXForce), Random.Range(minBumpYForce, maxBumpYForce));
            Debug.Log("金币力: " + force);

            geo.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
        }
    }

}

