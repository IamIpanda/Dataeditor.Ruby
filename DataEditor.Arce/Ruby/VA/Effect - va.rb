# This file is in coding: utf-8
# Arce Script : Effect - va.rb
# Desribe the fuxking effect view in the RPG Maker VX Ace

require "Ruby/Fuzzy.rb"

class RPG
	class UsableItem
		class Effect
		 def initialize(code = 0, data_id = 0, value1 = 0.0, value2 = 0.0)
		  @code = code
		  @data_id = data_id
		  @value1 = value1
		  @value2 = value2
		 end
		 attr_accessor :code
		 attr_accessor :data_id
		 attr_accessor :value1
		 attr_accessor :value2
		 end
	end
end
class VA_Help
	class Effect
		@@abilities = { 
			0 => "体力上限",
			1 => "魔力上限",
			2 => "物理攻击",
			3 => "物理防御",
			4 => "魔法攻击",
			5 => "魔法防御",
			6 => "敏捷值",
			7 => "幸运值"
		 }
		@@names = { 
			11 => "回复体力值",
			12 => "恢复魔力值",
			13 => "增加特技值",
			21 => "附加状态",
			22 => "解除状态",
			31 => "强化能力",
			32 => "弱化能力",
			33 => "解除能力强化",
			34 => "解除能力弱化",
			41 => "特殊效果",
			42 => "能力提升",
			43 => "学会技能",
			44 => "公共事件"
		 }
		def self.build_effect
			columns = ["类型", "内容"]
			texts = []			
			texts[0] = Text.new do |*args|
				@@names[args[0]["@code"].Value.to_s.to_i].encode
			end
			texts[1] = Text.new do |*args|
				arg = args[0]
				code = arg["@code"].Value
				data_id = arg["@data_id"].Value
				value1 = arg["@value1"].Value
				value2 = arg["@value2"].Value
				case code
				when 11, 12
					first = (value1 * 100).to_i
					second = value2.to_i
					if first == 0
						"#{ second }"
					elsif second == 0
						"#{ first } %"
					else
						"#{ first } % + #{ second }"
					end
				when 13
					# What the fuck the programmer is thinking when they write this ?
					"#{ value1.to_i } %"
				when 21, 22
					target = data_id == 0 ? "普通攻击".encode : Data["state"][data_id]["@name"].Text
					"[#{ target }] #{ (value1 * 100).to_i } %"
				when 31, 32
					target = @@abilities[data_id.to_s.to_i].encode
					"[#{ target }] #{ value1.to_i } 回合"
				when 33, 34
					target = @@abilities[data_id]
					"[#{ target }]".encode
				when 41
					"撤退".encode
				when 42
					target = @@abilities[data_id.to_s.to_i].encode
					"[#{ target }] + #{ value1.to_i }"
				when 43
					"[" + Data["skill"][data_id]["@name"].Text + "]"
				when 44
					"[" + Data["commonevent"][data_id]["@name"].Text + "]"
				end
		end
			window = Proc.new do |window, target|
				Builder.In(window)
				Builder.Add(:tabs) do
					Builder.Add(:tab ,{ text: "恢复" }) do
						radio(11) do
							VA_Help::Effect.int1
							Builder.Text(" + ")
							VA_Help::Effect.int2
						end
						VA_Help::Effect.radio(12) do
							VA_Help::Effect.int1
							Builder.Text(" + ")
							VA_Help::Effect.int2
						end
						VA_Help::Effect.radio(13) do
							VA_Help::Effect.fucking_brain_broken_programmer
						end
					end
					Builder.Add(:tab ,{ text: "能力" }) do
						VA_Help::Effect.radio(21) do
							VA_Help::Effect.choose("state", true)
							VA_Help::Effect.int1
						end
						VA_Help::Effect.radio(22) do
							VA_Help::Effect.choose("state", true)
							VA_Help::Effect.int1
						end
					end
					Builder.Add(:tab ,{ text: "状态" }) do
						VA_Help::Effect.radio(31) do
							VA_Help::Effect.ability
							VA_Help::Effect.int1(" 回合".encode)
						end
						VA_Help::Effect.radio(32) do
							VA_Help::Effect.ability
							VA_Help::Effect.int1(" 回合".encode)
						end
						VA_Help::Effect.radio(33) do
							VA_Help::Effect.ability
						end
						VA_Help::Effect.radio(34) do
							VA_Help::Effect.ability
						end
					end
					Builder.Add(:tab ,{ text: "其他" }) do
						VA_Help::Effect.radio(41) do
							Builder.Add(:choose, { actual: :data_id, label: 0, choice: { 0 => "撤退" } })
						end
						VA_Help::Effect.radio(42) do
							VA_Help::Effect.ability
							Builder.Text(" + ")
							VA_Help::Effect.fucking_brain_broken_programmer(" 点")
						end
						VA_Help::Effect.radio(43) { VA_Help::Effect.choose("skill") }
						VA_Help::Effect.radio(44) { VA_Help::Effect.choose("commonevent") }
					end
				end
				Builder.Out
				window.Value = target
			end
			Builder.Add(:view, { 
				 actual: :effects,
				 label: 0,
				 columns: columns, 
				 catalogue: texts, 
				 window: window, 
				 new: RPG::UsableItem::Effect.new.to_fuzzy 
				 })
		end
		def self.int1(text = " % ")
			Builder.Order(1)
			Builder.Add(:float, { actual: :value1, label: 0, digits: 0, times: 100.0 })
			Builder.Text(text)
			Builder.Next
		end
		def self.fucking_brain_broken_programmer(text = " % ")
			Builder.Order(1)
			Builder.Add(:float, { actual: :value1, label: 0, digits: 0, times: 1.0 })
			Builder.Text(text)
			Builder.Next
		end
		def self.int2
			Builder.Add(:int, { actual: :value2, label: 0 })
			Builder.Text(" 点")
		end
		def self.choose(key, with = false)
			choice = {  }
			choice[0] = "普通攻击" if (with)
			choice[nil] = Filechoice.new(key)
			Builder.Add(:choose, { actual: :data_id, label: 0 ,choice: choice })
		end
		def self.ability
			Builder.Order(1)
			Builder.Add(:choose, { actual: :data_id, label: 0 ,choice: @@abilities } )
			Builder.Next
		end
		def self.radio(value, &block)
			Builder.Add(:radio, { actual: :code, key: value, text: @@names[value] ,group: "VA_POPWINDOW_EFFECT_GROUP" }, &block)
		end
	end
end