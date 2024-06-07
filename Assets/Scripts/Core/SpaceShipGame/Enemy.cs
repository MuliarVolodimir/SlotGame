using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int _reward;

    private void OnDestroy()
    {
        var controller = FindObjectOfType<SpaceShipGameController>();
        if (controller != null)
        {
            controller.UpdateScore(_reward);
        }
    }
}
