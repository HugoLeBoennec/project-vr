using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConfig : MonoBehaviour
{
    public TextAsset textJson;


    public GameRule gameRules = new GameRule();

    // Start is called before the first frame update
    void Start()
    {
        gameRules = JsonUtility.FromJson<GameRule>(textJson.text);
        // print(CurrentName.typePlayer);
    }

    // Update is called once per frame
    void Update()
    {

    }
}

[System.Serializable]
public class GameRule
{

    public IDictionary<string, Color> ColorsShot = new Dictionary<string, Color>(){
        {"Green", Color.green},
        {"Blue", Color.blue},
        {"Red", Color.red},
        {"Yellow", Color.yellow},
        {"Gray", Color.gray}
    };
  
    public int LifeNumber;
    public int DelayShoot;
    public int DelayTeleport;
    public string ColorShotVirus;
    public string ColorShotKMS;
    public int RadiusExplosion;
    public int TimeToAreaContamination;

    // private void Awake()
    // {
    //     Instance = this;
    // }
}
