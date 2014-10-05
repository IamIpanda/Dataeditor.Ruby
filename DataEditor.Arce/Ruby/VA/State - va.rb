# This file is in coding: utf-8
# Arce Script : State - va.rb
# Describe the user interface for State in RPG Maker VX Ace

require "Ruby/VA/File - va.rb"
Builder.Add(:tab, { text: "状态" }) do
	list = Builder.Add(:list, { textbook: Help.Get_Default_Text, text: "状态" }) do
		Builder.Add(:metro, { text: "基本设置" }) do
			Builder.Add(:text, { actual: :name, text: "名称" })
			Builder.Add(:choose, { 
				actual: :restriction, 
				text: "行动制约",
				choice: {
					0 => "无法行动",
					1 => "攻击敌人",
					2 => "攻击双方",
					3 => "攻击队友",
					4 => "不行动"
					} })
				Builder.Next
			Builder.Add(:icon, { 
				actual: :icon_index,
				image: "Graphics/System/Iconset",
				text: "图标",
				version: "RPGVXAce",
				split: Help::ICON_SPLIT,
				label: 2
			 })
			Builder.Add(:int, { actual: :priority, text: "显示优先级" })
		end
		Builder.Add(:metro, { text: "解除条件" }) do
				Builder.Order
			Builder.Add(:check, { actual: :remove_at_battle_end, text: "战斗结束时解除" })
			Builder.Add(:check, { actual: :remove_by_restriction, text: "获得其他限制性状态时解除" })
				Builder.Next
			Builder.Add(:choose, { 
				actual: :auto_removal_timing, 
				text: "未命名" ,
				choice: {
					0 => "无",
					1 => "行动结束时",
					2 => "回合结束时"
					} })
				Builder.Next
			Builder.Add(:int, { actual: :min_turns, text: "持续回合数" })
				Builder.Text(" ~ ")
			Builder.Add(:int, { actual: :max_turns, label: 0 })
				Builder.Next
			Builder.Add(:check, { actual: :remove_by_damage, text: "受到伤害时解除" })
			Builder.Add(:int, { actual: :chance_by_damage, label: 0 })
				Builder.Text("%")
				Builder.Next
			Builder.Add(:check, { actual: :remove_by_walking, text: "一定步数后解除" })
			Builder.Add(:int, { actual: :steps_to_remove, label: 0 })
				Builder.Text("步")
		end
		Builder.Add(:metro, { text: "该状态附加到队友时的信息" }) do
			Builder.Order
			Builder.Text("（目标的名字）")
			Builder.Add(:text, { actual: :message1, label: 0 })
		end
		Builder.Add(:metro, { text: "该状态附加到敌人时的信息" }) do
			Builder.Order
			Builder.Text("（目标的名字）")
			Builder.Add(:text, { actual: :message2, label: 0 })
		end
		Builder.Add(:metro, { text: "该状态持续时的信息" }) do
			Builder.Order
			Builder.Text("（目标的名字）")
			Builder.Add(:text, { actual: :message3, label: 0 })
		end
		Builder.Add(:metro, { text: "该状态解除时的信息" }) do
			Builder.Order
			Builder.Text("（目标的名字）")
			Builder.Add(:text, { actual: :message4, label: 0 })
		end
			Builder.Next
		Builder.Add(:metro, { text: "特性" }) do
			VA_Help::Feature.build_feature
		end
		Builder.Add(:metro, { text: "备注" }) do
			Builder.Add(:text, { actual: :note, label: 0, width: 275, height: 165 })
		end
	end
	list.Value = Data["state"]
end


=begin

属性
restriction 
行動制約。

0 : なし 
1 : 敵を攻撃する 
2 : 敵か味方を攻撃する 
3 : 味方を攻撃する 
4 : 行動できない 
priority 
表示優先度 (0..100) 。

remove_at_battle_end 
戦闘終了時に解除 (true / false) 。

remove_by_restriction 
行動制約によって解除 (true / false) 。

auto_removal_timing 
自動解除のタイミング。

0 : なし 
1 : 行動終了時 
2 : ターン終了時 
min_turns 
max_turns 
継続ターン数の最小値と最大値。

remove_by_damage 
ダメージで解除 (true / false) 。

chance_by_damage 
ダメージで解除される確率 (%) 。

remove_by_walking 
歩数で解除 (true / false) 。

steps_to_remove 
解除されるまでの歩数。

message1 
message2 
message3 
message4 
メッセージ。上から、味方、敵、継続、解除。

=end