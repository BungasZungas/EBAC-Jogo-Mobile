using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    public Animator animator;
    public List<AnimatorSetup> animatorSetups;

    public enum AnimationType
    {
        IDLE,
        RUN,
        DEAD
    }

    public void Play(AnimationType type, float currentSpeedGactor = 1)
    {
        foreach(var animation in animatorSetups)
        {
            if(animation.type == type)
            {
                animator.SetTrigger(animation.trigger);
                animator.speed = animation.speed * currentSpeedGactor;
                break;
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Play(AnimationType.RUN);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            Play(AnimationType.DEAD);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Play(AnimationType.IDLE);
        }
    }
}

[System.Serializable]
public class AnimatorSetup
{
    public AnimatorManager.AnimationType type;
    public string trigger;
    public float speed = 1;
}
