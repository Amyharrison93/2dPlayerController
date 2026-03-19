using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [field: SerializeField] public Animator animator {get; private set;}
    [field: SerializeField] public string MoveAnimationName {get; private set;}
    [field: SerializeField] public string MoveAnimationValue {get; private set;}
    [field: SerializeField] public string jumpAnimationName {get; private set;}
    private bool wasRight;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void HandleMoveAnimations(float value)
    {
        if(value == 0)
        {
            if(wasRight)animator.Play("KiwiIdleRight");
            else animator.Play("KiwiIdleLeft");
        }
        if(value > 0.1)
        {
            if(animator.GetCurrentAnimatorStateInfo(0).IsName("KiwiRunRight")) return;
            animator.Play("KiwiRunRight");
            wasRight = true;
        }
        if(value < -0.9)
        {
            if(animator.GetCurrentAnimatorStateInfo(0).IsName("KiwiRunLeft")) return;
            animator.Play("KiwiRunLeft");
            wasRight = false;
        }
    }
    public void HandleVerticleMoveAnimation(Vector2 value)
    {
        string jumpOrFall = "Right";
        string leftOrRight = "Jump";

        if(value.x > 0)leftOrRight = "Right";
        if(value.x < 0)leftOrRight = "Left";

        if(value.y > 0)jumpOrFall = "Jump";
        if(value.y > 0)jumpOrFall = "Fall";

        animator.Play("Kiwi"+jumpOrFall+leftOrRight);
    }
    public void HandleDashAnimation(bool isRight)
    {
        if(isRight)animator.Play("KiwiDashRight");
        else animator.Play("KiwiDashLeft");
    }
}
