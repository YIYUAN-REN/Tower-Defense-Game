using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class FireEnemyDebuff : EnemyDebuff
{
    private float tickTime;
    private float timeSinceTick;
    private float tickDamage;

    public FireEnemyDebuff(float tickDamage, float tickTime, float duration, EnemyController target) : base(target, duration)
    {
        this.tickDamage = tickDamage;
        this.tickTime = tickTime;
    }

    public override void Update()
    {
        if (target != null)
        {
            timeSinceTick += Time.deltaTime;

            if (timeSinceTick >= tickTime)
            {
                timeSinceTick = 0;
                target.TakeDamage(tickDamage);
            }
        }

        base.Update();
    }
}
