using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PP1 : PlayerWeapon
{
    public PP1() : base("PP1", 20) { }
    public GameObject Bullet;
    public float DelayShoot;
    private bool delay = false;
    public Camera cam;


    [PunRPC]
    public void RpcShoot()
    {
        if (!delay)
        {
            delay = true;
            StartCoroutine(ShootWithDelay());
        }
    }


    IEnumerator ShootWithDelay()
    {
        // GameObject bullet = PhotonNetwork.Instantiate(Bullet.name, ShootPoint.transform.position, ShootPoint.transform.rotation);
        GameObject bullet = Instantiate(Bullet, ShootPoint.transform.position, ShootPoint.transform.rotation);
        Rigidbody br = bullet.GetComponent<Rigidbody>();
        // Vector3 camDirection = cam.transform.rotation.eulerAngles;
        Vector3 camDirection = cam.transform.forward + new Vector3(0, 0, 90);
        br.AddRelativeForce(camDirection * 100 * Time.deltaTime, ForceMode.Impulse);
        yield return new WaitForSeconds(DelayShoot);
        delay = false;

    }

    // Start is called before the first frame update
    public override void WeaponStart()
    {
        GameObject gM = GameObject.Find("GameManager");
        GameConfig gC = gM.GetComponent<GameConfig>();
        DelayShoot = gC.gameRules.DelayShoot/1000;
    }

    // Update is called once per frame
    public override void WeaponUpdate()
    {
    }
}
