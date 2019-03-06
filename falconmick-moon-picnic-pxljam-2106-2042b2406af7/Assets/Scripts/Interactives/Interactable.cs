using UnityEngine;
using System.Collections;

public abstract class Interactable : MonoBehaviour
{
    public string UniqueName
    {
        get { return GetUniqueName(); }
    }

    protected abstract string GetUniqueName();
    public abstract object Interact(object arg = null);

    public virtual bool CanInteract(object arg = null)
    {
        return true;
    }
}
