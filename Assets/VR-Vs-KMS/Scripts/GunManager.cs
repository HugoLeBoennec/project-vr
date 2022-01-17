using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Pun;

public class GunManager : MonoBehaviourPun
{
    public GameObject Weapon;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            if(Input.GetMouseButton(0))
            {
                Weapon.GetComponent<PlayerWeapon>().Shoot();
            }
        }
    }
}
