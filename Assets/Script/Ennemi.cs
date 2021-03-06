﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemi : MovingObject
{
    public int playerDamage;

    private Animator animator;
    private Transform target;
    private bool skipMove;

    protected override void OnCantMove<T>(T component)
    {
        Player  hitPlayer = component as Player;
        animator.SetTrigger("ennemiAttack");
        hitPlayer.loseFood(playerDamage);
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        ControlleurJeu.instance.addEnnemiesToList(this);
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    protected override void AttemptMove<T>(int xDir, int yDir)
    {
        base.AttemptMove<T>(xDir, yDir);

    }
    public void MoveEnnemy()
    {
        int xDir = 0;
        int yDir = 0;
        if (Mathf.Abs(target.position.x - transform.position.x) < float.Epsilon)
        {
            yDir = (target.position.y > transform.position.y) ? 1 : -1;

        }
        else
        {
            xDir = target.position.x > transform.position.x ? 1 : -1;
        }
        AttemptMove<Player>(xDir, yDir);
    }
}
