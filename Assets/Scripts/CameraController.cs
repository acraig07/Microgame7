using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform _traget;
    public float _lerpSpeed;

    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(_traget.position.x, _traget.position.y, - 10), _lerpSpeed * Time.deltaTime);
    }
}
