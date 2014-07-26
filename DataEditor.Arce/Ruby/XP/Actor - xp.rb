# This File is in coding: utf-8
# Arce Script: actor - xp.rb
# describe the user interface of actor

require "Ruby/XP/File - xp.rb"

tab = Builder.Add(:tab, { text: "角色" }) do
	Builder.Add(:list, { textbook: Help.Get_Default_Text, text: "角色" }) do
		Builder.Add(:group, { text: "" }) do
				Builder.Order
			Builder.Add(:text, { actual: :name, text: "名称" })
				Builder.Next
				choice = Filechoice.new("class")
			Builder.Add(:choose, { actual: :class_id, text: "职业", choice: { nil => choice } })
				Builder.Next
			Builder.Add(:int, { actual: :initial_level, text: "初始等级" })
			Builder.Add(:int, { actual: :final_level, text: "最终等级" })
				Builder.Next
				exp_proc = Proc.new do |*args|
					i = args[0] + 1
					pow_i = 2.4 + args[1][1] / 100.0
					args[1][0] * ((i + 3.0) ** pow_i) / (5.0 ** pow_i)
				end
			Builder.Add(:exp, { actual: {:base => :exp_basis, :inflation => :exp_inflation}, text: "EXP 曲线", min: 10, max: 50, value: exp_proc })
				Builder.Next
			Builder.Add(:image, { actual: {:name => :character_name, :hue => :character_hue }, 
				text: "角色脸谱", path: "Graphics/Characters", show: Help::XP_IMAGE_SHOW, split: Help::XP_IMAGE_SPLIT } )
				Builder.Next
			Builder.Add(:image, { actual: {:name => :battler_name, :hue => :battler_hue } ,
			 	text: "战斗图", path: "Graphics/Battlers", show: Help::XP_IMAGE_SPLIT, split: Help::XP_IMAGE_SPLIT })
				Builder.OrderAndNext
			Builder.Add(:actor_parameters, :actual => :parameters) do
				Builder.Order
				Builder.Add(:actor, { index: 0, text: "MaxHP" ,color: Painter[16], max_number: 9999 })
				Builder.Add(:actor, { index: 1, text: "MaxSP" ,color: Painter[17], max_number: 9999 })
					Builder.Next
				Builder.Add(:actor, { index: 2, text: "力量" ,color: Painter[19], max_number: 999 })
				Builder.Add(:actor, { index: 3, text: "速度" ,color: Painter[20], max_number: 999 })
					Builder.Next
				Builder.Add(:actor, { index: 4, text: "灵巧" ,color: Painter[22], max_number: 999 })
				Builder.Add(:actor, { index: 5, text: "敏捷" ,color: Painter[23], max_number: 999 })
			end
				Builder.Next
			Builder.Add(:group, :text => "初期装备") do 
				Builder.Order
				Builder.Add(:check, { actual: :weapon_fix, text: "武器固定" })
				Builder.Add(:lazy_choose, { actual: :weapon_id, label: 0, textbook: Help.Get_Default_Text, choice: { 0 => "（无）" }, 
					source: Proc.new do |target, parent, control|
						Data["weapon"][Data["class"][parent["@class_id"]]["@weapon_set"]]
					end })
					Builder.Next
				Builder.Add(:check, { actual: :armor1_fix, text: "盾固定" })
				Builder.Add(:lazy_choose, { actual: :armor1_id, label: 0, textbook: Help.Get_Default_Text, choice: { 0 => "（无）" }, 
					source: Proc.new do |target, parent, control|
						Data["armor"][Data["class"][parent["@class_id"]]["@armor_set"]].select {|target| target["@kind"].Value == 0}
					end })
					Builder.Next
				Builder.Add(:check, { actual: :armor2_fix, text: "头部固定" })
				Builder.Add(:lazy_choose, { actual: :armor2_id, label: 0, textbook: Help.Get_Default_Text, choice: { 0 => "（无）" }, 
					source: Proc.new do |target, parent, control|
						Data["armor"][Data["class"][parent["@class_id"]]["@armor_set"]].select {|target| target["@kind"].Value == 1}
					end })
					Builder.Next
				Builder.Add(:check, { actual: :armor3_fix, text: "防具固定" })
				Builder.Add(:lazy_choose, { actual: :armor3_id, label: 0, textbook: Help.Get_Default_Text, choice: { 0 => "（无）" }, 
					source: Proc.new do |target, parent, control|
						Data["armor"][Data["class"][parent["@class_id"]]["@armor_set"]].select {|target| target["@kind"].Value == 2}
					end })
					Builder.Next
				Builder.Add(:check, { actual: :armor4_fix, text: "饰品固定" })
				Builder.Add(:lazy_choose, { actual: :armor4_id, label: 0, textbook: Help.Get_Default_Text, choice: { 0 => "（无）" }, 
					source: Proc.new do |target, parent, control|
						Data["armor"][Data["class"][parent["@class_id"]]["@armor_set"]].select {|target| target["@kind"].Value == 3}
					end })
			end
		end
	end
end
tab.Value = Data["actor"]


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
