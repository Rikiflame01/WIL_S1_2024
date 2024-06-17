using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.Services.Authentication;
using Unity.Services.Core;
using System.Threading.Tasks;
using System.Collections.Generic;
using Unity.Services.CloudSave;

public class LoginManager : MonoBehaviour
{
    [Header("Sign-Up UI")]
    public TMP_InputField signUpUsernameInput;
    public TMP_InputField signUpPasswordInput;
    public Button signUpButton;
    public TMP_Text signUpFeedbackText;

    [Header("Sign-In UI")]
    public TMP_InputField signInUsernameInput;
    public TMP_InputField signInPasswordInput;
    public Button signInButton;
    public TMP_Text signInFeedbackText;

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

            // Save initial user data or proceed to the next scene
            await SaveUserData(username);

            // Load the next scene
            LoadNextScene();
        }
        catch (AuthenticationException ex)
        {
            // Compare error code to AuthenticationErrorCodes
            // Notify the player with the proper error message
            signUpFeedbackText.text = $"Sign-Up Failed: {ex.Message}";
            Debug.LogException(ex);
        }
        catch (RequestFailedException ex)
        {
            // Compare error code to CommonErrorCodes
            // Notify the player with the proper error message
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

            // Load the next scene
            LoadNextScene();
        }
        catch (AuthenticationException ex)
        {
            // Compare error code to AuthenticationErrorCodes
            // Notify the player with the proper error message
            signInFeedbackText.text = $"Sign-In Failed: {ex.Message}";
            Debug.LogException(ex);
        }
        catch (RequestFailedException ex)
        {
            // Compare error code to CommonErrorCodes
            // Notify the player with the proper error message
            signInFeedbackText.text = $"Sign-In Failed: {ex.Message}";
            Debug.LogException(ex);
        }
    }

    private void LoadNextScene()
    {
        // Load the next scene, for example, "MainScene"
        SceneManager.LoadScene("MainMenu");
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
