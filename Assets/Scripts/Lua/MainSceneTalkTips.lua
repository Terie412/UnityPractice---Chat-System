require "global_data"

MainSceneTalkTips = {
	name = "MainSceneTalkTips"
}

local GameObject = UnityEngine.GameObject
local Transform = UnityEngine.Transform
local TweenUtil = TweenUtil
local SceneManager = UnityEngine.SceneManager
local RectTransform = UnityEngine.RectTransform

local gameMgr = GameMgr.gameMgr
local globalData = GameMgr.globalData
local eventMgr = GameMgr.eventMgr
local netWorkMgr = GameMgr.netWorkMgr

function MainSceneTalkTips:init(gameObject)
	self.gameObject = gameObject
	self.transform = gameObject.transform
end

function MainSceneTalkTips:awake()
	self.talkAreaGO = GameObject.Find("TalkArea.GO")
	self.textArea = GameObject.Find("TextArea"):GetComponent("LoopScrollRect")
	self.mainSceneMask = self.transform:Find("/Canvas/MainScene/MainSceneMask").gameObject
	self.left = self.transform:Find("/Canvas/MainScene/Left").gameObject
end

function MainSceneTalkTips:start()
	eventMgr:RegisterEvent("WORLD_MSG_COME", EH_worldMsgCome)
	self.talkAreaGO:GetComponent("EventTriggerListener").onPointUp = OnClick_TalkAreaGO
end

function MainSceneTalkTips:update()
	
end

function MainSceneTalkTips:onEnable()
	
end

function OnClick_TalkAreaGO()
	MainSceneTalkTips.mainSceneMask:SetActive(true)
	MainSceneTalkTips.left:SetActive(true)

	leftRect = MainSceneTalkTips.left:GetComponent("RectTransform")
	leftRect.anchoredPosition = gameMgr:NewVector2(-leftRect.rect.width/2, leftRect.anchoredPosition.y)
	TweenUtil.ChangeAnchorPositionTo(leftRect, gameMgr:NewVector2(leftRect.rect.width/2, 0), 0.3)
end

function EH_worldMsgCome()
	count = MainSceneTalkTips.textArea.totalCount
	if(count <= 0)then
		MainSceneTalkTips.textArea.totalCount = count + 1
		MainSceneTalkTips.textArea:RefillCellsFromEnd()
	end
	MainSceneTalkTips.textArea:RefreshCells()
	-- MainSceneTalkTips.textArea:SrollToCell()
end

return MainSceneTalkTips