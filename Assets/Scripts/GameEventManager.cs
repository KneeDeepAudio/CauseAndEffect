using UnityEngine;
using System.Collections;

public static class GameEventManager
{

    public delegate void GameEvent();

    public static event GameEvent GameLaunch, GameReset;

    public static void TriggerGameLaunch()
    {
        if (GameLaunch != null)
        {
            GameLaunch();
        }
    }

    public static void TriggerGameReset()
    {
        if (GameReset != null)
        {
            GameReset();
        }
    }
}
