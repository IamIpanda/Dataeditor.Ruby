# This file is in coding: utf-8
# Arce Script : feature - va.rb
# Desribe the class page in the RPG Maker VX Ace

require "Ruby/VA/File - va.rb"
require "Ruby/VA/Feature - va.rb"

Builder.Add(:tab, { text: "职业" }) do
	list = Builder.Add(:list, { text: "职业" ,textbook: Help.Get_Default_Text }) do 
		Builder.Add(:metro, { text: "基本设置" }) do
			Builder.order
			Builder.Add(:text, { actual: :name, text: "名称" })
			Builder.Add(:metro, { actual: :exp_params }) do
				Builder.Add(:exp, { actual: {
				:arg0 => :INDEX0, 
				:arg1 => :INDEX1, 
				:arg2 => :INDEX2,
				:arg3 => :INDEX3},
			 text: "经验值曲线", min: 10, max: 50,
				value: Proc.new do |*args|
						lv = args[0].to_f
				  basis = args[1][0].to_f
				  extra = args[1][1].to_f
				  acc_a = args[1][2].to_f
				  acc_b = args[1][3].to_f
				  s1 = (basis * ((lv - 1) ** (0.9 + acc_a / 250)) * lv * (lv + 1) / (6 + lv ** 2 / 50 / acc_b) + (lv - 1) * extra).round.to_i
				  lv += 1
				  (basis * ((lv - 1) ** (0.9 + acc_a / 250)) * lv * (lv + 1) / (6 + lv ** 2 / 50 / acc_b) + (lv - 1) * extra).round.to_i - s1
					end
					 })
			end
		end
		Builder.Add(:metro, { text: "能力值成长曲线" }) do
			Builder.Add(:actor_parameters, :actual => :params) do
					Builder.Order
				Builder.Add(:actor, { index: 0, text: "体力上限" ,color: Painter[16], max_number: 9999 })
				Builder.Add(:actor, { index: 1, text: "魔力上限" ,color: Painter[18], max_number: 9999 })
				Builder.Add(:actor, { index: 2, text: "物理攻击" ,color: Painter[19], max_number: 999 })
				Builder.Add(:actor, { index: 3, text: "物理防御" ,color: Painter[21], max_number: 999 })
					Builder.Next
				Builder.Add(:actor, { index: 4, text: "魔法攻击" ,color: Painter[22], max_number: 999 })
				Builder.Add(:actor, { index: 5, text: "魔法防御" ,color: Painter[23], max_number: 999 })
				Builder.Add(:actor, { index: 6, text: "敏捷值" ,color: Painter[24], max_number: 999 })
				Builder.Add(:actor, { index: 7, text: "幸运值" ,color: Painter[25], max_number: 999 })
			end
		end
		Builder.Add(:metro, { text: "技能" }) do
			window = Proc.new do |window, value|
					Builder.In(window)
					Builder.Order
				Builder.Add(:int, { actual: :level, text: "等级" })
				Builder.Add(:choose, { actual: :skill_id, text: "学会的特技", choice: { nil => Filechoice.new("skill") } })
					Builder.Next
				Builder.Add(:text, { actual: :note, text: "备注" })
					Builder.Out
					window.Value = value
			end
			texts = []
			texts[0] = Text.new { |target, watch, i, j, k| "Lv.#{target["@level"].Value}" }
			texts[1] = Text.new do |target, watch, i, j, k|
				skill_id = target["@skill_id"].Value
				target = Data["skill"][skill_id]
				ans = Help.Auto_Get_Text(target, watch)
				ans
			end 
			texts[2] = Text.new { |target, watch, i, j, k| target["@note"].Text }
			Builder.Add(:view, { 
				 actual: :learnings,
				 text: "特技",
				 columns: ["等级", "学会的特技", "备注"], 
				 catalogue: texts,
				 window: window,
				 window_type: 1,
				 new: nil })
		end

			Builder.Next
		Builder.Add(:metro, { text: "特性" }) do
			VA_Help::Feature.build_feature
		end
		Builder.Add(:metro, { text: "备注" }) do
			Builder.Add(:text, { actual: :note, label: 0, height: 400, width: 600 })
		end

	end
	list.Value = Data["class"]
end
=begin
exp_params 
経験値曲線を決定する数値の配列。添字は以下の通りです。

0 : 基本値 
1 : 補正値 
2 : 増加度 A 
3 : 増加度 B 
params 
能力値成長曲線。各レベルに対応する通常能力値を格納した二次元配列 (Table) です。

params[param_id, level] という形をとり、param_id は以下の割り当てになります。

0 : 最大HP 
1 : 最大MP 
2 : 攻撃力 
3 : 防御力 
4 : 魔法力 
5 : 魔法防御 
6 : 敏捷性 
7 : 運 
learnings 
習得するスキル。RPG::Class::Learning の配列です。

=end