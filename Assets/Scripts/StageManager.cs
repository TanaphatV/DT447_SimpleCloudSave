using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.CloudSave;
using TMPro;
[System.Serializable]
public class StageSaveNode
{
    public Vector3 playerPosition;
    public List<StampSaveNode> stampNodes;
    public StageSaveNode(Vector3 playerPosition,List<StampSaveNode> stampNodes)
    {
        this.playerPosition = playerPosition;
        this.stampNodes = stampNodes;
    }
    public StageSaveNode(Vector3 playerPosition)
    {
        this.playerPosition = playerPosition;
        this.stampNodes = new List<StampSaveNode>();
    }
    [UnityEngine.Scripting.Preserve]
    public StageSaveNode()
    {

    }
}

public class StageManager : MonoBehaviour
{
    public static StageManager instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.Log("Error: instance is null");
            }
            return _instance;
        }
    }
    static StageManager _instance;
    // Start is called before the first frame update
    void Awake()
    {
        _instance = this;
    }
    [SerializeField] TextMeshProUGUI statusText;

    [SerializeField] PlayerController player;
    // Start is called before the first frame update
    async void Start()
    {
        statusText.text = "Loading...";
        Debug.Log("TryLoad");
        var client = CloudSaveService.Instance.Data;
        var data = await client.Player.LoadAsync(new HashSet<string> { "stage"});
        if(data.TryGetValue("stage",out var keyName))
        {
            StageSaveNode node = keyName.Value.GetAs<StageSaveNode>();
            player.transform.position = node.playerPosition;
            foreach(var stampNode in node.stampNodes)
            {
                StampManager.instance.LoadStamp(stampNode);
            }
            Debug.Log("Loaded");
        }
        statusText.text = "";
        player.pause = false;

    }

    public async void SaveStage()
    {
        Debug.Log("Saving");
        statusText.text = "Saving...";
        player.pause = true;
        var client = CloudSaveService.Instance.Data;
        List<StampSaveNode> stampNode = new List<StampSaveNode>();
        foreach (var stamp in FindObjectsOfType<Stamp>())
        {
            stampNode.Add(stamp.GetSaveNode());
        }
        StageSaveNode node = new StageSaveNode(player.transform.position,stampNode);
        var data = new Dictionary<string, object> { { "stage",node} };
        await client.Player.SaveAsync(data);
        player.pause = false;
        Debug.Log("Save Complete");
        statusText.text = "";
    }
}
