using UnityEngine;

public interface ITriggerCheckable
{
    bool isAggroed { get; set; }
    void SetAggroStatus(bool aggroed);

}
