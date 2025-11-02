using NUnit.Framework.Internal.Commands;
using UC;
using UnityEngine;

public class ChangeWeapon : MonoBehaviour
{
    [SerializeField] private UnityEngine.InputSystem.PlayerInput playerInput;
    [SerializeField, InputPlayer(nameof(playerInput))] private UC.InputControl changeInput;

    private void Awake()
    {
        changeInput.playerInput = playerInput;
    }

    private void Update()
    {
        if (changeInput.IsDown())
        {
            PlayerStats player = GetComponent<PlayerStats>();
            Shooting arma = GetComponent<Shooting>();
            arma.ChangeWeaponType();
            Debug.Log($"{player.Name} Mudou de arma, atual: {arma.Weapon.ToString()}");
        }   
    }
}
