using UnityEngine;
using System.Collections;
using System.IO;
using role_message;
using System.Collections.Generic;
using ProtoBuf;

public class RoleController : MessageController {

    private static RoleController instance;

    public static RoleController Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new RoleController();
            }
            return instance;
        }
    }

    private RoleController()
    {

    }

    public void OnMessageResponse(int opcode, MemoryStream stream)
    {
        Debug.Log("---role controller receive opcode " + opcode);
        switch (opcode)
        {
            case MessageProto.MSG_ROLE_LIST_RESPONSE_S2C % 100:
                OnRoleListResponse(stream);
                break;
            case MessageProto.MSG_ROLE_CREATE_RESPONSE_S2C % 100:
                OnRoleCreateResponse(stream);
                break;
            default:
                break;
        }
    }

    private void OnRoleCreateResponse(MemoryStream stream)
    {
        CMsgRoleCreateResponse response = ProtoBuf.Serializer.Deserialize<CMsgRoleCreateResponse>(stream);
        Role role = response.role;
        Debug.Log("---role---" + role.nickname);
        Application.LoadLevel("game");
    }

    private void OnRoleListResponse(MemoryStream stream)
    {
        CMsgRoleListResponse response = ProtoBuf.Serializer.Deserialize<CMsgRoleListResponse>(stream);
        List<Role> list = response.roles;
        Debug.Log("---role list---" + list.Capacity);

        RoleUI.Instance.ShowRoleList(list);
    }

    public void SendRoleListRequest(long accountid)
    {
        Debug.Log("---accountid:" + accountid);
        CMsgRoleListRequest request = new CMsgRoleListRequest();
        request.accountid = accountid.ToString();

        MemoryStream stream = new MemoryStream();
        Serializer.Serialize<CMsgRoleListRequest>(stream, request);

        NetManager.Instance.Send(MessageProto.MSG_ROLE_LIST_REQUEST_C2S, stream);
    }

    public void SendRoleCreateRequest(string nickname)
    {
        CMsgRoleCreateRequest request = new CMsgRoleCreateRequest();
        request.nickname = nickname;
        request.roletype = 1;
        request.accountid = ApplicationData.accountid.ToString();

        MemoryStream stream = new MemoryStream();
        Serializer.Serialize<CMsgRoleCreateRequest>(stream, request);

        NetManager.Instance.Send(MessageProto.MSG_ROLE_CREATE_REQUEST_C2S, stream);
    }
}
