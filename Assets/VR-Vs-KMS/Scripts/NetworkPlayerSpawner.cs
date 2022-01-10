// using UnityEngine;
// using Photon.Pun;

// public class NetworkPlayerSpawner : MonoBehaviourPunCallbacks
// {
//     private GameObject ActivePrefab;

//     [SerializeField]
//     private GameObject VrPrefab;

//     [SerializeField]
//     private GameObject PcPrefab;
//     Camera MainCam;
//     private GameObject spawnedPlayerPrefab;
//     [SerializeField]
//     private string prefabName;

//     private bool KeyUp = false;

//     private bool first;

//     public void Start()
//     {
//         first = true;
//         MainCam = Camera.main;
//     }

//     public void Update()
//     {
//         if (Input.GetAxis("Switch view") == 1 & KeyUp)
//         {
//             ChangeType();
//             KeyUp = false;
//         }
//         if (Input.GetAxis("Switch view") == 0)
//         {
//             KeyUp = true;
//         }
//     }

//     public override void OnJoinedRoom()
//     {
//         ActivePrefab = PcPrefab;
//         prefabName = ActivePrefab.name;

//         base.OnJoinedRoom();
//         if (MainCam != null)
//         {
//             MainCam.gameObject.SetActive(false);

//         }
//         CreatePlayer(prefabName, transform.position, transform.rotation);
//     }

//     public void CreatePlayer(string prefabName, Vector3 position, Quaternion rotation)
//     {
//         spawnedPlayerPrefab = PhotonNetwork.Instantiate(prefabName, position, rotation);
//         if(prefabName == PcPrefab.name)
//         {
//             MonoBehaviour[] comps = spawnedPlayerPrefab.transform.GetChild(0).GetComponents<MonoBehaviour>();
//             foreach(MonoBehaviour c in comps)
//             {
//                 c.enabled = true;
//             }
//             spawnedPlayerPrefab.transform.GetChild(1).gameObject.SetActive(true);
//         }
//     }

//     public override void OnLeftRoom()
//     {
//         base.OnLeftRoom();
//         PhotonNetwork.Destroy(spawnedPlayerPrefab);
//         if (MainCam != null)
//         {
//             MainCam.gameObject.SetActive(true);
//         }
//     }

//     private void ChangeType()
//     {
//         if (ActivePrefab == PcPrefab)
//         {
//             ActivePrefab = VrPrefab;
//             PhotonNetwork.Destroy(spawnedPlayerPrefab);
//             CreatePlayer(VrPrefab.name, transform.position, transform.rotation);
//         }
//         else
//         {
//             ActivePrefab = PcPrefab;
//             PhotonNetwork.Destroy(spawnedPlayerPrefab);
//             CreatePlayer(PcPrefab.name, transform.position, transform.rotation);
//         }
//     }
// }
