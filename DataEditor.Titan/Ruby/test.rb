#encoding:utf-8
# Arce Scrupt : Test.rb
# Run Single Tests.


Path["project"] = "Test/PjVATst"
Path.RequestPath("project","请选择工程文件夹")
require "Ruby/File - va.rb"
Builder.In(Window["Main"])
Builder.In(Builder.Add(:tabs, {}))
	Builder.In(Builder.Add(:tab, {:text => "www"}))
		Builder.Add(:text,{:text => "FUCK"})
		Builder.Add(:int,{:text => "WTF" , :maxvalue => 1000})
		Builder.Add(:float, {:text => "Floats", :digit => 3})
		Builder.Add(:group, {:text => "WOW"}) do
			Builder.Add(:text,{:text => "My love"})
			choice = Filechoice.new("actor")
			Builder.Add(:choose, {:text => "choose_test", :choice => {-1 => "HAHAHA", 0 => "CMB", nil => choice}}) 
		end
		Builder.Add(:image, {:text => "image_test", :path => "Graphics/Characters/", :split => Help::VX_IMAGE_SPLIT, :show => Help::VX_IMAGE_SHOW})
		Builder.Next
		#Builder.Add(:image, {:text => "icon_test", :path => "Graphics/System/Iconset.png", :split => new Split()})
		text = Text.new do |*args|
			"That's Test"
		end
		window = Proc.new do |window, value|
			window.Binding.Text = "我是傻雕"
			Builder.In(window)
				Builder.Add(:choose, {:text => "RUSB?", :choice => {-1 => "Yes!", 0 => "Of course!"}})
			Builder.Out
			window.Binding.Height = 600
		end
		Builder.Add(:complex, {:text => "drop_item", :text => text, :window => window})
	Builder.Out
	Builder.In(Builder.Add(:tabs, {}))
		#Builder.In(Builder.Add(:list, {:text => Help.Get_Default_Text}))
			
		#Builder.Out
	Builder.Out
Builder.Out