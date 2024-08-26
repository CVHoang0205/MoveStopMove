using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstracCharacter : MonoBehaviour
{
    public abstract void Oninit();
    public abstract void OnAttack();

    public abstract void OnDeath();

}
