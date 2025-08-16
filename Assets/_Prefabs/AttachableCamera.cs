using System.Collections;
using UnityEngine;

public class AttachableCamera : MonoBehaviour
{
    private Transform _target;

    [SerializeField]
    private float _followSpeed;

    public void AttachTo(Transform target)
    {
        _target = target;
        StartCoroutine(CoFollow());
    }

    public IEnumerator CoFollow()
    {
        var zPos = transform.position.z;
        while (_target != null)
        {
            var position = Vector3.Lerp(transform.position, _target.position, Time.deltaTime * _followSpeed);
            position.z = zPos;
            transform.position = position;
            yield return null;
        }
    }
}
