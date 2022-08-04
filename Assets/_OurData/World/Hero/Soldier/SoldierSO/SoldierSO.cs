using UnityEngine;

[CreateAssetMenu(fileName = "New Soldier", menuName = "ScriptableObject/New SoldierSO", order = 0)]
public class SoldierSO : ScriptableObject {
    public string nameSoldier;
    public string description;
    public Sprite image;

    [Header("Base stats")]
    public float atk = 10;
    public float def = 5;
    public float hp = 50;

    public float attackDelay = 2;
    public float attackRange = 5f;

    //Base buff
    [Header("Base buff")]

    public float atkBuffPercent = 0;
    public float defBuffPercent = 0;
    public float hpBuffPercent = 0;

    public float attackDelayBuffPercent = 0;

    [Header("Skill")]

    public Sprite imageActiveSkill;
    public string LocalizationKeyActiveSkill;

}
