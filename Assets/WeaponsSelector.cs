using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsSelector : MonoBehaviour
{
    [SerializeField]
    GameObject pistol;
    [SerializeField]
    private GameObject bone;
    // Start is called before the first frame update
    void Start()
    {
        GameObject myPistol = Instantiate(pistol) as GameObject;
        // GameObject myPistol = Instantiate(pistol, bone.transform.position + new Vector3(-0.04f, 0.15f, 0.04f), bone.transform.rotation * new Quaternion(-90f,0f,90f,0f)) as GameObject;
        myPistol.transform.parent = bone.transform;
        myPistol.transform.localRotation = Quaternion.Euler(-90f, 0f, 90f);
        myPistol.transform.localPosition = new Vector3(-0.04f, 0.15f, 0.04f);

    }

    // Update is called once per frame
    void Update()
    {

    }
}
