using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class RoleUI : MonoBehaviour {

    private static RoleUI instance;

    public static RoleUI Instance
    {
        get
        {
            return instance;
        }
    }

    private Button createBtn;
    private Button enterBtn;
    private Button selectRoleBtn;
    private InputField nicknameInput;
    private Text msgText;
    private Text rolelevelText;
    private bool isSecelt = false;
    private bool isHaverole = false;
    enum ButtonType
    {
        CREATE_ROLE,
        ENTER_GAME,
        SELECT_ROLE
    }

    void Awake() {
        instance = this;
    }

	// Use this for initialization
	void Start () {
        createBtn = GameObject.Find("CreateBtn").GetComponent<Button>();
        createBtn.onClick.AddListener(() => OnButtonClick(ButtonType.CREATE_ROLE));
        enterBtn = GameObject.Find("EnterBtn").GetComponent<Button>();
        enterBtn.onClick.AddListener(() => OnButtonClick(ButtonType.ENTER_GAME));
        selectRoleBtn = GameObject.Find("roleButton").GetComponent<Button>();
        selectRoleBtn.onClick.AddListener(() => OnButtonClick(ButtonType.SELECT_ROLE));
        msgText = GameObject.Find("MsgText").GetComponent<Text>();
        nicknameInput = GameObject.Find("NicknameInput").GetComponent<InputField>();
        rolelevelText = GameObject.Find("rolelevel").GetComponent<Text>();
        long accountid = ApplicationData.accountid;
        RoleController.Instance.SendRoleListRequest(accountid);
        selectRoleBtn.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void OnButtonClick(ButtonType type)
    {
        string account = nicknameInput.text;
        Debug.Log("---" + account + "---");
        
        if (type == ButtonType.CREATE_ROLE)
        {
            if (isHaverole)
            {
                ShowErrorMsg("你已经拥有的角色，点击开始游戏按钮开始进入游戏吧");
                return;
            }
            if (account.Equals(""))
            {
                ShowErrorMsg("昵称不能为空");
                return;
            }
            RoleController.Instance.SendRoleCreateRequest(account);
        }
        else if (type == ButtonType.ENTER_GAME)
        {
            Application.LoadLevel("game");
        }
    }

    public void ShowErrorMsg(string msg)
    {
        this.msgText.text = msg;
    }

    public void ShowRoleList(List<role_message.Role> roles)
    {
        if(roles != null && roles.Count > 0)
        {
            isHaverole = true;
            selectRoleBtn.gameObject.SetActive(true);
            role_message.Role role = roles[0];
            rolelevelText.text = role.nickname + " Lv." + role.level;
        }
    }
}
