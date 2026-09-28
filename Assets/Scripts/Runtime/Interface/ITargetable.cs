using UnityEngine;

public interface ITargetable
{
    public int HP { get; set; }
    public int DEF { get; set; }

    public void TakeDamage();

}
