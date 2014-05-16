# This file is in coding: utf-8
# Arce Sctipt : Armor - xp.rb
# Describe the user Interface for Armor

require "Ruby/XP/File - xp.rb"
Builder.Add(:tab , { :text => "防具" }) do
	list = Builder.Add(:list, {:textbook => Help.Get_Default_Text ,:text => "防具"}) do
			Builder.Order
		Builder.Add(:text , {:actual => :name , :text => "名称" })
		Builder.Add(:image , {:actual => {:name => :icon_name } , :label => 2, 
			 	:text => "图标", :path => "Graphics/Icons", :show => Help::XP_IMAGE_SPLIT, :split => Help::XP_IMAGE_SPLIT })
			Builder.Next
		Builder.Add(:text , {:actual => :description , :text => "说明"})
			Builder.Next
		Builder.Add(:choose , {:actual => :kind , :text => "种类" , :choice => { 0 => "盾" , 1 => "头部防具", 2 => "身体防具", 3 => "饰品" }})	
			choice = Filechoice.new("state")
		Builder.Add(:choose , {:actual => :auto_state_id , :text => "自动状态" , :choice => { 0 => "（无）" , nil => choice }})
			Builder.Next
		Builder.Add(:int, {:actual => :price , :text => "价格"})
		Builder.Add(:int, {:actual => :pdef , :text => "物理防御"})
		Builder.Add(:int, {:actual => :mdef , :text => "魔法防御"})
		Builder.Add(:int, {:actual => :eva , :text => "回避修正"})
			Builder.Next
		Builder.Add(:int, {:actual => :str_plus, :text => "力量+"})
		Builder.Add(:int, {:actual => :dex_plus, :text => "灵巧+"})
		Builder.Add(:int, {:actual => :agi_plus, :text => "速度+"})
		Builder.Add(:int, {:actual => :int_plus, :text => "魔力+"})
			Builder.OrderAndNext
			text = Text.new { |*args| args[0].Text }
		Builder.Add(:checklist , {:actual => :guard_element_set , :text => "属性防御", :data => Data["system"]["@elements"] , :textbook => text })
			text = Text.new { |*args| args[0]["@name"].Text }
		Builder.Add(:checklist , {:actual => :guard_state_set , :text => "状态防御", :data => Data["state"] , :textbook => text })
	end
	list.Value = Data["armor"]
end
=begin
属性id 
ID。

name 
名称。

icon_name 
图标图像的文件名。

description 
说明。

kind 
种类（0：盾，1：头部防具，2：身体防具，3：装饰品）。

auto_state_id 
自动状态的 ID。

price 
价格。

pdef 
物理防御。

mdef 
魔法防御。

eva 
回避修正。

str_plus 
力量+。

dex_plus 
灵巧+。

agi_plus 
速度+。

int_plus 
魔力+。

guard_element_set 
属性防御。为属性 ID 的数组。

guard_state_set 
状态防御。为状态 ID 的数组。
	
=end