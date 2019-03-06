using UnityEngine;
using System.Collections;

public abstract class ExecutableComponent : MonoBehaviour
{
    public abstract void Execute(object args = null);
}
