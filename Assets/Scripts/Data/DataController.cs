using System.Collections.Generic;
using UnityEngine;

public class DataController : MonoBehaviour
{

    public static SaveData currentSessionData;

    public void Init()
    {
        Subscribe();
        LoadData();
    }

    public void SaveGame()
    {
        var dataToSave = JsonUtility.ToJson(currentSessionData);
        PlayerPrefs.SetString("Savedata", dataToSave);
    }

    public void LoadData()
    {
        if (PlayerPrefs.HasKey("Savedata"))
        {
            var dataToLoad = PlayerPrefs.GetString("Savedata");
            currentSessionData = JsonUtility.FromJson<SaveData>(dataToLoad);
        } else
        {
            currentSessionData.UsableBalls = new List<BallStats>();
            BallCreator.CreateBallData("Basic", 3f, 0.8f, Color.white);
        }
    }

    private void Subscribe() => BallCreator.OnBallCreation += OnNewBallCreation;
    private void Unsubscribe() => BallCreator.OnBallCreation -= OnNewBallCreation;

    void OnNewBallCreation(BallStats stats)
    {
        currentSessionData.UsableBalls.Add(stats);
        currentSessionData.CurrentBallActivated = stats;
    }

    private void OnDestroy() => Unsubscribe();

    private void OnApplicationQuit() => SaveGame();

    private void OnApplicationFocus(bool focus)
    {
        if (!focus) SaveGame();
    }
}
