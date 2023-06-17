using Zenject;
using UnityEngine;
using Assets.Scripts.Pause_System;

public class NPCFactory : IFactory<Vector2Int, NPC>
{
    private readonly DiContainer container;
    private readonly IPauseHandler pauseHandler;

    [Inject]
    public NPCFactory(DiContainer container, IPauseHandler pauseHandler)
    {
        this.container = container;
        this.pauseHandler = pauseHandler;
    }

    public NPC Create(Vector2Int at)
    {
        throw new System.NotImplementedException();
    }
}