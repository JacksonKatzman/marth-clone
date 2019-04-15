using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Authentication_Handler : MonoBehaviour {

	public Text userNameInput, passwordInput, displayNameInput;
	public GameObject newAccountButton, logInButton, registerButton, backButton;

	public void NewAccountButton()
	{
		displayNameInput.gameObject.transform.parent.gameObject.SetActive(true);
		registerButton.SetActive(true);
		logInButton.SetActive(false);
		backButton.SetActive(true);
	}

	public void BackButton()
	{
		displayNameInput.gameObject.transform.parent.gameObject.SetActive(false);
		registerButton.SetActive(false);
		logInButton.SetActive(true);
		backButton.SetActive(false);
	}

	public void RegisterPlayerButton()
	{
		//TODO: Use Nakama to make a registration of account request using inputs from textboxes
	}

	public void AuthenticateLogInButton()
	{
		//TODO: Use Nakama to make a auth of account request using inputs from textboxes
		//TODO: Have the sucessful auth tell the SceneManager to switch scenes as shown below
		SceneManager.LoadScene("MainMenu");
	}
}
