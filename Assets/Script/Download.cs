using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.UI;
#if UNITY_ANDROID
using UnityEngine.Android;
#endif

public class DownloadImageOnline : MonoBehaviour
{
    public string imageUrl = "https://raw.githubusercontent.com/Pratham9911/InfinityAR/main/InfinityAR.png";
    public string imageFileName = "InfinityAR.png";

    public TextMeshProUGUI messageText;
    public Image popupImage;

    public void DownloadImage()
    {
#if UNITY_ANDROID
        if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite))
        {
            Permission.RequestUserPermission(Permission.ExternalStorageWrite);
        }
#endif
        StartCoroutine(DownloadImageCoroutine());
    }

    private IEnumerator DownloadImageCoroutine()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(imageUrl))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                byte[] imageData = request.downloadHandler.data;

                string targetPath = "";

#if UNITY_STANDALONE_WIN || UNITY_EDITOR
                string downloadsFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.UserProfile) + "/Downloads";
                targetPath = Path.Combine(downloadsFolder, imageFileName);
#elif UNITY_ANDROID
                string downloadsPath = "/storage/emulated/0/Download";
                targetPath = Path.Combine(downloadsPath, imageFileName);
#else
                targetPath = Path.Combine(Application.persistentDataPath, imageFileName);
#endif

                try
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(targetPath));
                    File.WriteAllBytes(targetPath, imageData);
                    ShowMessage("downloaded to InternalStorage/Downloads", true);
                }
                catch (IOException e)
                {
                    Debug.LogError("Saving failed: " + e.Message);
                    ShowMessage("Failed to save image", false);
                }
            }
            else
            {
                Debug.LogError("Download failed: " + request.error);
                ShowMessage("No internet or download failed", false);
            }
        }
    }

    private void ShowMessage(string text, bool showImage)
    {
        if (messageText != null)
        {
            messageText.text = text;
            messageText.alpha = 1f;
        }

        if (popupImage != null)
        {
            popupImage.color = new Color(popupImage.color.r, popupImage.color.g, popupImage.color.b, showImage ? 1f : 0f);
        }

        Invoke("HideMessage", 5f);
    }

    private void HideMessage()
    {
        if (messageText != null) messageText.alpha = 0f;
        if (popupImage != null) popupImage.color = new Color(popupImage.color.r, popupImage.color.g, popupImage.color.b, 0f);
    }
}
