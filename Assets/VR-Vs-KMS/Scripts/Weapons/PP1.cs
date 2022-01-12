using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PP1 : PlayerWeapon
{
    public PP1() : base("PP1",20) { }
    public float DelayShoot;
    private bool delay = false;

    [PunRPC]
    public void RpcShoot()
    {
        if(!delay)
        {
            delay = true;
            StartCoroutine(ShootWithDelay());
        }
    }

    IEnumerator ShootWithDelay()
    {
        GameObject bullet = Instantiate(Bullet,ShootPoint.transform.position,ShootPoint.transform.rotation);
        Rigidbody br = bullet.GetComponent<Rigidbody>();
        br.AddRelativeForce(Vector3.forward * 1000 * Time.deltaTime, ForceMode.Impulse);
        yield return new WaitForSeconds(DelayShoot);
        delay = false;
        
    }

    // Start is called before the first frame update
    public override void WeaponStart()
    {
        
    }

    // Update is called once per frame
    public override void WeaponUpdate()
    {
        
    }
}
