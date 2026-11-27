using System;
using UnityEngine;

public class SkillActiveController : MonoBehaviour
{
    public SkillGeneralStatus Skill;
    public event Action<SkillGeneralStatus> ActiveSkill;
    
    public void OnInteract()
    {
        ActiveSkill?.Invoke(Skill);
    }
}
