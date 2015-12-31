local skynet = require "skynet"
require "skynet.manager"
local netpack = require "netpack"
local message = require "message"

local msgpack = require("msgpack.core")

local CMD = {}
local protobuf = {}
local dump ={}

local function processAccountLoginRequest(msg)
	print("---process login---")
	local data = protobuf.decode("CMsgAccountLoginRequest", msg)
	local account = data.account
	local password = data.password
	print("----account:" , account)
	print("----password",password)

	local sql = "select * from tb_account where account = '" .. account .. "'" .." and password = ".."'" ..password .. "'"
	local ok, result = pcall(skynet.call, "dbservice", "lua", "query", sql)
	print ("query result=",dump( result ))
	
	local tb = {}
	if ok then
		print(#result)
		if #result > 0 then
			for key,value in pairs(result) do
				tb.result = 0
				tb.accountid = value["accountid"]
			end
		else
			tb.result = 1
		end
		--[[
		for k, v in pairs(value) do
			print(k, v)
		end
		]]--
	end

	local msgbody =  protobuf.encode("CMsgAccountLoginResponse", tb)
	return msgpack.pack(message.MSG_ACCOUNT_LOGIN_RESPONSE_S2C, msgbody)
end

local function processAccountRegistRequest(msg)
	print("---process regist---")
	local data = protobuf.decode("CMsgAccountRegistRequest", msg)
	local account = data.account
	local password = data.password
	print("---account:", account)

	local tb = {}
	local id = os.time()
	local sql = "insert into tb_account(account, password, accountid) values('".. account .."' ,'"..password .. "',"..id ..")"
	print(sql)
	local ok, result = pcall(skynet.call, "dbservice", "lua", "query", sql)
	print(result);
	print ("query result=",dump( result ))
	if ok then
		tb.result = 0;
		tb.accountid = id
		print("regist user success!")
	else
		tb.result = 2
	end

	local msgbody = protobuf.encode("CMsgAccountRegistResponse", tb)
	return msgpack.pack(message.MSG_ACCOUNT_REGIST_RESPONSE_S2C, msgbody)
end

function CMD.dispatch(opcode, msg)
	print("login dispatch msgno " .. opcode)
	if opcode == message.MSG_ACCOUNT_LOGIN_REQUEST_C2S % 100 then
		return processAccountLoginRequest(msg)
	elseif opcode == message.MSG_ACCOUNT_REGIST_REQUEST_C2S %100 then
		return processAccountRegistRequest(msg)
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
	local login_data = io.open("../proto/login_message.pb", "rb")
	local buffer = login_data:read "*a"
	login_data:close()
	protobuf.register(buffer)

	skynet.register "loginservice"
end)
