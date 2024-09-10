using DG.Tweening;
using UnityEngine;

public class DoorOpenDevice : MonoBehaviour
{
    [SerializeField]
    private Vector3 _deltaPostion;

    [SerializeField]
    private float _animationDuration = 3.0f;

    private bool _isOpen = false;

    private void Operate()
    {
        if (_isOpen)
        {
            Vector3 position = transform.position - _deltaPostion;
            transform.DOMove(position, _animationDuration);
        }
        else
        {
            Vector3 position = transform.position + _deltaPostion;
            transform.DOMove(position, _animationDuration);
        }

        _isOpen = !_isOpen;
    }

    private void Activate()
    {
        if (_isOpen == false)
        {
            Vector3 position = transform.position + _deltaPostion;
            transform.DOMove(position, _animationDuration);
            _isOpen = true;
        }
    }

    private void Deactivate()
    {
        if (_isOpen)
        {
            Vector3 position = transform.position - _deltaPostion;
            transform.DOMove(position, _animationDuration);
            _isOpen = false;
        }
    }
}
