using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackerInterface
{
    /// <summary>
    /// Makes the entity attack
    /// </summary>
    IEnumerator Attack();
}
