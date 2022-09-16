using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stage
{
    public Theme themeBackGound;
    public List<MonsterBase> stageMonster = new List<MonsterBase>();
}
