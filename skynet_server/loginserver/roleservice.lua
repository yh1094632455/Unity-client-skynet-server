local skynet = require "skynet"
require "skynet.manager"
local netpack = require "netpack"
local message = require "message"

msgpack = require("msgpack.core")

local CMD = {}
local protobuf = {}
local dump ={}

local function processRoleListRequest(msg)
	print("---process role list---")
	local data = protobuf.decode("CMsgRoleListRequest", msg)
	local accountid = data.accountid
	print("---account:", accountid)

	local roles = {}
	local sql = "select * from tb_role where accountid = '" .. accountid .. "'"
	local ok, result = pcall(skynet.call, "dbservice", "lua", "query", sql)
	print ("query result=",dump( result ))
	if ok then
		for key,value in pairs(result) do
			local role = {}
			role.id = value["id"]
			role.nickname = value["nickname"]
			role.level = value["level"]
			role.roletype = value["roletype"]
			table.insert(roles, role)
		end
	else
		print("---query error---")
	end

	local tb = {}
	tb.roles = roles
	local msgbody = protobuf.encode("CMsgRoleListResponse", tb)
	return msgpack.pack(message.MSG_ROLE_LIST_RESPONSE_S2C, msgbody)
end

local function processRoleCreateRequest(msg)
	print("---process role create---")
	local data = protobuf.decode("CMsgRoleCreateRequest", msg)
	local accountid = data.accountid
	local nickname = data.nickname
	local roletype = data.roletype
	local rolelevel = 1
	print("---nickname:", nickname)

	local tb = {}
	local id = os.time()
	local role = {}
	local sql = "insert into tb_role(roleid, accountid, nickname,level, roletype) values ('"..id.."','"..accountid.."','"..nickname.."',' 1 ','"..roletype.."')"
	local ok, result1 = pcall(skynet.call, "dbservice", "lua", "query", sql)
	print ("query result1=",dump( result1 ))
	if ok then
		tb.result = 0
		role.id = id
		role.nickname = nickname
		role.roletype = roletype
		role.level = 1
		print("---query1 error---")
	else
		print("---query error---")
	end
	tb.role = role
	local msgbody = protobuf.encode("CMsgRoleCreateResponse", tb)
	return msgpack.pack(message.MSG_ROLE_CREATE_RESPONSE_S2C, msgbody)
end

function CMD.dispatch(opcode, msg)
	print("role dispatch msgno " .. opcode)
	if opcode == message.MSG_ROLE_LIST_REQUEST_C2S % 100 then
		return processRoleListRequest(msg)	
	elseif opcode == message.MSG_ROLE_CREATE_REQUEST_C2S % 100 then
		return processRoleCreateRequest(msg)
	end
end

function dump(obj)
    local getIndent, quoteStr, wrapKey, wrapVal, dumpObj
    getIndent = function(level)
        return string.rep("\t", level)
    end
    quoteStr = function(str)
        return '"' .. string.gsub(str, '"', '\\"') .. '"'
    end
    wrapKey = function(val)
        if type(val) == "number" then
            return "[" .. val .. "]"
        elseif type(val) == "string" then
            return "[" .. quoteStr(val) .. "]"
        else
            return "[" .. tostring(val) .. "]"
        end
    end
    wrapVal = function(val, level)
        if type(val) == "table" then
            return dumpObj(val, level)
        elseif type(val) == "number" then
            return val
        elseif type(val) == "string" then
            return quoteStr(val)
        else
            return tostring(val)
        end
    end
    dumpObj = function(obj, level)
        if type(obj) ~= "table" then
            return wrapVal(obj)
        end
        level = level + 1
        local tokens = {}
        tokens[#tokens + 1] = "{"
        for k, v in pairs(obj) do
            tokens[#tokens + 1] = getIndent(level) .. wrapKey(k) .. " = " .. wrapVal(v, level) .. ","
        end
        tokens[#tokens + 1] = getIndent(level - 1) .. "}"
        return table.concat(tokens, "\n")
    end
    return dumpObj(obj, 0)
end
	
skynet.start(function()
	print("---start login server---")
	skynet.dispatch("lua", function(session, source, cmd, ...)
		local f = CMD[cmd]
		skynet.ret(skynet.pack(f(...)))
	end)
	
	protobuf = require "protobuf"
	local login_data = io.open("../proto/role_message.pb", "rb")
	local buffer = login_data:read "*a"
	login_data:close()
	protobuf.register(buffer)

	skynet.register "roleservice"
end)
