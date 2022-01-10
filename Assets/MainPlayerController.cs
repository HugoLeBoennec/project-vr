using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayerController : MonoBehaviour
{

    private GameObject _ActivePrefab;

    [SerializeField]
    private GameObject VrPrefab;

    [SerializeField]
    private GameObject PcPrefab;

    // Start is called before the first frame update
    void Start()
    {
        this.ActivePrefab = PcPrefab;
    }
    
    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Switch view") == 1 ){
            this.ActivePrefab = new GameObject();
        }
    }

    public GameObject ActivePrefab
    {
        get => _ActivePrefab;
        set
        {
            if(_ActivePrefab == PcPrefab){
                _ActivePrefab = VrPrefab;
            }
            else{
                _ActivePrefab = PcPrefab;
            }
        }
    }
}
