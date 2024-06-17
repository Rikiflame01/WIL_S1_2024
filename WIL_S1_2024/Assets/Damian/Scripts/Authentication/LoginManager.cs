using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.Services.Authentication;
using Unity.Services.Core;
using System.Threading.Tasks;
using System.Collections.Generic;
using Unity.Services.CloudSave;

public class LoginManager : MonoBehaviour
{
    [Header("Sign-Up UI")]
    public GameObject signUpCanvas;
    public TMP_InputField signUpUsernameInput;
    public TMP_InputField signUpPasswordInput;
    public Button signUpButton;
    public TMP_Text signUpFeedbackText;

    [Header("Sign-In UI")]
    public GameObject signInCanvas;
    public TMP_InputField signInUsernameInput;
    public TMP_InputField signInPasswordInput;
    public Button signInButton;
    public TMP_Text signInFeedbackText;

    [Header("Main Canvas")]
    public GameObject mainCanvas;

    private void Start()
    {
        // Initialize Unity Services
        InitializeUnityServices();

        // Add listeners to buttons
        signUpButton.onClick.AddListener(OnSignUpButtonClicked);
        signInButton.onClick.AddListener(OnSignInButtonClicked);
    }

    private async void InitializeUnityServices()
    {
        await UnityServices.InitializeAsync();
        Debug.Log("Unity Services Initialized");
    }

    private void OnSignUpButtonClicked()
    {
        string username = signUpUsernameInput.text;
        string password = signUpPasswordInput.text;
        SignUpWithUsernamePasswordAsync(username, password);
    }

    private void OnSignInButtonClicked()
    {
        string username = signInUsernameInput.text;
        string password = signInPasswordInput.text;
        SignInWithUsernamePasswordAsync(username, password);
    }

    private async Task SignUpWithUsernamePasswordAsync(string username, string password)
    {
        try
        {
            await AuthenticationService.Instance.SignUpWithUsernamePasswordAsync(username, password);
            signUpFeedbackText.text = "Sign-Up is successful.";
            Debug.Log("Sign-Up is successful.");

            // Save initial user data
            await SaveUserData(username);

            // Show the sign-in canvas and hide the sign-up canvas
            ShowSignInCanvas();
        }
        catch (AuthenticationException ex)
        {
            signUpFeedbackText.text = $"Sign-Up Failed: {ex.Message}";
            Debug.LogException(ex);
        }
        catch (RequestFailedException ex)
        {
            signUpFeedbackText.text = $"Sign-Up Failed: {ex.Message}";
            Debug.LogException(ex);
        }
    }

    private async Task SignInWithUsernamePasswordAsync(string username, string password)
    {
        try
        {
            await AuthenticationService.Instance.SignInWithUsernamePasswordAsync(username, password);
            signInFeedbackText.text = "Sign-In is successful.";
            Debug.Log("Sign-In is successful.");

            // Show the main canvas and hide the sign-in canvas
            ShowMainCanvas();
        }
        catch (AuthenticationException ex)
        {
            signInFeedbackText.text = $"Sign-In Failed: {ex.Message}";
            Debug.LogException(ex);
        }
        catch (RequestFailedException ex)
        {
            signInFeedbackText.text = $"Sign-In Failed: {ex.Message}";
            Debug.LogException(ex);
        }
    }

    public void ShowSignInCanvas()
    {
        signUpCanvas.SetActive(false);
        signInCanvas.SetActive(true);
        mainCanvas.SetActive(false);
    }

    public void ShowSignUpCanvas()
    {
        signUpCanvas.SetActive(true);
        signInCanvas.SetActive(false);
        mainCanvas.SetActive(false);
    }

    private void ShowMainCanvas()
    {
        signUpCanvas.SetActive(false);
        signInCanvas.SetActive(false);
        mainCanvas.SetActive(true);
    }

    private async Task SaveUserData(string username)
    {
        try
        {
            var data = new Dictionary<string, object>
            {
                { "username", username },
                { "signupTime", System.DateTime.UtcNow.ToString() }
            };
            await CloudSaveService.Instance.Data.ForceSaveAsync(data);
            Debug.Log("User data saved successfully");
        }
        catch (CloudSaveException ex)
        {
            Debug.LogError($"Failed to save user data: {ex.Message}");
        }
    }
}
