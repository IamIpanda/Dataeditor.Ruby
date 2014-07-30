# This file is in coding: utf-8
# Arce Script : Skill - xp.rb
# Describe the user interface for Skill

require "Ruby/XP/File - xp.rb"
tab = Builder.Add(:tab, { text: "技能" }) do
	Builder.Add(:list, { textbook: Help.Get_Default_Text ,text: "技能", default: RPG::Skill.new }) do
		Builder.Add(:group, { text: "" }) do
				Builder.Order
			Builder.Add(:text, { actual: :name, text: "名称" })
			Builder.Add(:oldimage, { actual: {:name => :icon_name}, text: "图标", path: "Graphics/Icons/" })
				Builder.Next
			Builder.Add(:text, { actual: :description, text: "说明", width: 270 })
				Builder.Next
			Builder.Add(:choose, { actual: :scope, text: "效果范围", choice: {
				0 => "无",
				1 => "敌方单体",
				2 => "地方全体",
				3 => "己方单体",
				4 => "己方全体",
				5 => "己方单体（HP 0）",
				6 => "己方全体（HP 0）",
				7 => "使用者"
				} })
			Builder.Add(:choose, { actual: :occasion, text: "可能使用时", choice: {
				0 => "平时",
				1 => "战斗中",
				2 => "菜单中",
				3 => "不能使用"
				} })
				Builder.Next
				choice = Filechoice.new("animation")
			Builder.Add(:choose, { actual: :animation1_id, text: "使用方的动画", choice: { 0 => "（无）" ,nil => choice } })	
			Builder.Add(:choose, { actual: :animation2_id, text: "对象方的动画", choice: { 0 => "（无）" ,nil => choice } })
				Builder.Next
			Builder.Add(:audio, { actual: :menu_se, text: "菜单画面时使用的SE", type: :SE })	
			Builder.Add(:choose, { actual: :common_event_id, text: "公共事件", choice: { 0 => "（无）" ,nil => Filechoice.new("commonEvent") } })
				Builder.Next

			Builder.Add(:int, { actual: :sp_cost, text: "消费 SP" })
			Builder.Add(:int, { actual: :power, text: "威力" })
			Builder.Add(:int, { actual: :atk_f, text: "攻击力 F" })
			Builder.Add(:int, { actual: :eva_f, text: "回避 F" })
				Builder.Next
			Builder.Add(:int, { actual: :str_f, text: "力量 F" })
			Builder.Add(:int, { actual: :dex_f, text: "灵巧 F" })
			Builder.Add(:int, { actual: :agi_f, text: "速度 F" })
			Builder.Add(:int, { actual: :int_f, text: "魔力 F" })
				Builder.Next
			Builder.Add(:int, { actual: :hit, text: "命中率" })
			Builder.Add(:int, { actual: :pdef_f, text: "物理防御 F" })
			Builder.Add(:int, { actual: :mdef_f, text: "魔法防御 F" })
			Builder.Add(:int, { actual: :variance, text: "分散度" })
				Builder.OrderAndNext
				text = Text.new { |*args| args[0].Text }
			Builder.Add(:checklist, { actual: :element_set ,text: "属性", data: Data["system"]["@elements"], textbook: text })
		 Builder.Add(:bufflist, { actual: {"+" => :plus_state_set, "-" => :minus_state_set},text: "状态变化",
			 data: Data["state"], textbook: Help.Get_Silence_Text, default: "" })
		end		
	end
end
tab.Value = Data["skill"]

=begin
id 
ID。

name 
名称。

icon_name 
图标图像的文件名。

description 
说明。

scope 
效果范围（0：无，1：敌单体，2：敌全体，3：己方单体，4：己方全体，5：己方单体（HP 0），6：己方全体（HP 0），7：使用者）。

occasion 
可使用的场合（0：平时，1：战斗中，2：菜单中，3：不能使用）。

animation1_id 
使用方的动画 ID。

animation2_id 
对象方的动画 ID。

menu_se 
菜单画面使用时的 SE（RPG::AudioFile）。

common_event_id 
公共事件 ID。

sp_cost 
消费 SP。

power 
威力。

atk_f 
攻击力 F。

eva_f 
回避 F。

str_f 
力量 F。

dex_f 
灵巧 F。

agi_f 
速度 F。

int_f 
魔力 F。

hit 
命中率。

pdef_f 
物理防御 F。

mdef_f 
魔法防御 F。

variance 
分散度。

element_set 
属性。为属性 ID 的数组。

plus_state_set 
附加状态。为状态 ID 的数组。

minus_state_set 
解除状态。为状态 ID 的数组。


=end