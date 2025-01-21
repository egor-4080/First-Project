using UnityEngine;

public class StartPistol : Weapon
{
    private Animator animator;

    protected override void Awake()
    {
        base.Awake();

        animator = GetComponent<Animator>();
    }

    protected override void Fire(bool isFacing)
    {
        base.Fire(isFacing);
        objectPool.SpawnBullet(damage, isFacing, spawnPoint, spreadAngle);
        animator.Play("Fire");
    }
}