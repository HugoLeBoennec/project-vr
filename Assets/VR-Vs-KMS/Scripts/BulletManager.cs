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
