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

    public void Start()
    {
        first = true;
        MainCam = Camera.main;
    }

    public void Update()
    {
        if (Input.GetButtonDown("Switch view"))
        {
            ChangeType();
        }
    }

    public override void OnJoinedRoom()
    {
        if(CurrentName.typePlayer == "KMS")
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
        if(prefabName == PcPrefab.name)
        {
            MonoBehaviour[] comps = spawnedPlayerPrefab.transform.GetChild(0).GetComponents<MonoBehaviour>();
            foreach(MonoBehaviour c in comps)
            {
                c.enabled = true;
            }
            spawnedPlayerPrefab.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            Instantiate(Teleport, position, rotation);
            Instantiate(TeleportArea, new Vector3(0,0.01f,0), rotation);
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

    public void ChangeType()
    {
        if (ActivePrefab == PcPrefab)
        {
            ActivePrefab = VrPrefab;
            PhotonNetwork.Destroy(spawnedPlayerPrefab);
            CreatePlayer(VrPrefab.name, transform.position, transform.rotation);
        }
        else
        {
            ActivePrefab = PcPrefab;
            PhotonNetwork.Destroy(spawnedPlayerPrefab);
            CreatePlayer(PcPrefab.name, transform.position, transform.rotation);
        }
    }
}
