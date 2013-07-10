using UnityEngine;
using System.Collections;

// ReSharper disable CheckNamespace
public class FiringCaptive : CapturedBehavior {
// ReSharper restore CheckNamespace

    public ShootForward Shooter;
    public float shooterTimer = 0.2f;
    public float bulletSpeed = 10f;
    public int maxBullets = 10;
    public override void DoEnter()
    {
        Shooter.shootDelay = shooterTimer;
        Shooter.bulletSpeed = this.bulletSpeed;
        Shooter.maxBullets = this.maxBullets;
        
        Shooter.StartTimedShoot();
        
    }

    public override void DoUpdate()
    {
        if (Shooter.clickClick)
        {
            renderer.material.color = Color.white;
        }
    }

    public override void DoExit()
    {
        Shooter.StopTimedShoot();
    }
	
}
