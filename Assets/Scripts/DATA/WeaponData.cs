using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Vampire Survivors/Weapon Item Data")]
public class WeaponItemData : ScriptableObject
{
    [Header("기본 정보")]
    public string Name;                 // 영어 이름
    public string Korean_Name;          // 한글 이름

    [Header("데미지")]
    public float Base_Damage_Min;
    public float Base_Damage_Max;

    [Header("쿨타임")]
    public float Cooldown_Min;
    public float Cooldown_Max;

    [Header("수량 (Amount)")]
    public int Amount_Min;
    public int Amount_Max;

    // 필요하면 여기에 더 추가 가능
    // public float Area_Min;
    // public float Area_Max;
    // public float Duration_Min;
    // public float Duration_Max;
    // public int Pierce_Min;
    // public int Pierce_Max;
}