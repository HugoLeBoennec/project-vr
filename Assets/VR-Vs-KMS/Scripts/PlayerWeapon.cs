using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Pun;
using Valve.VR;

public abstract class PlayerWeapon : MonoBehaviourPun
{
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
        if(WeaponName == "PP2")
        {
            if (SteamVR_Input.GetAction<SteamVR_Action_Boolean>("GrabPinch").state)
            {
                photonView.RPC("RpcShootVr", RpcTarget.All);
            }
        }
        else
        {
        }
    }

    public abstract void WeaponStart();
    public abstract void WeaponUpdate();
}
