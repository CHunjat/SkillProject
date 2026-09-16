using UnityEngine;

public interface Iskill
{
    string skillname { get; }

    float Cooldown { get; }


    void Execut(GameObject caster);
}
