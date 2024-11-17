using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.Services.Authentication;
using Unity.Services.Core;
using System.Threading.Tasks;

public class LoginManager : MonoBehaviour
{
    [Header("Sign-Up UI")]
    public Canvas signUpCanvas;
    public TMP_InputField signUpUsernameInput;
    public TMP_InputField signUpPasswordInput;
    public Button signUpButton;
    public TMP_Text signUpFeedbackText;

    [Header("Sign-In UI")]
    public Canvas signInCanvas;
    public TMP_InputField signInUsernameInput;
    public TMP_InputField signInPasswordInput;
    public Button signInButton;
    public TMP_Text signInFeedbackText;

    [Header("Main Canvas")]
    public Canvas mainCanvas;

    private UserManager userManager;

    private void Start()
    {
        InitializeUnityServices();

        signUpButton.onClick.AddListener(OnSignUpButtonClicked);
        signInButton.onClick.AddListener(OnSignInButtonClicked);

        userManager = FindObjectOfType<UserManager>();
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

            await userManager.SaveUserData(username);

            await userManager.RetrieveUserData();

            CanvasToggle.Instance.DisableCanvas(signUpCanvas);
            CanvasToggle.Instance.EnableCanvas(mainCanvas);
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

            await userManager.RetrieveUserData();

            CanvasToggle.Instance.DisableCanvas(signInCanvas);
            CanvasToggle.Instance.EnableCanvas(mainCanvas);
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
}
