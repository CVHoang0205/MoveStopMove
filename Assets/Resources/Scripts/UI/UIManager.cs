using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public GameObject panel;
    public GameObject InGamePanel;
    public GameObject SettingsPanel;
    public GameObject indicatorPanel;
    public GameObject Shop;


    public Button playeGame;
    public Button mainMenu;
    public Button quitSettings;

    public Button shopButton;
    public Button settingButton;
    public Button BackButton;
    public JoystickControl joystick;

    public TextMeshProUGUI goldText;

    // Start is called before the first frame update
    void Start()
    {
        InitGameState(1);
        playeGame.onClick.AddListener(() => InitGameState(2));
        InitGold();
        settingButton.onClick.AddListener(() => { SettingsPanel.SetActive(true); });
        mainMenu.onClick.AddListener(() => MainMenuClick());
        quitSettings.onClick.AddListener(() => { SettingsPanel.SetActive(false); });
        shopButton.onClick.AddListener(() => { OpenShop(); });
        BackButton.onClick.AddListener(() => BackHome());
    }

    public void BackHome() 
    {
        Debug.Log("press");
        InitGameState(1);
        Shop.SetActive(false);
    }

    public void OpenShop()
    {
        panel.SetActive(false);
        Shop.SetActive(true);
        CameraFollower.Ins.ChangeState(3);
    }

    private void MainMenuClick() 
    {
        SettingsPanel.SetActive(false);
        InitGameState(1);
        CreateBoss.Ins.RepPlayGame();
        InitGold();
    }

    public void InitGold() 
    {
        goldText.text = CreateBoss.Ins.gold.ToString();
    }

    public void InitGameState(int state) 
    {
        panel.SetActive(state == 1);
        InGamePanel.SetActive(state == 2);
        joystick.gameObject.SetActive(state == 2);
        if (state == 2) 
        {
            indicatorPanel.SetActive(true);
            CreateBoss.Ins.StartGame();
        }else if (state == 1) 
        {
            indicatorPanel.SetActive(false);
        }
        CameraFollower.Ins.ChangeState(state);
    }
}
