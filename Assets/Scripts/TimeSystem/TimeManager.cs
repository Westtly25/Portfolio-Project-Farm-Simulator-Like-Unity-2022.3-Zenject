﻿using System;
using Zenject;
using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.Pause_System;

[Serializable]
public sealed class TimeManager : ISaveable, IPauseListener, ITickable, IInitializable, IDisposable
{
    private Season gameSeason = Season.Spring;
    private int gameYear = 1;
    private int gameDay = 1;
    private int gameHour = 6;
    private int gameMinute = 30;
    private int gameSecond = 0;
    private string gameDayOfWeek = "Mon";

    private DayOfWeek day;

    private float gameTick = 0f;

    private string iSaveableUniqueID;
    public string ISaveableUniqueID
    {
        get => iSaveableUniqueID;
        set => iSaveableUniqueID = value;
    }

    private GameObjectSave gameObjectSave;
    public GameObjectSave GameObjectSave
    {
        get => gameObjectSave;
        set => gameObjectSave = value;
    }

    private readonly IPauseHandler pauseHandler;

    [Inject]
    public TimeManager(IPauseHandler pauseHandler)
    {
        this.pauseHandler = pauseHandler;
    }

    public void Initialize()
    {
        ISaveableUniqueID = Guid.NewGuid().ToString();
        GameObjectSave = new GameObjectSave();

        ISaveableRegister();

        EventHandler.CallAdvanceGameMinuteEvent(gameYear, gameSeason, gameDay, gameDayOfWeek, gameHour, gameMinute, gameSecond);
    }

    public void Dispose()
    {
        ISaveableDeregister();
    }

    public void Tick()
    {
        if (pauseHandler.IsPaused)
            return;

            GameTick();
    }

    private void GameTick()
    {
        gameTick += Time.deltaTime;

        if (gameTick >= StaticData.secondsPerGameSecond)
        {
            gameTick -= StaticData.secondsPerGameSecond;

            UpdateGameSecond();
        }
    }

    private void UpdateGameSecond()
    {
        gameSecond++;

        if (gameSecond > 59)
        {
            gameSecond = 0;
            gameMinute++;

            if (gameMinute > 59)
            {
                gameMinute = 0;
                gameHour++;

                if (gameHour > 23)
                {
                    gameHour = 0;
                    gameDay++;

                    if (gameDay > 30)
                    {
                        gameDay = 1;

                        int gs = (int)gameSeason;
                        gs++;

                        gameSeason = (Season)gs;

                        if (gs > 3)
                        {
                            gs = 0;
                            gameSeason = (Season)gs;

                            gameYear++;

                            if (gameYear > 9999)
                                gameYear = 1;


                            EventHandler.CallAdvanceGameYearEvent(gameYear, gameSeason, gameDay, gameDayOfWeek, gameHour, gameMinute, gameSecond);
                        }

                        EventHandler.CallAdvanceGameSeasonEvent(gameYear, gameSeason, gameDay, gameDayOfWeek, gameHour, gameMinute, gameSecond);
                    }

                    gameDayOfWeek = GetDayOfWeek();
                    EventHandler.CallAdvanceGameDayEvent(gameYear, gameSeason, gameDay, gameDayOfWeek, gameHour, gameMinute, gameSecond);
                }

                EventHandler.CallAdvanceGameHourEvent(gameYear, gameSeason, gameDay, gameDayOfWeek, gameHour, gameMinute, gameSecond);
            }

            EventHandler.CallAdvanceGameMinuteEvent(gameYear, gameSeason, gameDay, gameDayOfWeek, gameHour, gameMinute, gameSecond);

        }
    }

    private string GetDayOfWeek()
    {
        int totalDays = (((int)gameSeason) * 30) + gameDay;
        int dayOfWeek = totalDays % 7;

        return dayOfWeek switch
        {
            1 => "Mon",
            2 => "Tue",
            3 => "Wed",
            4 => "Thu",
            5 => "Fri",
            6 => "Sat",
            0 => "Sun",
            _ => "",
        };
    }

    public TimeSpan GetGameTime() =>
        new TimeSpan(gameHour, gameMinute, gameSecond);

    public void ISaveableRegister() =>
        SaveLoadManager.Instance.iSaveableObjectList.Add(this);
    public void ISaveableDeregister() =>
        SaveLoadManager.Instance.iSaveableObjectList.Remove(this);

    public GameObjectSave ISaveableSave()
    {
        GameObjectSave.sceneData.Remove(StaticData.PersistentScene);

        SceneSave sceneSave = new SceneSave();

        sceneSave.intDictionary = new Dictionary<string, int>();

        sceneSave.stringDictionary = new Dictionary<string, string>();

        sceneSave.intDictionary.Add("gameYear", gameYear);
        sceneSave.intDictionary.Add("gameDay", gameDay);
        sceneSave.intDictionary.Add("gameHour", gameHour);
        sceneSave.intDictionary.Add("gameMinute", gameMinute);
        sceneSave.intDictionary.Add("gameSecond", gameSecond);

        sceneSave.stringDictionary.Add("gameDayOfWeek", gameDayOfWeek);
        sceneSave.stringDictionary.Add("gameSeason", gameSeason.ToString());

        GameObjectSave.sceneData.Add(StaticData.PersistentScene, sceneSave);

        return GameObjectSave;
    }

    public void ISaveableLoad(GameSave gameSave)
    {
        if (gameSave.gameObjectData.TryGetValue(ISaveableUniqueID, out GameObjectSave gameObjectSave))
        {
            GameObjectSave = gameObjectSave;

            if (GameObjectSave.sceneData.TryGetValue(StaticData.PersistentScene, out SceneSave sceneSave))
            {
                if (sceneSave.intDictionary != null && sceneSave.stringDictionary != null)
                {
                    if (sceneSave.intDictionary.TryGetValue("gameYear", out int savedGameYear))
                        gameYear = savedGameYear;

                    if (sceneSave.intDictionary.TryGetValue("gameDay", out int savedGameDay))
                        gameDay = savedGameDay;

                    if (sceneSave.intDictionary.TryGetValue("gameHour", out int savedGameHour))
                        gameHour = savedGameHour;

                    if (sceneSave.intDictionary.TryGetValue("gameMinute", out int savedGameMinute))
                        gameMinute = savedGameMinute;

                    if (sceneSave.intDictionary.TryGetValue("gameSecond", out int savedGameSecond))
                        gameSecond = savedGameSecond;

                    if (sceneSave.stringDictionary.TryGetValue("gameDayOfWeek", out string savedGameDayOfWeek))
                        gameDayOfWeek = savedGameDayOfWeek;

                    if (sceneSave.stringDictionary.TryGetValue("gameSeason", out string savedGameSeason))
                    {
                        if (Enum.TryParse<Season>(savedGameSeason, out Season season))
                        {
                            gameSeason = season;
                        }
                    }

                    gameTick = 0f;

                    EventHandler.CallAdvanceGameMinuteEvent(gameYear, gameSeason, gameDay, gameDayOfWeek, gameHour, gameMinute, gameSecond);
                }
            }
        }
    }

    public void ISaveableStoreScene(string sceneName)
    {
    }

    public void ISaveableRestoreScene(string sceneName)
    {
    }

    public void Pause(bool isPaused)
    {
    }
}