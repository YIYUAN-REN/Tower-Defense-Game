using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeEnemyDebuff: EnemyDebuff
{
    private float slowingFactor;
    private bool applied;
    


    public FreezeEnemyDebuff(float slowingFactor, float duration, EnemyController target) : base(target, duration)
    {
        this.slowingFactor = slowingFactor;
    }

    public override void Update()
    {
        if (target != null)
        {
            if (!applied)
            {
                applied = true;
                //target.Speed -= (target.MaxSpeed * slowingFactor) / 100;
                target.shouldMove = false;
            }
        }

        base.Update();
    }

    public override void Remove()
    {
        target.shouldMove = true;

        base.Remove();
    }
}
