global_data = {}

local GameObject = UnityEngine.GameObject
local Transform = UnityEngine.Transform

function global_data:init()
	gameMgr_GO = GameObject.Find("/GameMgr")
	self.gameMgr = gameMgr_GO:GetComponent("GameMgr")
	self.eventMgr = gameMgr_GO:GetComponent("EventMgr")
	self.globalData = gameMgr_GO:GetComponent("GlobalData")

	print("初始化以下变量：")
	print(self.gameMgr)
	print(self.eventMgr)
	print(self.globalData)
	print("初始化完毕...")
	return {}
end

return global_data