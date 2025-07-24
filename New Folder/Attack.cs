using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public GameObject Slash;
    //public GameObject Slash;
    public GameObject UpSlash;
    public GameObject DownSlash;
    public GameObject AltSlash;


    public ContactFilter2D enemyContactFilter;

    public enum AttackType
    {
        Slash, AltSlash, DownSlash, UpSlash
    }
    public void Play(AttackType attackType,ref List<Collider2D> colliders)
    {
        switch (attackType)
        {
            case AttackType.Slash:
                Physics2D.OverlapCollider(Slash.GetComponent<Collider2D>(),enemyContactFilter,colliders);
                //音效
                Slash.GetComponent<AudioSource>().Play();
                break;

            case AttackType.AltSlash:
                Physics2D.OverlapCollider(AltSlash.GetComponent<Collider2D>(), enemyContactFilter, colliders);
                //音效
                AltSlash.GetComponent<AudioSource>().Play();
                break;

            case AttackType.DownSlash:
                Physics2D.OverlapCollider(DownSlash.GetComponent<Collider2D>(), enemyContactFilter, colliders);
                //音效
                DownSlash.GetComponent<AudioSource>().Play();
                break;

            case AttackType.UpSlash:
                Physics2D.OverlapCollider(UpSlash.GetComponent<Collider2D>(), enemyContactFilter, colliders);
                //音效
                UpSlash.GetComponent<AudioSource>().Play();
                break;
        }
    }
    

}
