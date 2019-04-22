using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nakama;
using System;

public class NakamaTest : MonoBehaviour
{
    private const string PrefKeyName = "nakama.session";

    private IClient _client = new Client("defaultkey", "18.224.212.203", 7350, false);
    private ISession _session;
    private ISocket _socket;
    IMatch myMatch;

    private async void Awake()
    {
        Debug.Log("NakamaTest: Awake");
        var authtoken = ""; // PlayerPrefs.GetString(PrefKeyName);
        Debug.Log("PlayerPrefsGetString: " + authtoken);
        if (!string.IsNullOrEmpty(authtoken))
        {
            //Debug.Log("Auth token was not null or open!");
            var session = Session.Restore(authtoken);
            //Debug.Log(new DateTime(session.ExpireTime));
            if (!session.IsExpired)
            {
                _session = session;
                Debug.Log(_session);
                Debug.Log("Hit this weird place.");
                return;
            }
        }
        //var deviceid = SystemInfo.deviceUniqueIdentifier;
        var email = "notnearlymad@gmail.com";
        var password = "default_password";
        var username = "notnearlymad";
        _session = await _client.AuthenticateEmailAsync(email, password, username, true);

        var payload = "{\"name\": \"Jon\"}";
        var rpcid = "hello_world";
        var pokemonInfo = await _client.RpcAsync(_session, rpcid, payload);
        Debug.LogFormat("Retrieved pokemon info: {0}", pokemonInfo);
    }

    /**
    private async void Awake()
    {
        Debug.Log("NakamaTest: Awake");
        var authtoken = ""; // PlayerPrefs.GetString(PrefKeyName);
        Debug.Log("PlayerPrefsGetString: " + authtoken);
        if (!string.IsNullOrEmpty(authtoken))
        {
            //Debug.Log("Auth token was not null or open!");
            var session = Session.Restore(authtoken);
            //Debug.Log(new DateTime(session.ExpireTime));
            if (!session.IsExpired)
            {
                _session = session;
                Debug.Log(_session);
                Debug.Log("Hit this weird place.");
                return;
            }
        }
        //var deviceid = SystemInfo.deviceUniqueIdentifier;
        var email = "notnearlymad@gmail.com";
        var password = "suckmydick";
        var username = "notnearlymad";
        _session = await _client.AuthenticateEmailAsync(email, password, username, false);
        //PlayerPrefs.SetString(PrefKeyName, _session.AuthToken);
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
        Debug.Log(new DateTime(_session.ExpireTime));
    }
    **/

    public async void AttemptMatchmake()
    {
        Debug.Log("Begin Attempted Matchmaking!");
        var query = "*";
        var minCount = 2;
        var maxCount = 2;
  

        var matchmakerTicket = await _socket.AddMatchmakerAsync(
            query, minCount, maxCount);
        _socket.OnMatchmakerMatched += async (_, matched) =>
        {
            Debug.LogFormat("Received MatchmakerMatched message: {0}", matched);
            var opponents = string.Join(",", matched.Users); // printable list.
            Debug.LogFormat("Matched opponents: {0}", opponents);
            
            myMatch = await _socket.JoinMatchAsync(matched);
            if(myMatch != null)
            {
                Debug.Log("Match ID: " + myMatch.Id);
            }
            else
            {
                Debug.Log("NO MATCH!");
            }
            CreateMessageListener();
        };
    }

    public void AttemptSendMessage()
    {
        if (myMatch != null)
        {
            _socket.SendMatchState(myMatch.Id, 1, "Hello!");
            Debug.Log("Msg Sent.");
        }
        else
        {
            Debug.Log("No Match.");
        }
    }

    void CreateMessageListener()
    {
        _socket.OnMatchState += (_, state) => {
            var content = System.Text.Encoding.UTF8.GetString(state.State);
            switch (state.OpCode)
            {
                case 101:
                    Debug.Log("A custom opcode.");
                    break;
                default:
                    Debug.LogFormat("User {0} sent {1}", state.UserPresence.Username, content);
                    break;
            }
        };
    }
}
