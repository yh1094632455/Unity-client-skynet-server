# skynet-unity-client
A MMORPG client 

#使用

1、运行ProtoToCS下proto2csharp.py，把proto文件转化为C#代码

2、proto文件在ProtoToCS\ProtoGen\proto\luamsg下，使用protobuf语法编写

3、客服端发送请求和获得服务器回调用法，参考（Assets\Scripts\Module\Login\LoginController.cs）

发送请求(LoginUI.cs）
	LoginController.Instance().SendLoginRequest(account, password);

回调（LoginController.cs）

NetManager.Instance.RegisterProtoHandler(MsgIDDefineDic.LOGIN_MESSAGE_CMSGACCOUNTLOGINRESPONSE, HandlerLogin);

