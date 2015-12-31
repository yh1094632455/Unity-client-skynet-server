using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;


public interface MessageController
{
    /*响应服务器返回的消息*/
    void OnMessageResponse(int opcode, MemoryStream stream);

}

