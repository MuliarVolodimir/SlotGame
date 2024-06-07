using System;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] MoveVector _moveVector;
    [SerializeField] float _lifeTime;

    public enum MoveVector
    {
        Up,
        Down
    }

    private void Start()
    {
        Destroy(gameObject, _lifeTime);
    }

    private void Update()
    {
        Moving();
    }

    private void Moving()
    {
        int verticalInput = 0;

        switch (_moveVector)
        {
            case MoveVector.Up:
                verticalInput = 1;
                break;
            case MoveVector.Down:
                verticalInput = -1;
                break;
            default:
                break;
        }

        Vector3 movement = new Vector2(0f, verticalInput) * _speed * Time.deltaTime;
        transform.Translate(movement);
    }
}
