using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class BatController : EnemyController
{
    //private Transform[] naviPoints;
    //private bool isLoop = false;
    //private Transform[] loopPoints;
    private Animator animator;
    public int IsWalking = -1;
    //public int IsRunning = -1;
    public int IsDead = -1;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        IsWalking = Animator.StringToHash("Walk");
        //IsRunning = Animator.StringToHash("Run");
        IsDead = Animator.StringToHash("Die");
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Rotate();
        if (life <= 0)
        {
            animator.SetBool(IsDead, true);
            Destroy(this.gameObject, 3);
        }
    }

    void Move()
    {
            //animator.SetBool(IsRunning, true);
        animator.SetBool("Walk", true);

        if (isLoop)
        {
            target = loopPoints[loopIndex].position;
        }
        else
        {
            target = naviPoints[index].position;
        }
        transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * speed);
        if (!isLoop && Vector3.Distance(target, transform.position) < 1f)
        {
            if (++index == 13)
            {
                isLoop = true;
            }
        }
        else if (isLoop && Vector3.Distance(target, transform.position) < 1f)
        {
            if (++loopIndex == 4)
            {
                loopIndex = 0;
            }
        }
    }
    void Rotate()
    {
        Vector3 target = this.target - transform.position;
        target.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(target);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
    }

    void OnDestroy()
    {

    }
}
