using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ShootManager : MonoBehaviourPunCallbacks
{
    public PlayerWeapon weapon;

    [SerializeField]
    private LayerMask mask;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1")){
            Shoot();
        }
    }

    public void Shoot(){
        // RaycastHit hit;
        // if(Physics.Raycast(transform.position,transform.forward,out hit, weapon.range, mask)){
        //     Debug.Log("Object Touché : " + hit.collider.name);
        // }
        
    }

    [PunRPC]
    public void Projectil(){

    }
}
