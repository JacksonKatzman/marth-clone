using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nakama;

public class NakamaTest : MonoBehaviour
{
    private const string PrefKeyName = "nakama.session";

    private IClient _client = new Client("defaultkey", "18.224.212.203", 7350, false);
    private ISession _session;
    private ISocket _socket;

    private async void Awake()
    {
        Debug.Log("NakamaTest: Awake");
        var authtoken = PlayerPrefs.GetString(PrefKeyName);
        if (!string.IsNullOrEmpty(authtoken))
        {
            var session = Session.Restore(authtoken);
            if (!session.IsExpired)
            {
                _session = session;
                Debug.Log(_session);
                Debug.Log("Hit this weird place.");
                return;
            }
        }
        var deviceid = SystemInfo.deviceUniqueIdentifier;
        _session = await _client.AuthenticateDeviceAsync(deviceid);
        PlayerPrefs.SetString(PrefKeyName, _session.AuthToken);
        _socket = _client.CreateWebSocket();
        _socket.OnConnect += (sender, args) =>
        {
            Debug.Log("Socket connected.");
        };
        _socket.OnDisconnect += (sender, args) =>
        {
            Debug.Log("Socket disconnected.");
        };
        await _socket.ConnectAsync(_session);
        Debug.Log(_session);
    }

    public async void AttemptMatchmake()
    {
        Debug.Log("Begin Attempted Matchmaking!");
        var query = "*";
        var minCount = 2;
        var maxCount = 2;
  

        var matchmakerTicket = await _socket.AddMatchmakerAsync(
            query, minCount, maxCount);
        _socket.OnMatchmakerMatched += (_, matched) =>
        {
            Debug.LogFormat("Received MatchmakerMatched message: {0}", matched);
            var opponents = string.Join(",", matched.Users); // printable list.
            Debug.LogFormat("Matched opponents: {0}", opponents);
        };
    }
}
