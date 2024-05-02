using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRight : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 2.5f;

    void Update()
    {
        transform.Translate(Vector3.right * _moveSpeed * Time.deltaTime);
    }
}
