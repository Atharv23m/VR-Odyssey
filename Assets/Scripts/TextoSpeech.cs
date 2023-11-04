using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Amazon;
using Amazon.Polly;
using Amazon.Runtime;
using System.IO;
using System.Threading.Tasks;
using Amazon.Polly.Model;
using UnityEngine.Networking;
public class textToSpeech : MonoBehaviour
{
    [SerializeField] private AudioSource audiosource;
    public GoogleLanguageAPIRequest script;
    // Start is called before the first frame update

    async void Say()
    {
        var credentials = new BasicAWSCredentials(accessKey: "AKIAZS6VDU3X6VNBS2YD", secretKey: "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");
        var client = new AmazonPollyClient(credentials, Amazon.RegionEndpoint.APSouth1);
        var request = new SynthesizeSpeechRequest()
        {
            Text = script.result,
            Engine = Engine.Neural,
            VoiceId = VoiceId.Amy,
            OutputFormat = OutputFormat.Mp3
        };
        var response = await client.SynthesizeSpeechAsync(request);
        writeIntoFile(response.AudioStream);
        using (var www = UnityWebRequestMultimedia.GetAudioClip(uri: $"{Application.persistentDataPath}/audio.mp3", AudioType.MPEG))
        {
            var op = www.SendWebRequest();
            while (!op.isDone) await Task.Yield();
            var clip = DownloadHandlerAudioClip.GetContent(www);
            audiosource.clip = clip;
            audiosource.Play();

        }
    }

    private void FixedUpdate()
    {
        if (script.result != "error")
        {
            Say();
            script.result = "error";
        }
    }
    // Update is called once per frame
    private void writeIntoFile(Stream stream)
    {
        using (var filestream = new FileStream(path: $"{Application.persistentDataPath}/audio.mp3", FileMode.Create))
        {
            byte[] buffer = new byte[24];
            int bytesRead;
            while ((bytesRead = stream.Read(buffer, offset: 0, count: buffer.Length)) > 0)
            {
                filestream.Write(buffer, offset: 0, count: buffer.Length);
            }
        }
    }
}
