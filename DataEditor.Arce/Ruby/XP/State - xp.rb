# This file is in coding: utf-8
# Arce Script : State = xp.rb
# Describe the user interface for Item

require "Ruby/XP/File - xp.rb"
tab = Builder.Add(:tab, { text: "状态" }) do
	Builder.Add(:list, { textbook: Help.Get_Default_Text, text: "状态", default: RPG::State.new }) do
		Builder.Add(:group, { text: "" }) do
			Builder.Order
			Builder.Add(:metro) do
				Builder.Add(:text, { actual: :name, text: "名称" })
				choice = Filechoice.new("animation")
				Builder.Add(:choose, { actual: :animation_id, text: "动画" ,choice: { nil => choice } })
				Builder.Add(:choose, { actual: :restriction, text: "限制" ,choice: {
					0 => "无",
					1 => "不能使用魔法",
					2 => "普通攻击敌人",
					3 => "普通攻击同伴",
					4 => "不行动"
					} })
				Builder.Next
			end
			Builder.Add(:group) do
				Builder.Add(:check, { actual: :nonresistance, text: "不能抵抗" })
				Builder.Add(:check, { actual: :zero_hp, text: "当作 HP 为 0" })
				Builder.Add(:check, { actual: :cant_get_exp, text: "不能获得 EXP" })
				Builder.Add(:check, { actual: :cant_evade, text: "不能回避攻击" })
				Builder.Add(:check, { actual: :slip_damage, text: "连续伤害" })
			end
			Builder.Next
			Builder.Add(:int, { actual: :rating, text: "定量" })
			Builder.Add(:int, { actual: :hit_rate, text: "命中率 %" })
			Builder.Add(:int, { actual: :maxhp_rate, text: "MaxHP %" })
			Builder.Add(:int, { actual: :maxsp_rate, text: "MaxSP %" })
			Builder.Next
			Builder.Add(:int, { actual: :str_rate, text: "力量 %" })
			Builder.Add(:int, { actual: :dex_rate, text: "灵巧 %" })
			Builder.Add(:int, { actual: :agi_rate, text: "速度 %" })
			Builder.Add(:int, { actual: :int_rate, text: "魔力 %" })
			Builder.Next
			Builder.Add(:int, { actual: :atk_rate, text: "攻击力 %" })
			Builder.Add(:int, { actual: :pdef_rate, text: "物理防御 %" })
			Builder.Add(:int, { actual: :mdef_rate, text: "魔法防御 %" })
			Builder.Add(:int, { actual: :eva, text: "回避修正 %" })
			Builder.Next
			Builder.Add(:group, { text: "解除条件" }) do
				Builder.Order
				Builder.Add(:check, { actual: :battle_only, text: "战斗结束时解除" })
				Builder.Next(4)
				Builder.Add(:int, { actual: :hold_turn, label: 0 })
				Builder.Text("回合经过后", 0, 4)
				Builder.Add(:int, { actual: :auto_release_prob, label: 0 })
				Builder.Text("% 的概率解除", 0, 4)
				Builder.Next(4)
				Builder.Text("受到物理攻击后", 0, 4)
				Builder.Add(:int, { actual: :shock_release_prob, label: 0 })
				Builder.Text("% 的概率解除", 0, 4)
			end
			Builder.OrderAndNext
			text = Text.new { |*args| args[0].Text }
			Builder.Add(:checklist, { actual: :guard_element_set ,text: "属性", data: Data["system"]["@elements"], textbook: text })
			Builder.Add(:bufflist, { actual: {"+" => :plus_state_set, "-" => :minus_state_set},text: "状态变化",
				data: Data["state"], textbook: Help.Get_Silence_Text, default: "" })
		end
	end
end
tab.Value = Data["state"]
=begin
RPG::State
id 
ID。

name 
名称。

animation_id 
动画 ID。

restriction 
限制（0：无，1：不能使用魔法，2：普通攻击敌人，3：普通攻击同伴，4：不行动）。

nonresistance 
选项「不能抵抗」的真伪值。

zero_hp 
选项「当作 HP 为 0 的状态」的真伪值。

cant_get_exp 
选项「无法获得 EXP」的真伪值。

cant_evade 
选项「不能回避攻击」的真伪值。

slip_damage 
选项「连续伤害」的真伪值。

rating 
额定值（0..10）。

hit_rate 
命中率 %。

maxhp_rate 
MaxHP %。

maxsp_rate 
MaxSP %。

str_rate 
力量 %。

dex_rate 
灵巧 %。

agi_rate 
速度 %。

int_rate 
魔力 %。

atk_rate 
攻击力 %。

pdef_rate 
物理防御 %。

mdef_rate 
魔法防御 %。

eva 
回避修正。

battle_only 
是否战斗结束时解除的真伪值。

hold_turn 
auto_release_prob 
hold_turn 回合经过后 auto_release_prob % 的概率解除。

shock_release_prob 
受到物理攻击后 shock_release_prob % 的概率解除。

guard_element_set 
属性防御。为属性 ID 的数组。

plus_state_set 
附加状态。为状态 ID 的数组。

minus_state_set 
解除状态。为状态 ID 的数组。


=end