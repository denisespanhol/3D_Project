using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : Singleton<AnimationManager>
{
    public Animator animator;
    public List<AnimatorSetup> animatorSetups;

    public enum AnimationType
    {
        IDLE,
        RUN,
        JUMP
    }

    public void Play(AnimationType type, float currentSpeedFactor = 1)
    {
        animatorSetups.ForEach(animatorSetup =>
        {
            if (animatorSetup.animationType == type) animator.SetTrigger(animatorSetup.trigger);
            animator.speed = animatorSetup.speed * currentSpeedFactor;
        });
    }
}

[System.Serializable]
public class AnimatorSetup
{
    public AnimationManager.AnimationType animationType;
    public string trigger;
    public float speed = 1f;
}
