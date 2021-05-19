using UnityEngine;


/// <summary>
/// If D key is pressed, change player double leg
/// </summary>
[RequireComponent(typeof(PlayerController))]
public class ChangeDoubleLeg : MonoBehaviour
{
    public KeyCode key = KeyCode.D;
    PlayerController player;

    private void Start()
    {
        player = GetComponent<PlayerController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(key))
            player.doubleLeg = !player.doubleLeg;
    }
}
