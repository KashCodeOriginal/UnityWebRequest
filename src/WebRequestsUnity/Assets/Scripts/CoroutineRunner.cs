using System.Collections;
using UnityEngine;

public interface ICoroutineRunner
{
    public Coroutine StartCoroutine(IEnumerator coroutine);
}

