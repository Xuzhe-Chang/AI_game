using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    [SerializeField] protected int health;

    protected bool isDead;

    protected void CheackIsDead()
    {

        if (health <= 0 && !isDead)
        { Dead(); }
    }
    public virtual void Hurt(int damage)
    {

        if (!isDead)
        {
            health -= damage;
            CheackIsDead(); // ����ؼ�
        }
    }
    public virtual void Hurt(int damage,Transform attackPosition)
    {

        if (!isDead)
        {
            health -= damage;
            Debug.Log($"[Breakable] {gameObject.name} ����������ǰѪ��Ϊ��{health}");
            CheackIsDead();  // ����ؼ�
        }
    }
    protected virtual void Dead()
    {
        isDead = true;
        Debug.Log($"[Breakable] {gameObject.name} ������");
    }
}
