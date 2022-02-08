using System;
using DG.Tweening;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    private Transform _target;
    private Vector3 _startPosition;

    private void Start()
    {
        _startPosition = transform.position;
    }

    public void Update()
    {
        if (_target != null)
        {
            if (_target.position.y > transform.position.y)
            {
                transform.position = new Vector3(transform.position.x, _target.position.y, transform.position.z);
            }
        }
    }

    public void Restart()
    {
        transform.position = _startPosition;
    }

    public void BindTarget(Transform target)
    {
        _target = target;
    }
}