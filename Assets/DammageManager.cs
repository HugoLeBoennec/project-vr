using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class DammageManager : MonoBehaviourPun
{
    public int health = 5;

    Text TxtHealth;

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
        if (Col.gameObject.tag == "Bullet")
        {
            photonView.RPC("RPCRemoveLife", RpcTarget.All);
            Destroy(Col.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        TxtHealth = GameObject.Find("HealthText").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(photonView.IsMine)
        {
            TxtHealth.text = "Health : " + health;
            Debug.Log(health);
        }
    }
}
