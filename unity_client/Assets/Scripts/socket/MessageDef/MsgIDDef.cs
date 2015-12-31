using System;
using System.Collections.Generic;
using System.Text;
public class MsgIDDef
{
	private Dictionary<int, Type> sc_msg_dic = new Dictionary<int, Type>();
	private static MsgIDDef instance;
	public static MsgIDDef Instance()
	{
		if (null == instance)
		{
			instance = new MsgIDDef();
		}
		return instance;
	}
	private MsgIDDef()
	{
		sc_msg_dic.Add(10001,typeof(login_message.CMsgAccountLoginRequest));
		sc_msg_dic.Add(10002,typeof(login_message.CMsgAccountLoginResponse));
		sc_msg_dic.Add(10003,typeof(login_message.CMsgAccountRegistRequest));
		sc_msg_dic.Add(10004,typeof(login_message.CMsgAccountRegistResponse));
		sc_msg_dic.Add(10101,typeof(role_message.CMsgRoleListRequest));
		sc_msg_dic.Add(10102,typeof(role_message.CMsgRoleListResponse));
		sc_msg_dic.Add(10103,typeof(role_message.CMsgRoleCreateRequest));
		sc_msg_dic.Add(10104,typeof(role_message.CMsgRoleCreateResponse));
	}
	public Type GetMsgType(int msgID)
	{
		Type msgType = null;
		sc_msg_dic.TryGetValue(msgID, out msgType);
		if (msgType==null)
		{
			return null;
		}
		return msgType;
	}
}
