# Arce Sctipt : Weapon - xp.rb
# Describe the user Interface for Weapon

require "File - xp.rb"
Builder.Add(:tab , { :text => "武器" }) do
		list = Builder.Add(:list, {:textbook => Help.Get_Default_Text}) do
		Builder.Add(:text , {:actual => :name , :text => "名称" })
		Builder.Add(:icon , {:actual => :icon_name, :text => "图标"})
			Builder.Next
		Builder.Add(:text , {:actual => :description , :text => "说明"})
			Builder.Next
		Builder.Add
			choice = Filechoice.new("animation")
		Builder.Add(:choose , {:actual => :animation1_id , :text => "使用方的动画" , :choose => { 0 => "（无）" , nil => choice }})	
		Builder.Add(:choose , {:actual => :animation2_id , :text => "对象方的动画" , :choose => { 0 => "（无）" , nil => choice }})
			Builder.Next
		Builder.Add(:int, {:actual => :price , :text => "价格"})
		Builder.Add(:int, {:actual => :atk , :text => "攻击力"})
		Builder.Add(:int, {:actual => :pdef , :text => "物理防御"})
		Builder.Add(:int, {:actual => :mdef , :text => "魔法防御"})
			Builder.Next
		Builder.Add(:int, {:actual => :str_plus, :text => "力量+"})
		Builder.Add(:int, {:actual => :dex_plus, :text => "灵巧+"})
		Builder.Add(:int, {:actual => :agi_plus, :text => "速度+"})
		Builder.Add(:int, {:actual => :int_plus, :text => "魔力+"})
			Builder.Order
			Builder.Next
			Builder.Order
		Builder.Add(:checklist , {:actual => :element_set , :text => "属性", :link => Filechoice.new("system.property")})
		Builder.Add(:bufflist , {:actual => {"+" => plus_state_set, "-" => minus_state_set, "" => nil}})
	end
end

=begin
RPG::Weapon
武器的数据类。
id 
ID。

name 
名称。

icon_name 
图标图像的文件名。

description 
说明。

animation1_id 
攻击方的动画 ID。

animation2_id 
对象方的动画 ID。

price 
价格。

atk 
攻击力。

pdef 
物理防御。

mdef 
魔法防御。

str_plus 
力量+。

dex_plus 
灵巧+。

agi_plus 
速度+。

int_plus 
魔力+。

element_set 
属性。为属性 ID 的数组。

plus_state_set 
附加状态。为状态 ID 的数组。

minus_state_set 
解除状态。为状态 ID 的数组。

=end