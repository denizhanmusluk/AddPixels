using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Globals 
{
    public static int currentLevel = 1;
    public static int currentLevelIndex = 0, LevelCount;
    public static int moneyAmount = 0;

    public static bool buildActive = false;

    public static float currrentAnimSpeed = 0; // Upgradeable


    public static int coinPerBrick = 1; // Upgradeable
    public static int brickPerHit = 1; // Upgradeable
    public static float coolDownSpeed = 10; // Upgradeable
    public static float healthDownSpeed = 10; // Upgradeable
    public static float clickAnimSpeed = 2; // Upgradeable


    public static int brickLevel = 0;
    public static int staminaLevel = 0;
    public static int clickAnimLevel = 0;
}
