# This file is in coding: utf-8
# Arce Script : Damage - va.rb
# Desribe the fuxking damage view in the RPG Maker VX Ace

require "Ruby/Fuzzy.rb"

class VA_Help
	class Damage
		@@fuz = [0, 0, 0, false].to_fuzzy
		def self.build_damage
			Builder.Add(:metro, { actual: :damage, text: "伤害" }) do
				Builder.Order
				Builder.Add(:choose, { actual: :type, text: "类型", choice: {
					0 => "无",
					1 => "HP 伤害",
					2 => "MP 伤害",
					3 => "HP 回复",
					4 => "MP 回复",
					5 => "HP 吸收",
					6 => "MP 吸收"
					} })
				Builder.Add(:choose, { actual: :element_id, text: "属性", choice: {
					-1 => "普通攻击",
					0 => "无",
					nil => Fileselect.new(Data["system"]["@elements"])
					} })
				Builder.Next
				target = Builder.Add(:text, { actual: :formula, text: "计算公式" })
				Builder.Next
				Builder.Add(:int, { actual: :variance, text: "离散度" })
				Builder.Add(:bool_choose, { actual: :critical, text: "允许必杀" })
				proc = Proc.new do |control, args|
					window = Builder.Add(:dialog)
					Builder.In(window)
					Builder.Order
					Builder.Add(:int, { actual: :INDEX0, text: "基础伤害" })
					Builder.Add(:int, { actual: :INDEX1, text: "物理关系度" })
					Builder.Add(:int, { actual: :INDEX2, text: "魔法关系度" })
					Builder.Next
					Builder.Add(:check, { actual: :INDEX3, text: "忽略目标的防御力" })
					Builder.Out
					window.Value = @@fuz
					if(window.ShowAndTell)
						texts = []
						data0 = @@fuz[0].Value
						data1 = @@fuz[1].Value
						data2 = @@fuz[2].Value
						ignore = @@fuz[3].Value
						texts.push data0
						texts.push "a.atk * " + (data1 * 0.4).to_s + (ignore ? "" : " - b.def * " + (data1 * 0.2).to_s) if data1 > 0
						texts.push "a.mat * " + (data1 * 0.4).to_s + (ignore ? "" : " - b.mde * " + (data1 * 0.2).to_s) if data2 > 0
						args[0].Binding.Text = texts.join(" + ")
						args[0].Push
					end
				end
				Builder.Add(:button, { text: "简易设置...", parameter: [target], run: proc })
			end
		end
	end
end
