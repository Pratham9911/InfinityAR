using UnityEngine;
using TMPro;  // TextMeshPro support
using SQLite;
using System;
using System.IO;
using System.Collections.Generic;

public class EventDatabase : MonoBehaviour
{
    // References to three different TextMeshPro UI elements for the events
    public TMP_Text eventText1;
    public TMP_Text eventText2;
    public TMP_Text eventText3;

    private string databasePath;
    private SQLiteConnection db;

    void Start()
    {
        SetupDatabase();
        GetTodaysEvents();
    }

    void SetupDatabase()
    {
        // Use StreamingAssets as a common path
        string streamingAssetsPath = Path.Combine(Application.streamingAssetsPath, "events.db");

#if UNITY_EDITOR
        databasePath = streamingAssetsPath; // Direct access in Unity Editor
#elif UNITY_ANDROID
            databasePath = Path.Combine(Application.persistentDataPath, "events.db"); // Must copy on Android
            if (!File.Exists(databasePath)) // Copy database if not already copied
            {
                CopyDatabaseFromStreamingAssets(streamingAssetsPath, databasePath);
            }
#endif

        // Debug log to check where the database is being read from
        Debug.Log("Database Path: " + databasePath);

        // Open the existing database
        db = new SQLiteConnection(databasePath);
    }

    void CopyDatabaseFromStreamingAssets(string sourcePath, string destinationPath)
    {
        if (sourcePath.Contains("://")) // Android requires UnityWebRequest
        {
            StartCoroutine(DownloadDatabase(sourcePath, destinationPath));
        }
        else if (File.Exists(sourcePath))
        {
            File.Copy(sourcePath, destinationPath);
        }
    }

    System.Collections.IEnumerator DownloadDatabase(string sourcePath, string destinationPath)
    {
        using (UnityEngine.Networking.UnityWebRequest request = UnityEngine.Networking.UnityWebRequest.Get(sourcePath))
        {
            yield return request.SendWebRequest();
            if (request.result == UnityEngine.Networking.UnityWebRequest.Result.Success)
            {
                File.WriteAllBytes(destinationPath, request.downloadHandler.data);
            }
        }
    }

    void GetTodaysEvents()
    {
        int day = DateTime.Now.Day;
        int month = DateTime.Now.Month;

        Debug.Log($"Fetching events for date: {day}/{month}");

        try
        {
            List<HistoricalEvent> events = db.Query<HistoricalEvent>(
                "SELECT * FROM historical_events WHERE Day = ? AND Month = ?", day, month
            );

            // Clear the previous text from the TMP_Text components
            eventText1.text = "";
            eventText2.text = "";
            eventText3.text = "";

            // Check if any events were found
            if (events.Count == 0)
            {
                eventText1.text = "No events found for today.";
            }
            else
            {
                // Display up to 3 events
                for (int i = 0; i < events.Count && i < 3; i++)
                {
                    string eventInfo = $"{events[i].Day}/{events[i].Month}/{events[i].Year}: {events[i].Description}";

                    if (i == 0)
                    {
                        eventText1.text = eventInfo;
                    }
                    else if (i == 1)
                    {
                        eventText2.text = eventInfo;
                    }
                    else if (i == 2)
                    {
                        eventText3.text = eventInfo;
                    }
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Database Query Failed: " + e.Message);
            eventText1.text = "Error fetching events.";
        }
    }
}

// Define SQLite table structure (Ensure this matches your DB schema)
public class HistoricalEvent
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public int Day { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
}
