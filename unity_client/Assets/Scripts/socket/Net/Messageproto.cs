using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class MessageProto
{
    //消息号由模块号+操作号组成

    //Login Module
    public const int MSG_LOGIN_MODULE_NO                        = 0x00010000;
    public const int MSG_ACCOUNT_LOGIN_REQUEST_C2S              = 0x00010001;
    public const int MSG_ACCOUNT_LOGIN_RESPONSE_S2C             = 0x00010002;
    public const int MSG_ACCOUNT_REGIST_REQUEST_C2S             = 0x00010003;
    public const int MSG_ACCOUNT_REGIST_RESPONSE_S2C            = 0x00010004;

    public const int MSG_ROLE_MODULE_NO                         = 0x00020000;
    public const int MSG_ROLE_LIST_REQUEST_C2S                  = 0x00020001;
    public const int MSG_ROLE_LIST_RESPONSE_S2C                 = 0x00020002;
    public const int MSG_ROLE_CREATE_REQUEST_C2S                = 0x00020003;
    public const int MSG_ROLE_CREATE_RESPONSE_S2C               = 0x00020004;
}