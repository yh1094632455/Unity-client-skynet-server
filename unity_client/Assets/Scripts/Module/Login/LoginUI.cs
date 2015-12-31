using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using login_message;

public class LoginUI : MonoBehaviour {

    private Button loginBtn;
    private Button registBtn;
    private InputField accountFiled;
    private InputField passwordFiled;
    private Text msgText;

    private enum ButtonType
    {
        LOGIN,
        REGIST
    }

    void Awake()
    {
        AddListener();
    }

	// Use this for initialization
	void Start () {
        accountFiled = GameObject.Find("AccountField").GetComponent<InputField>();
        passwordFiled = GameObject.Find("PasswordField").GetComponent<InputField>();

        msgText = GameObject.Find("MsgText").GetComponent<Text>();

        loginBtn = GameObject.Find("LoginBtn").GetComponent<Button>();
        loginBtn.onClick.AddListener(() => OnButtonClick(ButtonType.LOGIN));
        registBtn = GameObject.Find("RegistBtn").GetComponent<Button>();
        registBtn.onClick.AddListener(() => OnButtonClick(ButtonType.REGIST));
	}
	
    private void AddListener()
    {
        Messenger.AddListener<CMsgAccountLoginResponse>(MessageNotice.MN_LOGIN, UpdateLogin);
    }

    private void RemoveListener()
    {
        Messenger.RemoveListener<CMsgAccountLoginResponse>(MessageNotice.MN_LOGIN, UpdateLogin);
    }
	// Update is called once per frame
	void Update () {
	
	}

    private void OnButtonClick(ButtonType type)
    {
        string account = accountFiled.text;
        string password = passwordFiled.text;
        Debug.Log("---"+account+"---");
        if(account.Equals(""))
        {
            ShowErrorMsg("请先输入账号");
            return;
        }
        if (password.Equals(""))
        {
            ShowErrorMsg("请输入密码");
            return;
        }
        if(type == ButtonType.LOGIN)
        {
            LoginController.Instance().SendLoginRequest(account, password);
        }
        else if(type == ButtonType.REGIST)
        {
            LoginController.Instance().SendRegistRequest(account,password);
        }
    }

    public void ShowErrorMsg(string msg)
    {
        msgText.text = msg;
    }

    private void UpdateLogin(CMsgAccountLoginResponse data)
    {
        if(data != null)
        {
            Debug.Log(data.accountid);
        }
    }
}
