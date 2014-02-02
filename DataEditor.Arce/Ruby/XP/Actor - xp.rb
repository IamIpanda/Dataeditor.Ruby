#encoding:utf-8
# Arce Script: actor - xp.rb
# describe the user interface of actor

require "Ruby/XP/File - xp.rb"

Builder.Add(:tab , { :text => "角色" }) do
	list = Builder.Add(:list, {:textbook => Help.Get_Default_Text, :text => "角色"}) do
		Builder.Add(:text , {:actual => :name , :text => "名称" })
			choice = Filechoice.new("class")
		Builder.Add(:choose , {:actual => :class , :text => "职业" , :choices => { nil => choice } })
			Builder.Order
		Builder.Add(:int , {:actual => :start_level , :text => "初始等级"})
		Builder.Add(:int , {:actual => :final_level , :text => "最终等级"})
			Builder.Next
			Builder.Order
		Builder.Add(:exp , {:actual => {:base => :exp_basis, :inflation => :exp_inflation} , :text => "EXP 曲线"})
		Builder.Add(:image , {:actual => {:name => :character_name, :hue => :character_hue } , :text => "角色脸谱" ,:show => Help::XP_IMAGE_SHOW, :split => Help::XP_IMAGE_SPLIT } )
		Builder.Add(:image , {:actual => {:name => :battler_name, :hue => :battler_hue } , :text => "战斗图", :show => Help::XP_IMAGE_SPLIT, :split => Help::XP_IMAGE_SPLIT })
			Builder.Order
			Builder.Next
		Builder.Add(:group, :text => "初期装备") do 
		end
	end
	list.Value = Data["actor"]
end


=begin
id 
ID。

name 
名称。

class_id 
职业 ID。

initial_level 
初期等级。

final_level 
最终等级。

exp_basis 
EXP 曲线的基本值（10..50）。

exp_inflation 
EXP 曲线的增加度（10..50）。

character_name 
人物图像的文件名。

character_hue 
人物图像的色相变化值（0..360）。

battler_name 
战斗图像的文件名。

battler_hue 
战斗图像的色相变化值（0..360）。

parameters 
包含了各等级基本参数的二维数组（Table）。

具体来说应该是 parameters[kind, level] 的形式。

kind 是参数的种类（0：MaxHP，1：MaxSP，2：力量，3：灵巧，4：速度，5：魔力）。

weapon_id 
初期装备的武器的 ID。

armor1_id 
初期装备的盾的 ID。

armor2_id 
初期装备的头部防具的 ID。

armor3_id 
初期装备的身体防具的 ID。

armor4_id 
初期装备的装饰品的 ID。

weapon_fix 
武器的装备固定标记。

armor1_fix 
盾的装备固定标记。

armor2_fix 
头部防具的装备固定标记。

armor3_fix 
身体防具的装备固定标记。

armor4_fix 
装饰品的装备固定标记。
=end
