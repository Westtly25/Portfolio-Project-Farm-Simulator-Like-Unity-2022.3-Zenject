using Zenject;

public class ItemPickUpFactory
{
    private readonly DiContainer container;

    [Inject]
    public ItemPickUpFactory(DiContainer container)
    {
        this.container = container;
    }
}
