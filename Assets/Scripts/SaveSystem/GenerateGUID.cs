using UnityEngine;

[ExecuteAlways]
public sealed class GenerateGUID : MonoBehaviour
{
    [SerializeField]
    private string guid = "";

    public string GUID
    {
        get => guid;
        private set => guid = value;
    }

    private void Awake()
    {
        if (!Application.IsPlaying(gameObject))
        {
            if (guid == "")
            {
                guid = System.Guid.NewGuid().ToString();
            }
        }
    }
}
