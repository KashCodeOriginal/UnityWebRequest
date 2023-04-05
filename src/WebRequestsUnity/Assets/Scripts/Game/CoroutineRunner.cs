using System.Collections;
using UnityEngine;

namespace Game
{
    public interface ICoroutineRunner
    {
        public Coroutine StartCoroutine(IEnumerator coroutine);
    }
}

