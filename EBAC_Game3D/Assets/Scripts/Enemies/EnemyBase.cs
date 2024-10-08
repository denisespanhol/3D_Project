using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Animation;

public class EnemyBase : MonoBehaviour, IDamageable
{
    public Collider collider;
    public FlashColor flashColor;
    public ParticleSystem particlesSystem;
    public float startLife = 10f;
    public bool lookAtPlayer = false;

    [SerializeField] private float _currentLife;

    [Header("Animation")]
    [SerializeField] private AnimationBase _animationBase;

    [Header("Start Animation")]
    public float startAnimationDuration = .2f;
    public Ease startAnimationEase = Ease.OutBack;
    public bool startWithBornAnimation = true;

    private Player _player;

    private void Awake()
    {
        Init();
    }

    private void Start()
    {
        _player = GameObject.FindObjectOfType<Player>();
    }

    protected void ResetLife()
    {
        _currentLife = startLife;
    }

    protected virtual void Init()
    {
        ResetLife();
        BornAnimation();
    }

    protected virtual void Kill()
    {
        OnKill();
    }

    protected virtual void OnKill()
    {
        if (collider != null) collider.enabled = false;
        Destroy(gameObject, 3f);
        PlayAnimationByTrigger(AnimationType.DEATH);
    }

    public void OnDamage(float f)
    {
        if (flashColor != null) flashColor.Flash();
        if (particlesSystem != null) particlesSystem.Emit(15);

              
        _currentLife -= f;

        if (_currentLife <= 0)
        {
            Kill();
        }
    }

    #region ANIMATION
    private void BornAnimation()
    {
        transform.DOScale(0, startAnimationDuration).SetEase(startAnimationEase).From();
    }

    public void PlayAnimationByTrigger(AnimationType animationType)
    {
        _animationBase.PlayAnimationByTrigger(animationType);
    }
    #endregion

    public void Damage(float damage)
    {
        OnDamage(damage);
    }

    public void Damage(float damage, Vector3 dir)
    {
        OnDamage(damage);

        transform.position -= dir;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Player p = collision.transform.GetComponent<Player>();

        if (p != null)
        {
            p.Damage(1);
        }
    }

    public virtual void Update()
    {
        if(lookAtPlayer)
        {
            transform.LookAt(_player.transform.position);
        }
    }

}
