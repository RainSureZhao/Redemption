using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; 

    private Dictionary<string, Player> _players = new Dictionary<string, Player>();

    private static string _info;

    [SerializeField] public MatchingSettings matchingSettings;

    private void Awake()
    {
        instance = this;
    }

    public void RegisterPlayer(string name, Player player)
    {
        _players.Add(name, player);
    }

    public static void UpdateInfo(string info)
    {
        _info = info;
    }

    public void UnRegisterPlayer(string name)
    {
        _players.Remove(name);
    }

    public Player GetPlayer(string name)
    {
        return _players[name];
    }

    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(200f, 200f, 200f, 400f));
        GUILayout.BeginVertical();

        GUI.color = Color.red;
        foreach (var name in _players.Keys)
        {
            var player = GetPlayer(name);
            GUILayout.Label(name + " - " + player.GetHealth());
            // GUILayout.Label(name);
        }

        GUILayout.EndVertical();
        GUILayout.EndArea();
    }
}
