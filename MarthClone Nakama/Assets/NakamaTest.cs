using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nakama;

public class NakamaTest : MonoBehaviour
{
    private const string PrefKeyName = "nakama.session";

    private IClient _client = new Client("defaultkey", "18.224.212.203", 7350, false);
    private ISession _session;

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
                return;
            }
        }
        var deviceid = SystemInfo.deviceUniqueIdentifier;
        _session = await _client.AuthenticateDeviceAsync(deviceid);
        PlayerPrefs.SetString(PrefKeyName, _session.AuthToken);
        Debug.Log(_session);
    }
}
