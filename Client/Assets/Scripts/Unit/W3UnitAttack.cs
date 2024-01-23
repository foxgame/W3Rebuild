using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class W3Unit
{
    bool bAttack = false;
    bool bAttackOver = true;
    bool bAttackCooling = false;

    float attackTime = 0.0f;


    public void startAttack()
    {
        bAttack = true;
    }

    public void stopAttack()
    {
        if ( bAttack )
        {
            bAttack = false;
            bAttackOver = true;
            playAnimation( defaultAnimationType );
        }
    }

    public void updateAttack( float delay )
    {
        if ( bAttackCooling )
        {
            attackTime -= delay;

            if ( attackTime <= 0 )
            {
                bAttackCooling = false;
            }
        }

        if ( !bAttack )
        {
            return;
        }

        if ( !bAttackOver &&
            attackTime < unitWeapons.cool1 - 0.5f )
        {
            onAttackOver();
        }

        if ( !bAttackCooling )
        {
            // attack
            attack();
            attackTime = unitWeapons.cool1;
            bAttackCooling = true;
        }
    }

    public void onAttackOver()
    {
        bAttackOver = true;

        for ( int i = 0 ; i < targets.Count ; i++ )
        {
            W3Unit u = targets[ i ];

            if ( u.baseData.hp > 0 )
            {
                u.addHP( -unitWeapons.mindmg1 );
            }
            else
            {
                stopAttack();
            }
        }
    }

    void attack()
    {
        for ( int i = 0 ; i < targets.Count ; i++ )
        {
            W3Unit u = targets[ i ];

            if ( u.baseData.hp > 0 )
            {
                playAnimation( W3AnimationType.Attack1 , true );
                bAttackOver = false;

                Vector3 v3 = u.getPosition();

                RotateTo( (int)v3.x , (int)v3.z );
            }
        }

    }

}

