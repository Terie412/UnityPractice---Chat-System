require "global_data"

launch = {
	name = "launch"
}

local GameObject = UnityEngine.GameObject
local Transform = UnityEngine.Transform
local TweenUtil = TweenUtil
local SceneManager = UnityEngine.SceneManager

local gameMgr = GameMgr.gameMgr
local globalData = GameMgr.globalData

function launch:init(gameObject)
	self.gameObject = gameObject
end

function launch:awake()
	self.slider = GameObject.Find("Slider"):GetComponent("Slider")
	self.info = GameObject.Find("Text"):GetComponent("Text")
end

function launch:start()
	coroutine.start(SliderPractice)
end

function launch:update()
	
end

function launch:onEnable()
	
end

function SliderPractice()
	tips =
        {
            "获取资源地址...",
            "下载高清资源...",
            "资源解压缩，此过程不需要消耗流量...",
            "检查资源完整性...",
            "准备开始游戏..."
        }
       for i=1,5 do
       		TweenUtil.ChangeSliderValueTo(launch.slider, launch.slider.value + 0.2, 0.5)
       		launch.info.text = tips[i]
       		coroutine.wait(0.5)
       end
       gameMgr:LoadScene("Main")
       gameMgr.gameObject:AddComponent(System.Type.GetType("NetWorkMgr"))
end

return launch
