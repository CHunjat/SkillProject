#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

public class WeaponDataCreatorWindow : EditorWindow
{
    private TextAsset jsonFile;
    private string saveFolder = "Assets/Data/WeaponData";
    private Vector2 scrollPos;
    private string logMessage = "";
    private MessageType logType = MessageType.Info;

    [MenuItem("Tools/Vampire Survivors/Weapon Data Creator")]
    public static void ShowWindow()
    {
        GetWindow<WeaponDataCreatorWindow>("Weapon Data Creator");
    }

    private void OnGUI()
    {
        GUILayout.Label("Vampire Survivors Weapon Data Creator", EditorStyles.boldLabel);
        EditorGUILayout.Space(10);

        // JSON 파일 선택
        EditorGUILayout.LabelField("1. JSON 파일 선택", EditorStyles.boldLabel);
        jsonFile = (TextAsset)EditorGUILayout.ObjectField("Weapon JSON", jsonFile, typeof(TextAsset), false);

        EditorGUILayout.Space(10);

        // 저장 경로
        EditorGUILayout.LabelField("2. 저장 경로", EditorStyles.boldLabel);
        saveFolder = EditorGUILayout.TextField("Save Folder", saveFolder);

        EditorGUILayout.Space(15);

        // 생성 버튼
        GUI.enabled = jsonFile != null;
        if (GUILayout.Button("ScriptableObject 생성하기", GUILayout.Height(40)))
        {
            CreateWeaponAssets();
        }
        GUI.enabled = true;

        EditorGUILayout.Space(10);

        // 로그 출력
        if (!string.IsNullOrEmpty(logMessage))
        {
            EditorGUILayout.HelpBox(logMessage, logType);
        }

        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField("사용 방법", EditorStyles.boldLabel);
        EditorGUILayout.HelpBox(
            "1. JSON 파일을 Project 창에 넣으세요.\n" +
            "2. 위 ObjectField에 JSON을 드래그앤드롭하세요.\n" +
            "3. 'ScriptableObject 생성하기' 버튼을 누르세요.\n" +
            "→ Assets/Data/WeaponData 폴더에 자동 생성됩니다.",
            MessageType.Info);
    }

    private void CreateWeaponAssets()
    {
        if (jsonFile == null)
        {
            SetLog("JSON 파일이 선택되지 않았습니다.", MessageType.Error);
            return;
        }

        // 폴더 생성
        if (!Directory.Exists(saveFolder))
        {
            Directory.CreateDirectory(saveFolder);
            AssetDatabase.Refresh();
        }

        // JSON 파싱 (배열을 직접 못 읽기 때문에 Wrapper 사용)
        string wrappedJson = "{\"weapons\":" + jsonFile.text + "}";
        WeaponListWrapper wrapper = JsonUtility.FromJson<WeaponListWrapper>(wrappedJson);

        if (wrapper == null || wrapper.weapons == null || wrapper.weapons.Count == 0)
        {
            SetLog("JSON 파싱에 실패했거나 데이터가 없습니다.", MessageType.Error);
            return;
        }

        int createdCount = 0;
        int overwrittenCount = 0;

        foreach (var data in wrapper.weapons)
        {
            string assetPath = $"{saveFolder}/{data.Name}.asset";

            // 이미 존재하는 에셋인지 확인
            WeaponItemData existing = AssetDatabase.LoadAssetAtPath<WeaponItemData>(assetPath);
            WeaponItemData asset;

            if (existing != null)
            {
                asset = existing;
                overwrittenCount++;
            }
            else
            {
                asset = ScriptableObject.CreateInstance<WeaponItemData>();
                AssetDatabase.CreateAsset(asset, assetPath);
                createdCount++;
            }

            // 데이터 할당
            asset.Name = data.Name;
            asset.Korean_Name = data.Korean_Name;
            asset.Base_Damage_Min = data.Base_Damage_Min;
            asset.Base_Damage_Max = data.Base_Damage_Max;
            asset.Cooldown_Min = data.Cooldown_Min;
            asset.Cooldown_Max = data.Cooldown_Max;
            asset.Amount_Min = data.Amount_Min;
            asset.Amount_Max = data.Amount_Max;

            EditorUtility.SetDirty(asset);
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        SetLog($"완료!\n새로 생성: {createdCount}개\n덮어쓰기: {overwrittenCount}개\n총 {wrapper.weapons.Count}개 처리됨", MessageType.Info);
    }

    private void SetLog(string message, MessageType type)
    {
        logMessage = message;
        logType = type;
        Repaint();
    }

    // JSON 파싱용 Wrapper 클래스
    [System.Serializable]
    private class WeaponListWrapper
    {
        public List<WeaponJsonData> weapons;
    }

    [System.Serializable]
    private class WeaponJsonData
    {
        public string Name;
        public string Korean_Name;
        public float Base_Damage_Min;
        public float Base_Damage_Max;
        public float Cooldown_Min;
        public float Cooldown_Max;
        public int Amount_Min;
        public int Amount_Max;
    }
}
#endif