using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    //handle to animator
    private Animator _anim;
    private Animator _swordAnimator;

    // Start is called before the first frame update
    void Start()
    {
        //assign handle to animator
        _anim = GetComponentInChildren<Animator>();
        _swordAnimator = transform.GetChild(1).GetComponent<Animator>();
    }

    public void Move(float move)
    {
        //anim set float Move, move
        _anim.SetFloat("Move", Mathf.Abs(move));
    }

    public void Jump(bool jumping)
    {
        _anim.SetBool("IsJumping", jumping);
    }

    public void Attack()
    {
        _anim.SetTrigger("Attack");
        _swordAnimator.SetTrigger("SwordAnimation");
    }

    void Update()
    {
        if (_anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            _anim.SetBool("IsAttack", true);
        }
        else
        {
            _anim.SetBool("IsAttack", false);
        }
    }

    public void Death()
    {
        _anim.SetBool("IsJumping", false);
        _anim.SetBool("IsDied", true);
        _anim.SetTrigger("Death");
    }

    public void Hit()
    {
        _anim.SetTrigger("Hit");
    }
}
