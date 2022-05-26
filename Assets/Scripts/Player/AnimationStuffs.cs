using UnityEngine;

public class AnimationStuffs : MonoBehaviour
{
    
    public Animator animator;
    public SpriteRenderer  sr;
    
    

    public void Awake()
    {
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }


   

    public void Facing(float direction)
    {
        sr.flipX = direction switch
        {
            < 0f => true,
            > 0f => false,
            _ => sr.flipX
        };
    }

    public void PlayWalk(int mName, float var)
    {
        animator.SetFloat(mName, var);
    }

    public void JumpAnim(int mName, bool var)
    {
        animator.SetBool(mName, var);
    }

    public void DashAnim(int mName, bool var)
    {
        animator.SetBool(mName, var);
    }
}