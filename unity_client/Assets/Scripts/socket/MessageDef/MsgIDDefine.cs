using System;
using System.Collections.Generic;
using System.Text;
public class MsgIDDefine
{
	static Dictionary<int, string> msgid2msgname = new Dictionary<int, string>();
	static Dictionary<string, int> msgname2msgid = new Dictionary<string, int>();
	static void Initialize()
	{
		msgid2msgname[10001] = "login_message.CMsgAccountLoginRequest";
		msgname2msgid["login_message.CMsgAccountLoginRequest"] = 10001;
		msgid2msgname[10002] = "login_message.CMsgAccountLoginResponse";
		msgname2msgid["login_message.CMsgAccountLoginResponse"] = 10002;
		msgid2msgname[10003] = "login_message.CMsgAccountRegistRequest";
		msgname2msgid["login_message.CMsgAccountRegistRequest"] = 10003;
		msgid2msgname[10004] = "login_message.CMsgAccountRegistResponse";
		msgname2msgid["login_message.CMsgAccountRegistResponse"] = 10004;
		msgid2msgname[10101] = "role_message.CMsgRoleListRequest";
		msgname2msgid["role_message.CMsgRoleListRequest"] = 10101;
		msgid2msgname[10102] = "role_message.CMsgRoleListResponse";
		msgname2msgid["role_message.CMsgRoleListResponse"] = 10102;
		msgid2msgname[10103] = "role_message.CMsgRoleCreateRequest";
		msgname2msgid["role_message.CMsgRoleCreateRequest"] = 10103;
		msgid2msgname[10104] = "role_message.CMsgRoleCreateResponse";
		msgname2msgid["role_message.CMsgRoleCreateResponse"] = 10104;
	}
	static string GetMsgNameByID(int msgid)
	{
		string msgname = null;
		if (msgid2msgname.TryGetValue(msgid,out msgname))
		{
			return msgname;
		}
		return "";
	}
	static int GetMsgIDByName(string msgname)
	{
		int msgid = 0;
		if (msgname2msgid.TryGetValue(msgname,out msgid))
		{
			return msgid;
		}
		return 0;
	}
}
