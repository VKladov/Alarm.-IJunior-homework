using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]
public class EnterChecker : MonoBehaviour
{
    [SerializeField] private UnityEvent _entered;
    [SerializeField] private UnityEvent _exited;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _entered?.Invoke();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _exited?.Invoke();
    }
}
