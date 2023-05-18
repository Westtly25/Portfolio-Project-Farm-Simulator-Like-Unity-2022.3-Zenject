using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.InputSystem;
using Assets.Scripts.Input_System;

[RequireComponent(typeof(Rigidbody2D),
                  typeof(GenerateGUID),
                  typeof(SortingGroup))]
public class Hero : MonoBehaviour
{
    [Header("Cached Components")]
    [SerializeField] private Rigidbody2D rgb2D;
    [SerializeField] private GenerateGUID generateGUID;

    [Header("Classes")]
    [SerializeField] private Movement movement;
    [SerializeField] private DefaultInputActions inputActions;

    private InputService inputService;

    public void Construct(InputService inputService)
    {
        this.inputService = inputService;
    }

    public void Initialize()
    {
        movement = new Movement(2.5f);
        inputActions = new DefaultInputActions();
    }

    public void SetComponentsData()
    {
        rgb2D = GetComponent<Rigidbody2D>();
        generateGUID = GetComponent<GenerateGUID>();
    }

    private void FixedUpdate()
    {
        if (!inputActions.Player.Move.IsPressed()) { return; }

        Vector2 direction = inputActions.Player.Move.ReadValue<Vector2>();
        rgb2D.MovePosition(rgb2D.position + movement.GetDirection(direction));
    }
}