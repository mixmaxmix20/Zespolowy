
// Klasa AnimationStrings przechowuje nazwy parametrów używanych w animatorze jako stałe stringi.
// Dzięki temu unikamy literówek i mamy jedno centralne miejsce do zarządzania nazwami parametrów animacji.
// Jest to szczególnie pomocne, gdy animacje są wyzwalane lub kontrolowane z poziomu skryptów.

internal class AnimationStrings
{
    internal static string isMoving = "isMoving";
    internal static string isGrounded = "isGrounded";
    internal static string yVelocity = "yVelocity";
    internal static string jumpTrigger = "jump";
    internal static string isOnCeiling = "isOnCeiling";
    internal static string isOnWall = "isOnWall";
    internal static string attackTrigger = "attack";
    internal static string canReceiveInput = "canReceiveInput"; 
    internal static string hasTarget = "hasTarget";
    internal static string isAlive = "isAlive";
    internal static string canMove = "canMove";
    internal static string hitTrigger = "hit";
    internal static string lockVelocity = "lockVelocity";
    internal static string attackCooldown = "attackCooldown";
    internal static string currentSpeed = "currentSpeed" ;
}

