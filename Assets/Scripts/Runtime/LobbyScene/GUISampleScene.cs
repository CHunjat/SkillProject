using UnityEngine;
using UnityEngine.UI;

public class GUISampleScene : UIBase
{

    enum Texts
    {
        TitleText,
        Patron
    }


    enum Buttons
    {
        Countinue,
        Start,
        Setting,
        Exit
    }

    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        // 1. 자동 바인딩 수행 (Enum에 적힌 이름의 컴포넌트들을 알아서 수집)
        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));

        // 2. 바인딩된 컴포넌트 사용 및 이벤트 등록
        GetButton((int)Buttons.Start).onClick.AddListener(OnClickedStart);
        GetButton((int)Buttons.Countinue).onClick.AddListener(OnCountinueButton);
        GetButton((int)Buttons.Setting).onClick.AddListener(OnSettingButton);
        GetButton((int)Buttons.Exit).onClick.AddListener(OnExitButton);
        GetText((int)Texts.TitleText).text = "GUI SAMPLE SCENE";
    }

    private void OnClickedStart()
    {
        Debug.Log("게임 시작 버튼 클릭됨!");
    }
    private void OnCountinueButton()
    {
        Debug.Log("컨티뉴버튼");
    }
    private void OnSettingButton()
    {
        Debug.Log("설정버튼");
    }
    private void OnExitButton()
    {
        Debug.Log("나가기버튼");
#if UNITY_EDITOR
        // 1. 유니티 에디터 안에서 플레이 중일 때 게임 끄기
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // 2. 실제로 빌드된 게임(.exe, .apk 등)에서 게임 끄기
        Application.Quit();
#endif
    }



}

