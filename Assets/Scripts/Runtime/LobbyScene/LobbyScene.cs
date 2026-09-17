using UnityEngine;
using UnityEngine.UI;
using TMPro; // TextMeshPro를 사용하기 위한 네임스페이스 추가!

public class LobbyScene : SceneUI
{
    enum Buttons
    {
        StartBtn,
        SettingBtn,
        ExitBtn
    }

    enum Texts
    {
        TitleText,
        VersionText,
        GoldText
    }

    public override void Init()
    {
        base.Init(); // 부모(UIBase) 초기화

        // Text 대신 TextMeshProUGUI로 싹 찾아옵니다.
        Bind<Button>(typeof(Buttons));
        Bind<TextMeshProUGUI>(typeof(Texts));

        // 버튼 클릭 이벤트 연결
        GetButton((int)Buttons.StartBtn).onClick.AddListener(OnClickStartBtn);
        GetButton((int)Buttons.SettingBtn).onClick.AddListener(OnClickSettingBtn);
        GetButton((int)Buttons.ExitBtn).onClick.AddListener(OnClickExitBtn);

        // UIBase의 제네릭 Get 함수를 이용해 TMP를 가져와 텍스트를 바꿉니다.
        Get<TextMeshProUGUI>((int)Texts.TitleText).text = "MY AWESOME GAME";
        Get<TextMeshProUGUI>((int)Texts.VersionText).text = "v 1.0.0";
        Get<TextMeshProUGUI>((int)Texts.GoldText).text = "10,000 G";
    }

    // ==========================================
    // 버튼 클릭 시 작동할 함수들
    // ==========================================

    private void OnClickStartBtn()
    {
        Debug.Log("게임 시작! ➡️ GameScene으로 이동");
    }

    private void OnClickSettingBtn()
    {
        Debug.Log("환경설정 창 열기");
    }

    private void OnClickExitBtn()
    {
        Debug.Log("게임 종료");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}