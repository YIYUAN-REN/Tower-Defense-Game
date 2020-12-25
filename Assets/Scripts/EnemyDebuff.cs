using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public abstract class EnemyDebuff
{
    protected EnemyController target;
    private float duration;
    private float elaspsed;

    public EnemyDebuff(EnemyController target, float duration)
    {
        this.target = target;
        this.duration = duration;
    }

    public virtual void Update()
    {
        elaspsed += Time.deltaTime;

        if (elaspsed >= duration)
        {
            Remove();
        }
    }

    public virtual void Remove()
    {
        if (target != null)
        {
            target.RemoveEnemyDebuff(this);
        }
    }
}
