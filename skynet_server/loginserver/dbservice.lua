local skynet = require "skynet"
require "skynet.manager"
local mysql = require "mysql"

local db 
local CMD = {}

function CMD.query(sql)
	print("--- dbserver query:" .. sql)
	return db:query(sql)
end

skynet.start(function()
	skynet.dispatch("lua", function(session, address, cmd, ...)
		local f = CMD[cmd]
		skynet.ret(skynet.pack(f(...)))
	end)

	db = mysql.connect {
		host = "127.0.0.1",
		port = 3306,
		database = "simplerpg",
		user = "root",
		password = "root",
		max_packet_size = 1024 * 1024
	}
	print(db)
	if not db then
		print("failedi to connect mysql")
	end

	skynet.register "dbservice"
end)
		
