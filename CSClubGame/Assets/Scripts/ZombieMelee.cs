using UnityEngine;
using System.Collections;

public class ZombieMelee : MonoBehaviour
{
    private int attackDamage;
    private float attackSpeed;
    private float nextAttackTime;

    // Use this for initialization
    void Start()
    {
        nextAttackTime = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setParm(int atkDmg, float atkSpeed)
    {
        this.attackDamage = atkDmg;
        this.attackSpeed = atkSpeed;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (Time.time < nextAttackTime)
            return;
        if (!other.tag.Contains("Player"))
            return;

        nextAttackTime = Time.time + attackSpeed;
        other.GetComponent<Player>().TakeDamage(attackDamage, this.gameObject);
    }
}
