using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking; 
using System.Text.RegularExpressions;
using System.IO;
using System.Collections;


public class RiddleLoader : MonoBehaviour
{
    private Dictionary<int, List<(string riddle, string answer)>> riddles = new Dictionary<int, List<(string, string)>>();
    public int level = 1;
    private bool riddlesLoaded = false;

    void Start()
    {
        StartCoroutine(LoadRiddles());
    }

    private IEnumerator LoadRiddles()
    {
        string path;

        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            path = Application.streamingAssetsPath + "/riddles.csv";
            UnityWebRequest request = UnityWebRequest.Get(path);
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Failed to load CSV in WebGL: " + request.error);
                yield break;
            }

            ParseCSV(request.downloadHandler.text);
        }
        else
        {
            path = Application.dataPath + "/Resources/riddles.csv";
            if (!File.Exists(path))
            {
                Debug.LogError("Riddle CSV file not found!");
                yield break;
            }

            string csvData = File.ReadAllText(path);
            ParseCSV(csvData);
        }
    }

    private void ParseCSV(string csvData)
    {
        riddlesLoaded = false;
        string[] lines = csvData.Split('\n');

        foreach (string line in lines)
        {
            string[] columns = Regex.Split(line, ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");

            if (columns.Length < 3)
            {
                Debug.LogError($"Malformed CSV row: {line}");
                continue;
            }

            string riddle = columns[0].Trim().Trim('"');
            string answer = columns[1].Trim().Trim('"');
            int level;

            if (!int.TryParse(columns[2].Trim(), out level))
            {
                Debug.LogError($"Invalid level format in CSV: {columns[2]}");
                continue;
            }

            if (!riddles.ContainsKey(level))
                riddles[level] = new List<(string, string)>();

            riddles[level].Add((riddle, answer));
        }
        riddlesLoaded = true;
    }

    public (string, string) GetRiddle()
    {
        if (!riddlesLoaded)
            return ("Riddles not yet loaded", "");


        if (riddles.ContainsKey(level) && riddles[level].Count > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, riddles[level].Count);
            return riddles[level][randomIndex];
        }
        return ("No riddle found", "");
    }
}

