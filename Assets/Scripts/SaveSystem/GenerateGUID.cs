using UnityEngine;

[ExecuteAlways]
public class GenerateGUID : MonoBehaviour
{
    [SerializeField]
    private string guid = "";

    public string GUID
    {
        get => guid;
        set => guid = value;
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
