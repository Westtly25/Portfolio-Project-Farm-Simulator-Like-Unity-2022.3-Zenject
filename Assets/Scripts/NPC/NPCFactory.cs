using Zenject;
using UnityEngine;

public class NPCFactory : IFactory<Vector2Int, NPC>
{
    private readonly DiContainer container;

    [Inject]
    public NPCFactory(DiContainer container)
    {
        this.container = container;
    }

    public NPC Create(Vector2Int param)
    {
        throw new System.NotImplementedException();
    }
}