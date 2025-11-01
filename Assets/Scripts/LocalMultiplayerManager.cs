using UnityEngine;
using UnityEngine.InputSystem;

public class LocalMultiplayerManager : MonoBehaviour
{
    public static LocalMultiplayerManager instance;

    [SerializeField] private GameObject playerPrefab;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        // Cria dois jogadores locais usando o PlayerInputManager
        if (PlayerInputManager.instance == null)
        {
            Debug.LogError("Adicione um PlayerInputManager na cena!");
            return;
        }
    }
}
