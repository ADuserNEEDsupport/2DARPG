using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerState
{
    public PlayerWallSlideState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.ChangeState(player.wallJumpState);
            return;
        }

        if(xInput != 0 && player.facingDir != xInput)//如果玩家按了按键，并且方向不等于当前，则让玩家从墙上掉下来
        {
            stateMachine.ChangeState(player.idleState);
        }

        if(yInput != -1)
        {
            rb.velocity = new Vector2(0, rb.velocity.y * .7f);
        }

        if (player.IsGroundDetected())//如果抵达地面，则让玩家从墙上掉下来
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
