using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDamage : DamageType
{
    [SerializeField] GameObject projectileToSpawn;
    [SerializeField] Transform spawnLocation;

    public ProjectileWeapon projectileWeapon { get; private set; }

    protected override void Start()
    {
        base.Start();
        projectileWeapon = GetComponent<ProjectileWeapon>();
    }

    public void LaunchProjectile()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(.5f, .5f, 0));
        RaycastHit hit;

        Vector3 destination = new Vector3();

        if(Physics.Raycast(ray, out hit))
        {
            destination = hit.point;
        }
        else
        {
            destination = ray.GetPoint(1000);
        }

        GameObject projectile = Instantiate(projectileToSpawn, spawnLocation.position, spawnLocation.rotation);
        projectile.GetComponent<Projectile>().owner = this;
        projectile.GetComponent<Projectile>().Init();
        projectile.GetComponent<Rigidbody>().velocity = (destination - spawnLocation.position).normalized * projectileWeapon.projectileVelocity;
        //projectile.GetComponent<Rigidbody>().velocity = projectile.transform.forward.normalized * harpoonGun.projectileVelocity;
        //projectile.GetComponent<Rigidbody>().AddForce(Vector3.forward * harpoonGun.projectileVelocity);
    }

}
