using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BulletManager : MonoBehaviour
{
    private Text TxtHealth;
    // Start is called before the first frame update
    void Start()
    {
        Color currentColor;
        MeshRenderer rendu = gameObject.GetComponent<MeshRenderer>();
        GameObject gM = GameObject.Find("GameManager");
        GameConfig gC = gM.GetComponent<GameConfig>();
        if(gameObject.name.Contains("Bullet Pc"))
        { 
            currentColor = gC.gameRules.ColorsShot[gC.gameRules.ColorShotKMS];
        } 
        else
        {
            currentColor = gC.gameRules.ColorsShot[gC.gameRules.ColorShotVirus];

        }
            rendu.material.SetColor("_Color", currentColor);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter(Collision Col)
    {
        if(Col.gameObject.tag != "Player" )
        {
            Destroy(gameObject);
        }
    }
}
