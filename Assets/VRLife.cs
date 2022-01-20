using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRLife : MonoBehaviour
{
    // Start is called before the first frame update
    int playerHealth;
    DammageManager playerDammage;
    public GameObject DeathPanel;


    void Start()
    {

        playerDammage = GetComponentInChildren<DammageManager>(true);
        DeathPanel = GameObject.Find("HUD/DeadCanvas");
        DeathPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        playerHealth = playerDammage.health;

        if (playerHealth <= 0)
        {
            transform.position = new Vector3(50, 50, 56.15f);
            StartCoroutine(Respwan());


        }
    }

    IEnumerator Respwan()
    {

        //yield on a new YieldInstruction that waits for 3 seconds.}
        yield return new WaitForSeconds(3);
        playerDammage.health = 5;
        transform.position = new Vector3(-34, 0.8f, 4);

    }
}
