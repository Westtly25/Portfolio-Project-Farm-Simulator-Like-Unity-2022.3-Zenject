using UnityEngine;

public class GameManager : SingletonMonobehaviour<GameManager>
{
    public Weather currentWeather;

    protected override void Awake()
    {
        base.Awake();

        //TODO: Need a resolution settings options screen
        Screen.SetResolution(1920, 1080, FullScreenMode.FullScreenWindow, 0);

        // Set starting weather
        currentWeather = Weather.dry;
    }
}
