using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    public EnemyController enemy;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<EnemyController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(enemy.life <= 0)
        {
            animator.Play("Die");            
        }
        else
        {
            animator.SetBool("Walk", true);
        }
    }
}
