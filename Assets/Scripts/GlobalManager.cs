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

    public void ChangePlayersStats(PlayerStats player1, PlayerStats player2)
    {
        int value = player1.Score;
        player1.ChangeScore(player2.Score);
        player2.ChangeScore(value);
    }
}
