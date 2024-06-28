using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;

[System.Serializable]
public class PlayerInfo
{
    public int Coins;
    public int Height;
    public int Width;
    public int Level;
}

public class Progress : MonoBehaviour
{
    public PlayerInfo PlayerInfo;
    public static Progress Instance;

    [DllImport("__Internal")]
    private static extern void SaveExtern(string data);
    [DllImport("__Internal")]
    private static extern void LoadExtern();

    [SerializeField] TextMeshProUGUI _playerInfoText;
    private void Awake()
    {
        if (Instance == null)
        {
            transform.parent = null;
            DontDestroyOnLoad(gameObject);
            Instance = this;
#if UNITY_WEBGL
            LoadExtern();
#endif

        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Save()
    {
        string jsonString = JsonUtility.ToJson(PlayerInfo);
#if UNITY_WEBGL
        SaveExtern(jsonString);
#endif
    }
    public void SetPlayerInfo(string value)
    {
        PlayerInfo = JsonUtility.FromJson<PlayerInfo>(value);
        _playerInfoText.text = PlayerInfo.Coins + "\n" + PlayerInfo.Width + "\n" + PlayerInfo.Height + "\n" + PlayerInfo.Level;
    }

}
