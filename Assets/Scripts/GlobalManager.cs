using UnityEngine;

public class GlobalManager : MonoBehaviour
{
    [HideInInspector]
    public static GlobalManager instance;
    [HideInInspector]
    public int playerCnt;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        playerCnt = 0;
    }
}
