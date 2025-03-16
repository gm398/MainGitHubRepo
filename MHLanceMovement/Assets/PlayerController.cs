using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public MovementState PlayerMoveState;
}

public enum MovementState
{
    IDLE,
    RUNNING,
    JUMPING,
    FALLING,
    ROLLING,
    FORCEDANIMATION,
    ANIMATION,
    STUNNED,
    KNOCKEDBACK,
}
