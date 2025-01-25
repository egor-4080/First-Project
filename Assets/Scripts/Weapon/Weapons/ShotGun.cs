using UnityEngine;

public class ShotGun : Weapon
{
    [SerializeField] private int bulletsAmount;
    [SerializeField] private AnimationClip fireAnimation;

    private Animator animator;
    private bool isAnimationReloaded = true;

    protected override void Awake()
    {
        base.Awake();

        animator = GetComponent<Animator>();
    }


    protected override void Fire(bool isFacing)
    {
        base.Fire(isFacing);

        if (isAnimationReloaded)
        {
            for (int i = 0; i < bulletsAmount; i++)
            {
                objectPool.SpawnBullet(damage, isFacing, spawnPoint, spreadAngle);
            }

            isAnimationReloaded = false;
            animator.Play(fireAnimation.name);
        }
    }

    public void AnimationReload()
    {
        isAnimationReloaded = true;
    }
}