using System.Collections.Generic;
using Unity.Services.Authentication;
using Unity.Services.CloudCode;
using Unity.Services.Core;
using UnityEngine;

/*
 * Note: You must have a published script to use the Cloud Code SDK.
 * You can publish a script from the Unity Cloud Dashboard - https://cloud.unity.com
 */
public class TestAPI : MonoBehaviour
{
    /*
     * CloudCodeResponse represents the response from the script, used for deserialization.
     * In this example, the script returns a JSON in the format
     * {"welcomeMessage": "Hello, arguments['name']. Welcome to Cloud Code!"}
     */
    class CloudCodeResponse
    {
        public string welcomeMessage;
    }

    /*
     * Initialize all Unity Services and Sign In an anonymous player.
     * You can perform this operation in a more centralized spot in your project
     */
    public async void Awake()
    {
        await UnityServices.InitializeAsync();
        SetupEvents();
        await UnityServices.InitializeAsync();
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }
    // Setup authentication event handlers if desired
    void SetupEvents()
    {
        AuthenticationService.Instance.SignedIn += () => {
            // Shows how to get a playerID
            Debug.Log($"PlayerID: {AuthenticationService.Instance.PlayerId}");

            // Shows how to get an access token
            Debug.Log($"Access Token: {AuthenticationService.Instance.AccessToken}");
           // AuthenticationService.Instance.Player

        };

        AuthenticationService.Instance.SignInFailed += (err) => {
            Debug.LogError(err);
        };

        AuthenticationService.Instance.SignedOut += () => {
            Debug.Log("Player signed out.");
        };

        AuthenticationService.Instance.Expired += () =>
        {
            Debug.Log("Player session could not be refreshed and expired.");
        };
    }
    /*
     * Populate a Dictionary<string,object> with the arguments and invoke the script.
     * Deserialize the response into a CloudCodeResponse object
     */
    public async void OnClick()
    {
        var arguments = new Dictionary<string, object> { { "name", "Unity" } };
        var response = await CloudCodeService.Instance.CallEndpointAsync<CloudCodeResponse>("testScript01", arguments);
        Debug.Log(response.welcomeMessage);
    }
}