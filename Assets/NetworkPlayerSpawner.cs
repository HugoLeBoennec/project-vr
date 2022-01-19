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

    private GameObject tp;

    public void Start()
    {
        MainCam = Camera.main;
    }

    public void Update()
    {

    }

    public override void OnJoinedRoom()
    {
        if (CurrentName.typePlayer == "Virus")
        {
            ActivePrefab = VrPrefab;
        }
        else
        {
            ActivePrefab = PcPrefab;
        }

        prefabName = ActivePrefab.name;

        base.OnJoinedRoom();
        if (MainCam != null)
        {
            MainCam.gameObject.SetActive(false);

        }
        CreatePlayer(prefabName, new Vector3(-1,1,5), new Vector3(-34,0.8f,4), transform.rotation);
    }

    public void CreatePlayer(string prefabName, Vector3 positionPc, Vector3 positionVr, Quaternion rotation)
    {
        if (prefabName == PcPrefab.name)
        {
            spawnedPlayerPrefab = PhotonNetwork.Instantiate(prefabName, positionPc, Quaternion.Euler(0, -180, 0));
            MonoBehaviour[] comps = spawnedPlayerPrefab.transform.GetChild(0).GetComponents<MonoBehaviour>();
            foreach (MonoBehaviour c in comps)
            {
                c.enabled = true;
            }
            spawnedPlayerPrefab.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            spawnedPlayerPrefab = PhotonNetwork.Instantiate(prefabName, positionVr, Quaternion.Euler(0, 90, 0));
            PrepareVR();
            
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

    public void PrepareVR()
    {
        if(!GameObject.Find("Teleporting(Clone)"))
        {
            Instantiate(Teleport);
            Instantiate(TeleportArea);
        }
    }
}
