using System;
using UnityEngine;

[Serializable]
public class Movement : IMovement
{
    [Header("Hero Data")]
    [SerializeField] private readonly float movementSpeed = 2;
    [SerializeField] private readonly float walkingSpeed;
    [SerializeField] private readonly float runningSpeed;

    public Movement() { }

    public Movement(float movementSpeed)
    {
        this.movementSpeed = movementSpeed;
    }

    public Vector2 GetDirection(Vector2 direction)
    {
        return new Vector2(direction.x, direction.y) * movementSpeed * Time.deltaTime;
    }
}