using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class DammageManager : MonoBehaviourPun
{
    public int health = 5;

    Text TxtHealth;

    [SerializeField]
    string hUD;

    Collider[] colliders;

    [PunRPC]
    public void RPCRemoveLife()
    {
        if (!photonView.IsMine)
        {
            return;
        }
        else
        {
            
            health -= 1;
            TxtHealth.text = "Health : " + health;
            Debug.Log(TxtHealth.text);
        }
    }

    void OnCollisionEnter(Collision Col)
    {
        Debug.Log(Col.gameObject.tag);

        if (Col.gameObject.tag == "BulletVr")
        {
            photonView.RPC("RPCRemoveLife", RpcTarget.All);
            Destroy(Col.gameObject);
        }
    }

    // void OnTriggerEnter(Collision Col)
    // {
    //     Debug.Log(Col.gameObject.tag);
    //     if (Col.gameObject.tag == "Bullet")
    //     {
    //         photonView.RPC("RPCRemoveLife", RpcTarget.All);
    //         Destroy(Col.gameObject);
    //     }
    // }

    // Start is called before the first frame update
    void Start()
    {
        TxtHealth = GameObject.Find(hUD).GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(photonView.IsMine)
        {
            TxtHealth.text = "Health : " + health;
        }
        colliders = Physics.OverlapCapsule(transform.position  - new Vector3(0,1,0), transform.position  + new Vector3(0,1,0),0.5f);
        foreach (var collider in colliders)
        {
            Debug.Log("Tag : " + collider.gameObject.tag);
            if(collider.gameObject.tag == "BulletPc")
            {
                photonView.RPC("RPCRemoveLife", RpcTarget.All);
                Destroy(collider.gameObject);
            }
        }
    }
}
