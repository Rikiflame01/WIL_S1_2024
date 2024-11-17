using UnityEngine;
using TMPro;
using Unity.Services.Core;
using Unity.Services.CloudSave;
using System.Collections.Generic;
using System.Threading.Tasks;

public class UserManager : MonoBehaviour
{
    public TMP_Text welcomeText;

    private void Start()
    {
        InitializeUnityServices();
    }

    private async void InitializeUnityServices()
    {
        await UnityServices.InitializeAsync();
        Debug.Log("Unity Services Initialized");
    }

    public async Task SaveUserData(string playerName)
    {
        try
        {
            var data = new Dictionary<string, object>
            {
                { "playerName", playerName }
            };
            await CloudSaveService.Instance.Data.ForceSaveAsync(data);
            Debug.Log($"User data saved successfully: {playerName}");
        }
        catch (CloudSaveException ex)
        {
            Debug.LogError($"Failed to save user data: {ex.Message}");
        }
    }

    public async Task RetrieveUserData()
    {
        try
        {
            var keys = new HashSet<string> { "playerName" };
            var data = await CloudSaveService.Instance.Data.LoadAsync(keys);

            if (data.ContainsKey("playerName"))
            {
                string playerName = data["playerName"].ToString();
                welcomeText.text = "Hello " + playerName;
                Debug.Log($"User data retrieved successfully: {playerName}");
            }
            else
            {
                welcomeText.text = "Hello Player";
                Debug.LogWarning("Player name not found in Cloud Save data");
            }
        }
        catch (CloudSaveException ex)
        {
            Debug.LogError($"Failed to retrieve user data: {ex.Message}");
        }
    }
}
