#encoding:utf-8
# Arce Scrupt : Test.rb
# Run Single Tests.

require "Lead.rb"

Path["project"] = "Test/PjVXTst"
puts "FUCK!!!"
=begin
Path.RequestPath("project","请选择工程文件夹")

require "Ruby/File - vx.rb"
Builder.In(Window["Main"])
Builder.Add(:tabs) do
	Builder.Add(:tab, { text: "www" }) do
		Builder.Add(:text,{ text: "FUCK" })
		Builder.Add(:int,{ text: "WTF", maxvalue: 1000 })
		Builder.Add(:float, { text: "Floats", digit: 3 })
		Builder.Add(:group, { text: "WOW" }) do
			Builder.Add(:text,{ text: "My love" })
			choice = Filechoice.new("actor")
			Builder.Add(:choose, { text: "choose_test", choice: {-1 => "HAHAHA", 0 => "CMB", nil => choice} }) 
		end
		Builder.Add(:image, { text: "image_test", path: "Graphics/Characters/", split: Help::VX_IMAGE_SPLIT, show: Help::VX_IMAGE_SHOW })
		Builder.Next
		#Builder.Add(:image, { text: "icon_test", path: "Graphics/System/Iconset.png", split: new Split() })
		text = Text.new do |*args|
			"That's Test"
		end
		window = Proc.new do |window, value|
			window.Binding.Text = "我是傻雕"
			Builder.In(window)
				Builder.Add(:choose, { text: "你是傻逼吧？", choice: {-1 => "Yes!", 0 => "Of course!"} })
			Builder.Out
			window.Binding.Height = 600
		end
		Builder.Add(:complex, { text: "drop_item", textbook: text, window: window })
	end
	Builder.Add(:tab) do
		x = Builder.Add(:list, { textbook: Help.Get_Default_Text })
		x.Value = Data["actor"]
	end
	Builder.Add(:tab, { text: "我是傻逼" }) do
		Builder.Add(:textlist, { choices: ["A","B","C","D","E","F"], value: [1,2,3,4,5,6,7], 
			default: 0, file: Data["actor"], textbook: Help.Get_Default_Text })
		window = Proc.new do |window, value|

		end
		text = []
		text[0] = Text.new { |value, *args| value[:id].to_s }
		text[1] = Text.new do |value, watch, *args|
			"测试"
		end
		columns = ["学会的等级", "技能"]
		Builder.Add(:view, { 
			text: "ListViewTest",
			catalogue: text, 
			window: window,
			columns: columns,
			new: nil,
			width: 600,
			height: 400
	  })
		end
		block = Proc.new { puts "Yes you are SB!" }
		Builder.Add(:tab, { }) do
			s = Builder.Add(:radio, { group: "SB_GROUP_FOR_TEST", key: 1000, text: "Untitled" }) do
				Builder.Add(:button, { text: "I'm SB", run: block }) 
				Builder.Add(:button, { text: "I'm SB", run: block }) 
				Builder.Add(:button, { text: "I'm SB", run: block }) 
				Builder.Add(:button, { text: "I'm SB", run: block }) 
				Builder.Add(:button, { text: "I'm SB", run: block }) 
				Builder.Add(:button, { text: "I'm SB", run: block }) 
			end
			Builder.Add(:radio, { group: "SB_GROUP_FOR_TEST", key: 2000, text: "Vntitled" }) do
				Builder.Add(:button, { text: "Click Me", run: Proc.new { puts "WOW" } })
			end
			Builder.Add(:radio, { group: "SB_GROUP_FOR_TEST", key: 3000, text: "Wntitled" })
			Builder.Add(:radio, { group: "SB_GROUP_FOR_TEST", key: 4000, text: "Xntitled" })
		end
	end
#Builder.Out
=end