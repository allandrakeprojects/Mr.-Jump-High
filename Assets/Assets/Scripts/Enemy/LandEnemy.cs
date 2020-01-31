using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandEnemy : Enemy, IDamageable
{
    public int Health { get; set; }

    public override void Init()
    {
        base.Init();
        Health = base.health;
    }

    public override void Movement()
    {
        base.Movement();
    }

    private bool detectStartUpdate = false;

    public void Damage()
    {
        if (isDead)
            return;

        int getFlameStatus = PlayerPrefs.GetInt("EnableFlame", 0);
        if (getFlameStatus == 1)
        {
            Health = 0;
        }


        Health--;
        anim.SetTrigger("Hit");
        isHit = true;
        anim.SetBool("InCombat", true);

        if (Health < 1)
        {
            anim.SetTrigger("Death");
            isDead = true;
            //gameObject.SetActive(false);

            //GameObject coin = Instantiate(coinPrefab, transform.position, Quaternion.identity);
            //coin.GetComponent<Coin>().coins = base.coins;
        }
    }
}
