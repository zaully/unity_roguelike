﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MovingObject {

    public int playerDamange;

    private Animator animator;
    private Transform target;
    private bool skipMove;
    
	protected override void Start () {
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        GameManager.instance.RegisterEnemy(this);
        base.Start();
	}

    protected override void AttemptMove<T>(int xDir, int yDir) {
        if (skipMove) {
            skipMove = false;
            return;
        }
        base.AttemptMove<T>(xDir, yDir);
        skipMove = true;
    }

    protected override void OnCantMove<T>(T component) {
        Player hit = component as Player;
        animator.SetTrigger("enemyAttack");
        hit.LoseFood(playerDamange);
    }

    public void MoveEnemy() {
        int xDir = 0;
        int yDir = 0;

        if (Mathf.Abs(target.position.x - transform.position.x) < float.Epsilon) {
            yDir = target.position.y > transform.position.y ? 1 : -1;
        } else {
            xDir = target.position.x > transform.position.x ? 1 : -1;
        }

        AttemptMove<Player>(xDir, yDir);
    }
}
