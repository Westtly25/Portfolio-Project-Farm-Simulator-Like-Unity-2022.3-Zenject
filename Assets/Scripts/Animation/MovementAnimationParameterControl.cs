
using UnityEngine;

public class MovementAnimationParameterControl : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        EventHandler.MovementEvent += SetAnimationParameters;
    }

    private void OnDisable()
    {
        EventHandler.MovementEvent -= SetAnimationParameters;
    }

    private void SetAnimationParameters(float xInput, float yInput, bool isWalking, bool isRunning, bool isIdle, bool isCarrying, ToolEffect toolEffect,
        bool isUsingToolRight, bool isUsingToolLeft, bool isUsingToolUp, bool isUsingToolDown,
        bool isLiftingToolRight, bool isLiftingToolLeft, bool isLiftingToolUp, bool isLiftingToolDown,
        bool isPickingRight, bool isPickingLeft, bool isPickingUp, bool isPickingDown,
        bool isSwingingToolRight, bool isSwingingToolLeft, bool isSwingingToolUp, bool isSwingingToolDown,
        bool idleUp, bool idleDown, bool idleLeft, bool idleRight)
    {
        animator.SetFloat(StaticData.xInput, xInput);
        animator.SetFloat(StaticData.yInput, yInput);
        animator.SetBool(StaticData.isWalking, isWalking);
        animator.SetBool(StaticData.isRunning, isRunning);

        animator.SetInteger(StaticData.toolEffect, (int)toolEffect);

        if (isUsingToolRight)
            animator.SetTrigger(StaticData.isUsingToolRight);
        if (isUsingToolLeft)
            animator.SetTrigger(StaticData.isUsingToolLeft);
        if (isUsingToolUp)
            animator.SetTrigger(StaticData.isUsingToolUp);
        if (isUsingToolDown)
            animator.SetTrigger(StaticData.isUsingToolDown);

        if (isLiftingToolRight)
            animator.SetTrigger(StaticData.isLiftingToolRight);
        if (isLiftingToolLeft)
            animator.SetTrigger(StaticData.isLiftingToolLeft);
        if (isLiftingToolUp)
            animator.SetTrigger(StaticData.isLiftingToolUp);
        if (isLiftingToolDown)
            animator.SetTrigger(StaticData.isLiftingToolDown);

        if (isSwingingToolRight)
            animator.SetTrigger(StaticData.isSwingingToolRight);
        if (isSwingingToolLeft)
            animator.SetTrigger(StaticData.isSwingingToolLeft);
        if (isSwingingToolUp)
            animator.SetTrigger(StaticData.isSwingingToolUp);
        if (isSwingingToolDown)
            animator.SetTrigger(StaticData.isSwingingToolDown);

        if (isPickingRight)
            animator.SetTrigger(StaticData.isPickingRight);
        if (isPickingLeft)
            animator.SetTrigger(StaticData.isPickingLeft);
        if (isPickingUp)
            animator.SetTrigger(StaticData.isPickingUp);
        if (isPickingDown)
            animator.SetTrigger(StaticData.isPickingDown);

        if (idleUp)
            animator.SetTrigger(StaticData.idleUp);
        if (idleDown)
            animator.SetTrigger(StaticData.idleDown);
        if (idleLeft)
            animator.SetTrigger(StaticData.idleLeft);
        if (idleRight)
            animator.SetTrigger(StaticData.idleRight);
    }

    private void AnimationEventPlayFootstepSound()
    {
        //AudioService.Instance.PlaySound(SoundName.effectFootstepHardGround);
    }
}
