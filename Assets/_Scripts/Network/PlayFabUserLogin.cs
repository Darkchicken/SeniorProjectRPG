using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using PlayFab;
using PlayFab.ClientModels;

public class PlayFabUserLogin : MonoBehaviour {

    public InputField loginUsernameField;
    public InputField loginPasswordField;
    public Text loginErrorText;
    public Button loginButton;
    public InputField registerUsernameField;
    public InputField registerPasswordField;
    public InputField registerEmailField;
    public Text registerErrorText;
    public Button registerButton;
    public Canvas gameMenu;


    void Start ()
    {
        loginButton.onClick.AddListener(Login);
        registerButton.onClick.AddListener(Register);
	}

    public void Login()
    {
        var loginRequest = new LoginWithPlayFabRequest()
        {
            TitleId = PlayFabSettings.TitleId,
            Username = loginUsernameField.text,
            Password = loginPasswordField.text
        };

        PlayFabClientAPI.LoginWithPlayFab(loginRequest, (result) =>
        {
            LoginRegisterSuccess(result.PlayFabId, result.SessionTicket);
        }, (error) =>
        {
            loginErrorText.text = error.ErrorMessage;
            loginErrorText.gameObject.SetActive(true);
            PlayFabErrorHandler.HandlePlayFabError(error);
        });
    }


    public void Register()
    {
        var request = new RegisterPlayFabUserRequest()
        {
            TitleId = PlayFabSettings.TitleId,
            Username = registerUsernameField.text,
            Password = registerPasswordField.text,
            Email = registerEmailField.text
        };
        PlayFabClientAPI.RegisterPlayFabUser(request, (result) =>
        {
            LoginRegisterSuccess(result.PlayFabId, result.SessionTicket);
        }, (error) =>
        {
            registerErrorText.text = error.ErrorMessage;
            registerErrorText.gameObject.SetActive(true);
            PlayFabErrorHandler.HandlePlayFabError(error);
        });
    }

    private void LoginRegisterSuccess(string PlayFabId, string SessionTicket)
    {
        PlayFabDataStore.playFabId = PlayFabId;
        PlayFabDataStore.sessionTicket = SessionTicket;
        loginErrorText.gameObject.transform.parent.gameObject.SetActive(false);
        gameMenu.gameObject.SetActive(true);

        /* Playfab Photon Token
        var request = new GetPhotonAuthenticationTokenRequest()
        {
            PhotonApplicationId = "67a8e458-b05b-463b-9abe-ce766a75b832".Trim()
        };

        PlayFabClientAPI.GetPhotonAuthenticationToken(request, (result) =>
        {
            Debug.Log("Photon Token Authenticated!");
        },
        (error) =>
        {
            Debug.Log("Photon Token NOT Authenticated!");
        });
        */
    }
}
