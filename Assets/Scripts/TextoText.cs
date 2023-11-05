using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GoogleLanguageAPIRequest : MonoBehaviour
{
    [System.Serializable]
    public class SafetyRating
    {
        public string category;
        public string probability;
    }

    [System.Serializable]
    public class Candidate
    {
        public string output;
        public SafetyRating[] safetyRatings;
    }

    [System.Serializable]
    public class JsonData
    {
        public Candidate[] candidates;
    }

    public string prom = "";
    public string result = "error";
    public Text Prompt;
    private const string apiKey = "AIzaSyDQogd1EAVXFgXwZaBA5hTMxul3KWl8URA"; // Replace with your actual API key
    private const string url = "https://generativelanguage.googleapis.com/v1beta2/models/text-bison-001:generateText?key=" + apiKey;

    public void OnClick() // This function will be called on button click
    {
        // Create a custom JSON request data string based on 'prom'
        string limit = "Answer in not more than 50 words";
        string requestData = $"{{\"prompt\": {{\"text\": \"{prom + limit}\"}}}}";

        // Set up the UnityWebRequest
        var request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = new System.Text.UTF8Encoding().GetBytes(requestData);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        // Send the request
        request.SendWebRequest().completed += OnRequestCompleted;
    }

    private void OnRequestCompleted(AsyncOperation operation)
    {
        var request = ((UnityWebRequestAsyncOperation)operation).webRequest;

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.LogError(request.error);
        }
        else
        {
            Debug.Log(request.downloadHandler.text);

            // Request was successful, and the response is in request.downloadHandler.text
            string jsonText = request.downloadHandler.text; // Replace with your JSON text

            // Deserialize the JSON
            JsonData jsonData = JsonUtility.FromJson<JsonData>(jsonText);

            // Access the "output" from the first candidate
            result = jsonData.candidates[0].output;
            Prompt.text = jsonData.candidates[0].output;
            /*            print(result);
            */            // Now, 'output' contains the value "48°51?29?N 2°17?40?E"
        }
    }
    private void Update()
    {
        prom = Prompt.text;
    }
}
