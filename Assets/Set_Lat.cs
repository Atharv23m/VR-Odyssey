using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

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

    private const string apiKey = "AIzaSyDQogd1EAVXFgXwZaBA5hTMxul3KWl8URA"; // Replace with your actual API key
    private const string baseUrl = "https://generativelanguage.googleapis.com/v1beta2/models/text-bison-001:generateText?key=" + apiKey;

    [System.Obsolete]
    private IEnumerator Start()
    {
        // Change "x" to your desired string
        string queryString = "allahbad"; // Replace "your_string" with your specific query

        // Construct the prompt with the dynamic string
        string requestData = "{\"prompt\": {\"text\": \"what are the coordinates of " + queryString + "\"}}";

        // Set up the UnityWebRequest
        var request = new UnityWebRequest(baseUrl, "POST");
        byte[] bodyRaw = new System.Text.UTF8Encoding().GetBytes(requestData);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        // Send the request
        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.LogError(request.error);
        }
        else
        {
            //Debug.Log(request.downloadHandler.text);

            // Request was successful, and the response is in request.downloadHandler.text
            string jsonText = request.downloadHandler.text; // Replace with your JSON text

            // Deserialize the JSON
            JsonData jsonData = JsonUtility.FromJson<JsonData>(jsonText);

            // Access the "output" from the first candidate
            print(jsonData.candidates[0].output);

            // Now, 'output' contains the generated response with the coordinates
        }
    }
}