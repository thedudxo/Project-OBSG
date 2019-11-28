using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axis {
    public const string HORIZONTAL = "Horizontal";
    public const string VERTICAL = "Vertical";
    public const string JUMP = "Jump";
}

public class MouseAxis {
    public const string MOUSE_X = "Mouse X";
    public const string MOUSE_Y = "Mouse Y";
}

public class Tags {
    public const string WEAPON = "Weapon";
    public const string UP_STAIR = "UpStair";
    public const string DOWN_STAIR = "DownStair";
    public const string PLAYER = "Player";
    public const string ENEMY = "Enemy";
    public const string FINISH = "Finish";
    public const string RESPAWN = "Respawn";
    public const string MAIN_CAMERA = "MainCamera";
    public const string BOSS = "Boss";
}

public class Layers {
    public const string FPS = "FPS";
    public const string DEFAULT = "Default";
    public const string ENVIRONMENT = "Environment";
}

public class PlayerAnimation {
    //Original Animator
    public const string WALKING = "Walking";
    public const string RIGHT_PUNCH = "RightAttack";
    public const string LEFT_PUNCH = "LeftAttack";
    public const string LEFT_THROW = "LeftThrow";
    public const string RIGHT_THROW = "RightThrow";
    public const string END_RIGHT = "EndRight";
    public const string END_LEFT = "EndLeft";
    //New Animator
    public const string WALK_BLEND = "WalkBlend";
    public const string UNEQUIP = "Unequip";
    public const string ATTACK = "Attack";
    public const string STOP_ATTACK = "StopAttack";

}

public class EnemyAnimation {
    public const string ENEMY_ATTACK = "Attack";
    public const string IDLE_BLEND = "IdleBlend";
}

public class WeaponParticles {
    public const string ALPHA = "Alpha";
    public const string PICK_UP = "OnPickUp";
    public const string RESET = "OnReset";
}