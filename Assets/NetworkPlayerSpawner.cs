using UnityEngine;
using Photon.Pun;

public class NetworkPlayerSpawner : MonoBehaviourPunCallbacks
{
    private GameObject ActivePrefab;

    [SerializeField]
    private GameObject VrPrefab;

    [SerializeField]
    private GameObject PcPrefab;

    [SerializeField]
    private GameObject Teleport;

    [SerializeField]
    private GameObject TeleportArea;
    Camera MainCam;
    private GameObject spawnedPlayerPrefab;
    private string prefabName = "TestPlayer";

    private bool first;

    private GameObject tp;

    public void Start()
    {
        first = true;
        MainCam = Camera.main;
        // tp = Instantiate(Teleport);
        tp.SetActive(false);

    }

    public void Update()
    {
        // if (Input.GetButtonDown("Switch view"))
        // {
        //     ChangeType();
        // }
    }

    public override void OnJoinedRoom()
    {
        if (CurrentName.typePlayer == "KMS")
        {
            ActivePrefab = PcPrefab;
        }
        else
        {
            ActivePrefab = VrPrefab;
        }

        prefabName = ActivePrefab.name;

        base.OnJoinedRoom();
        if (MainCam != null)
        {
            MainCam.gameObject.SetActive(false);

        }
        CreatePlayer(prefabName, transform.position, transform.rotation);
    }

    public void CreatePlayer(string prefabName, Vector3 position, Quaternion rotation)
    {
        spawnedPlayerPrefab = PhotonNetwork.Instantiate(prefabName, position, rotation);
        if (prefabName == PcPrefab.name)
        {
            MonoBehaviour[] comps = spawnedPlayerPrefab.transform.GetChild(0).GetComponents<MonoBehaviour>();
            foreach (MonoBehaviour c in comps)
            {
                c.enabled = true;
            }
            spawnedPlayerPrefab.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            tp.SetActive(true);
            TeleportArea.SetActive(true);;
            // GameObject.Find("TP_Floor")
        }
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        PhotonNetwork.Destroy(spawnedPlayerPrefab);
        if (MainCam != null)
        {
            MainCam.gameObject.SetActive(true);
        }
    }

    //     public void ChangeType()
    //     {
    //         if (ActivePrefab == PcPrefab)
    //         {
    //             ActivePrefab = VrPrefab;
    //             PhotonNetwork.Destroy(spawnedPlayerPrefab);
    //             CreatePlayer(VrPrefab.name, transform.position, transform.rotation);
    //             Debug.Log(GameObject.Find("Teleporting(Clone)"));
    //             Debug.Log("test test test");
    //             // GameObject.Find("Teleporting").SetActive(true);
    //         }
    //         else
    //         {
    //             ActivePrefab = PcPrefab;
    //             Debug.Log(GameObject.Find("Teleporting(Clone)"));
    //             Debug.Log("test test test");
    //             // GameObject.Find("Teleporting").SetActive(false);
    //             PhotonNetwork.Destroy(spawnedPlayerPrefab);
    //             CreatePlayer(PcPrefab.name, transform.position, transform.rotation);
    //         }
    //     }
}
