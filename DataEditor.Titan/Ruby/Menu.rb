# Menu.rb
# Describe the Menu

class Menu
	module_function
	def Initialize
		Menu.Add("文件")  do
			Menu.Add("保存",menu_save) 
			Menu.Add("另存为",menu_save_as)
			Menu.Add("")
			Menu.Add("退出",menu_exit)
		end
		Menu.Add("帮助") do
			Menu.Add("关于",menu_about)
		end
	end
end