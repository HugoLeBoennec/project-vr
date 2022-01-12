using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Pun;

public abstract class PlayerWeapon : MonoBehaviourPun
{
    public GameObject Bullet;
    public GameObject ShootPoint;
    private string WeaponName;
    private int WeaponDamage;

    public PlayerWeapon(string WeaponName, int WeaponDamage)
    {
        this.WeaponName = WeaponName;
        this.WeaponDamage = WeaponDamage;
    }

    void Start()
    {
        WeaponStart();
    }
    
    public void Shoot()
    {
        photonView.RPC("RpcShoot", RpcTarget.All);
    }

    void Update()
    {
        WeaponUpdate();
    }

    public abstract void WeaponStart();
    public abstract void WeaponUpdate();
}
