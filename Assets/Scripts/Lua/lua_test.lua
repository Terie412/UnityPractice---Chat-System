function Main()
	print("这是我的第一个lua文件")
	GameObject = UnityEngine.GameObject
	print(GameObject)
	print(UnityEngine)
	-- print(Transform)
	obj = GameObject.Find("/Canvas/Image").gameObject.name;
	print(obj)
	-- for k,v in pairs(obj) do
	-- 	print(k,v)
	-- end
end