using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public List<Skills> skills = new List<Skills>();

    public void AddSkill(Skills skill)
    {
        skills.Add(skill);
    }
}
