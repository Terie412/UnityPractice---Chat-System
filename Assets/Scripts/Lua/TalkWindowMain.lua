require "global_data"

TalkWindowMain = {
	name = "TalkWindowMain"
}

local GameObject = UnityEngine.GameObject
local Transform = UnityEngine.Transform
local TweenUtil = TweenUtil
local SceneManager = UnityEngine.SceneManager
local RectTransform = UnityEngine.RectTransform
local Message = NetWorkMgr.Message

local gameMgr = GameMgr.gameMgr
local globalData = GameMgr.globalData
local eventMgr = GameMgr.eventMgr
local netWorkMgr = GameMgr.netWorkMgr

function TalkWindowMain:init(gameObject)
    self.gameObject = gameObject
    self.transform = gameObject.transform
end

function TalkWindowMain:awake()
  self.packUp = GameObject.Find("PackUp")
  self.totalTalkSendButton = GameObject.Find("Main/Bottom/Button")
  self.mainSceneMask = self.transform:Find("/Canvas/MainScene/MainSceneMask").gameObject
  self.totalTalkScrollRect = self.transform:Find("Main/Center/TotalTalkWindow/Scroll View"):GetComponent("LoopScrollRect")
  self.totalTalkSendButton = self.transform:Find("Main/Bottom/Button").gameObject;
  self.talkInputField = self.transform:Find("Main/Bottom/InputField"):GetComponent("InputField")  
  self.totalTalkWindow = self.transform:Find("Main/Center/TotalTalkWindow").gameObject
  self.friendsTalkWindow = self.transform:Find("Main/Center/FriendTalkWindow").gameObject
  self.friendsListScrollRect = self.transform:Find("Main/Center/FriendTalkWindow/FriendList/Scroll View"):GetComponent("LoopScrollRect")
  self.bgNoFriendSelected = self.transform:Find("Main/Center/FriendTalkWindow/BgNoFriendSelected").gameObject
  self.fTalkWindow = self.transform:Find("Main/Center/FriendTalkWindow/TalkWindow").gameObject
  self.fTalkWindowHead = self.transform:Find("Main/Center/FriendTalkWindow/TalkWindow/Head").gameObject
  self.friendTalkScrollRect = self.transform:Find("Main/Center/FriendTalkWindow/TalkWindow/Scroll View"):GetComponent("LoopScrollRect")
  self.totalOpenButton = self.transform:Find("Main/Top/ChannelList/Total")
  self.friendsOpenButton = self.transform:Find("Main/Top/ChannelList/Friends")
end

function TalkWindowMain:start()
	
end

function TalkWindowMain:update()
	
end

function TalkWindowMain:onEnable()
    self.totalTalkWindow:SetActive(true)
    self.friendsTalkWindow:SetActive(false)

    self.packUp:GetComponent("EventTriggerListener").onPointUp = OnClick_PackUp
    self.totalTalkSendButton:GetComponent("EventTriggerListener").onPointUp = OnClick_TotalTalkWindowSendButton
    self.friendsOpenButton:GetComponent("EventTriggerListener").onPointUp = OnClick_FriendsOpenButton
    self.totalOpenButton:GetComponent("EventTriggerListener").onPointUp = OnClick_TotalOpenButton
    eventMgr:RegisterEvent("WORLD_MSG_COME", EH_onReceiveWorldMsg)

    EH_onReceiveWorldMsg()
end

function OnClick_TotalOpenButton()
    TalkWindowMain.friendsTalkWindow:SetActive(false)
    TalkWindowMain.totalTalkWindow:SetActive(true)
    color = TalkWindowMain.friendsOpenButton:GetComponent("Image").color
    TalkWindowMain.friendsOpenButton:GetComponent("Image").color = gameMgr:NewColor(color.r, color.g, color.b, 0)
    TalkWindowMain.totalOpenButton:GetComponent("Image").color = gameMgr:NewColor(color.r, color.g, color.b, 0.3)
    
    TalkWindowMain.packUp:GetComponent("EventTriggerListener").onPointUp = OnClick_PackUp
    TalkWindowMain.totalTalkSendButton:GetComponent("EventTriggerListener").onPointUp = OnClick_TotalTalkWindowSendButton
    TalkWindowMain.friendsOpenButton:GetComponent("EventTriggerListener").onPointUp = OnClick_FriendsOpenButton
    TalkWindowMain.totalOpenButton:GetComponent("EventTriggerListener").onPointUp = OnClick_TotalOpenButton
    eventMgr:RegisterEvent("WORLD_MSG_COME", EH_onReceiveWorldMsg)
    eventMgr:UnRegisterEvent("OPEN_FRIEND_TALK_WINDOW", EH_openFriendTalkWindow)
end

function OnClick_FriendsOpenButton()
    TalkWindowMain.friendsTalkWindow:SetActive(true)
    TalkWindowMain.totalTalkWindow:SetActive(false)
    TalkWindowMain.bgNoFriendSelected:SetActive(true)
    TalkWindowMain.fTalkWindow:SetActive(false)
    color = TalkWindowMain.friendsOpenButton:GetComponent("Image").color
    TalkWindowMain.totalOpenButton:GetComponent("Image").color = gameMgr:NewColor(color.r, color.g, color.b, 0)
    TalkWindowMain.friendsOpenButton:GetComponent("Image").color = gameMgr:NewColor(color.r, color.g, color.b, 0.3)
    eventMgr:UnRegisterEvent("WORLD_MSG_COME", EH_onReceiveWorldMsg)

    -- refresh friend list --
    friendCount = netWorkMgr.friends.Count
    TalkWindowMain.friendsListScrollRect.totalCount = friendCount
    TalkWindowMain.friendsListScrollRect:RefillCells()

    eventMgr:RegisterEvent("OPEN_FRIEND_TALK_WINDOW", EH_openFriendTalkWindow)    
end

function EH_openFriendTalkWindow(objs)
    TalkWindowMain.bgNoFriendSelected:SetActive(false)
    TalkWindowMain.fTalkWindow:SetActive(true)

    p = objs[0]
    print("与巅峰召唤师聊天：", p.name)
    netWorkMgr.friendToTalk = p
    TalkWindowMain.fTalkWindowHead:GetComponent("Text").text = "与巅峰召唤师 "..p.name.." 聊天"

    eventMgr:RegisterEvent("FRIEND_MSG_COME", EH_onReceiveFriendMsg)
    TalkWindowMain.totalTalkSendButton:GetComponent("EventTriggerListener").onPointUp = OnClick_FriendTalkWindowSendButton

    count = netWorkMgr:GetFriendMsgCount(p)
    TalkWindowMain.friendTalkScrollRect.totalCount = count
    TalkWindowMain.friendTalkScrollRect:RefillCellsFromEnd()
end

function EH_onReceiveFriendMsg(objs)
    friend = objs[0]
    if(TalkWindowMain.friendsTalkWindow.activeSelf)then
        if(netWorkMgr.friendToTalk == friend)then                      
            count = netWorkMgr:GetFriendMsgCount(friend)

            TalkWindowMain.friendTalkScrollRect.totalCount = count
            TalkWindowMain.friendTalkScrollRect:RefillCellsFromEnd()
        end
    end   
end

function OnClick_PackUp()
    eventMgr:UnRegisterEvent("WORLD_MSG_COME", EH_onReceiveWorldMsg)
    leftrect = TalkWindowMain.gameObject:GetComponent("RectTransform")
    TweenUtil.ChangeAnchorPositionTo(leftrect, gameMgr:NewVector2(-leftrect.rect.width / 2, 0), 0.3)
    coroutine.start(DisableAfterSeconds,{TalkWindowMain.mainSceneMask, leftrect.gameObject}, 0.1)
end

function OnClick_FriendTalkWindowSendButton()
    message = TalkWindowMain.talkInputField.text
    if(message == "")then
        return
    end
    TalkWindowMain.talkInputField.text = ""

    msg = gameMgr:NewMessage(netWorkMgr.me, message)
    netWorkMgr:AddFriendMessage(msg)
    curCount = TalkWindowMain.friendTalkScrollRect.totalCount
    if(curCount >= 30)then
        TalkWindowMain.friendTalkScrollRect.totalCount = 30
        TalkWindowMain.friendTalkScrollRect:Refresh()
    else
        TalkWindowMain.friendTalkScrollRect.totalCount = curCount + 1
        TalkWindowMain.friendTalkScrollRect:RefillCellsFromEnd()
    end
    eventMgr:DispatchEvent("ME_FRIEND_MSG_COME", {netWorkMgr.friendToTalk, msg})
end

function OnClick_TotalTalkWindowSendButton()
    message = TalkWindowMain.talkInputField.text
    if(message == "")then
        return
    end
    TalkWindowMain.talkInputField.text = ""

    msg = gameMgr:NewMessage(netWorkMgr.me, message)
    netWorkMgr.worldMsg:Add(msg)
    curCount = TalkWindowMain.totalTalkScrollRect.totalCount
    if(curCount >= 30)then
        TalkWindowMain.totalTalkScrollRect.totalCount = 30
        TalkWindowMain.totalTalkScrollRect:Refresh()
    else
        TalkWindowMain.totalTalkScrollRect.totalCount = curCount + 1
        TalkWindowMain.totalTalkScrollRect:RefillCellsFromEnd()
    end
end

function EH_onReceiveWorldMsg()
    msgCount = netWorkMgr.worldMsg.Count
    count = 30
    if (msgCount < 30) then
        count = msgCount
    end
    TalkWindowMain.totalTalkScrollRect.totalCount = count
    TalkWindowMain.totalTalkScrollRect:RefillCellsFromEnd()
end

function DisableAfterSeconds(goes, sec)
    coroutine.wait(0.5)
    for i,go in ipairs(goes) do
        go:SetActive(false)
    end
end

return TalkWindowMain
