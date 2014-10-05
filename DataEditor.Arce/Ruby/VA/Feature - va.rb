# This file is in coding: utf-8
# Arce Script : feature - va.rb
# Desribe the fuxking feture view in the RPG Maker VX Ace

require "Ruby/Fuzzy.rb"

class RPG
	class BaseItem
		class Feature
			def initialize
				@code = 0
				@data_id = 0
				@value_id = 0.0
			end
		end
	end
end

class VA_Help
	class Feature
		@@elements = [
				"体力上限",
				"魔力上限",
				"物理攻击",
				"物理防御",
				"魔法攻击",
				"魔法防御",
				"敏捷值",
				"幸运值"
			]
		@@abilities = [
				"物理命中几率",
				"物理闪避几率",
				"必杀几率",
				"必杀闪避几率",
				"魔法闪避几率",
				"魔法反射几率",
				"物理反击几率",
				"体力值再生速度",
				"魔力值再生速度",
				"特技值再生速度"
			]
		@@specials = [
				"受到攻击几率",
				"防御效果比率",
				"恢复效果比率",
				"药理知识",
				"魔力值消耗率",
				"特技值补充率",
				"物理伤害加成",
				"魔法伤害加成",
				"地形伤害加成",
				"经验获取加成"
		]
		@@equipments = [
				"武器",
				"盾牌",
				"头盔",
				"铠甲",
				"饰品"
			]
		@@battleflags = [
				"自动战斗",
				"擅长防御",
				"保护弱者",
				"特技专注"
			]
		@@disappears = [
				"首领",
				"瞬间消失",
				"不消失"
			]
		@@outs = [
				"遇敌几率减半",
				"随机遇敌无效",
				"敌人偷袭无效",
				"先制攻击几率上升",
				"获得金钱几率双倍",
				"物品掉落几率双倍"
			]
		def self.build_feature
			columns = ["类型","内容"]
			texts = []
			texts[0] = Text.new do |*args|
				text = { 
					11 => "物理抗性",
					12 => "弱化抗性",
					13 => "状态抗性",
					14 => "状态免疫",
					21 => "普通能力",
					22 => "添加能力",
					23 => "特殊能力",
					31 => "攻击附加属性",
					32 => "攻击附加状态",
					33 => "修正攻击速度",
					34 => "增加攻击次数",
					41 => "添加技能类型",
					42 => "禁用技能类型",
					43 => "添加技能",
					44 => "禁用技能",
					51 => "装备武器类型",
					52 => "装备护甲类型",
					53 => "固定装备",
					54 => "禁用装备",
					55 => "装备风格",
					61 => "增加行动次数",
					62 => "特殊标志",
					63 => "消失效果",
					64 => "队伍能力"
				 }
				text[args[0]["@code"].Value.to_s.to_i].encode
			end
			texts[1] = Text.new do |*args|
				arg = args[0]
				code = arg["@code"].Value
				data_id = arg["@data_id"].Value
				value = arg["@value"].Value
				part0 = ""
				part1 = ""
				part2 = (value * 100).to_i.to_s + "%"
				add = true
				case code
				when 11
					part0 = Data["system"]["@elements"][data_id].Text
					part1 = " * "
				when 12, 21
					part0 = @@elements[data_id].encode
					part1 = " * "
				when 13
					part0 = Data["state"][data_id]["@name"].Text
					part1 = " * "
				when 14
					part0 = Data["state"][data_id]["@name"].Text
				when 22
					part0 = @@abilities[data_id].encode
						part1 = " + "
				when 23
					part0 = @@specials[data_id].encode
					part1 = " * "
				when 31
					part0 = Data["system"]["@elements"][data_id].Text
				when 32
					part0 = Data["state"][data_id]["@name"].Text
					part1 = " + "
				when 33, 34
					part0 = value.to_i.to_s
					add = false
				when 41, 42
					part0 = Data["system"]["@skill_types"][data_id].Text
				when 43, 44
					part0 = Data["skill"][data_id]["@name"].Text
				when 51
					part0 = Data["system"]["@weapon_types"][data_id].Text
				when 52
					part0 = Data["system"]["@armor_types"][data_id].Text
				when 53, 54
					part0 = @@equipments[data_id].encode
				when 55
					part0 = "双持武器".encode
					add = false
				when 61
					part0 = part2
					add = false
				when 62
					part0 = @@battleflags[data_id].encode
					add = false
				when 63
					part0 = @@disappears[data_id].encode
					add = false
				when 64
					part0 = @@outs[data_id].encode
					add = false
				end
				part0 = "[#{ part0 }]" if add
				if part1 == ""
					part0
				else
					part0 + part1 + part2
				end
			end
			window = Proc.new do |window, target|
				Builder.In(window)
				Builder.Add(:tabs) do
					Builder.Add(:tab ,{ text: "抗性" }) do
						VA_Help::Feature.radio(11, "属性抗性") do
						 VA_Help::Feature.choose({ nil => Fileselect.new(Data["system"]["@elements"]) })
							Builder.Text(" * ")
							VA_Help::Feature.percent
						end
						VA_Help::Feature.radio(12, "弱化抗性") do
							VA_Help::Feature.choose( VA_Help::Feature.turn_to_dictionary("elements") )
							Builder.Text(" * ")
							VA_Help::Feature.percent
						end
						VA_Help::Feature.radio(13, "状态抗性") do
							VA_Help::Feature.choose( { nil => Filechoice.new("state") } )
							Builder.Text(" * ")
							VA_Help::Feature.percent
						end
						VA_Help::Feature.radio(14, "状态免疫") do
							VA_Help::Feature.choose( { nil => Filechoice.new("state") } )
						end
					end
					Builder.Add(:tab, { text: "能力" }) do
						VA_Help::Feature.radio(21, "普通能力") do
							VA_Help::Feature.choose( VA_Help::Feature.turn_to_dictionary("elements") )
							Builder.Text(" * ")
							VA_Help::Feature.percent
						end
						VA_Help::Feature.radio(22, "添加能力") do
							VA_Help::Feature.choose( VA_Help::Feature.turn_to_dictionary("abilities") )
							Builder.Text(" + ")
							VA_Help::Feature.percent
						end
						VA_Help::Feature.radio(23, "特殊能力") do
							VA_Help::Feature.choose( VA_Help::Feature.turn_to_dictionary("specials") )
							Builder.Text(" * ")
							VA_Help::Feature.percent
						end
					end
					Builder.Add(:tab, { text: "攻击" }) do
						VA_Help::Feature.radio(31, "攻击附加属性") do
						 VA_Help::Feature.choose({ nil => Fileselect.new(Data["system"]["@elements"]) })
						end
						VA_Help::Feature.radio(32, "攻击附加状态") do
						 VA_Help::Feature.choose({ nil => Filechoice.new("state") })
						 Builder.Text(" + ")
						 VA_Help::Feature.percent
						end
						VA_Help::Feature.radio(33, "修正攻击速度") { Builder.Add(:float, {:actual => :value, :label => 0, :digits => 0 }) }
						VA_Help::Feature.radio(34, "增加攻击次数") { Builder.Add(:float, {:actual => :value, :label => 0, :digits => 0 }) }
					end
					Builder.Add(:tab, { text: "技能" }) do
						VA_Help::Feature.radio(41, "添加技能类型") { VA_Help::Feature.choose({ nil => Fileselect.new(Data["system"]["@skill_types"] )} ) }
						VA_Help::Feature.radio(42, "禁止技能类型") { VA_Help::Feature.choose({ nil => Fileselect.new(Data["system"]["@skill_types"] )} ) }
						VA_Help::Feature.radio(43, "添加技能") { VA_Help::Feature.choose({ nil => Filechoice.new("skill")} ) }
						VA_Help::Feature.radio(44, "禁用技能") { VA_Help::Feature.choose({ nil => Filechoice.new("skill")} ) }
					end
					Builder.Add(:tab, { text: "装备" }) do
						VA_Help::Feature.radio(51, "添加武器类型") { VA_Help::Feature.choose({ nil => Fileselect.new(Data["system"]["@weapon_types"]) }) }
						VA_Help::Feature.radio(52, "禁用武器类型") { VA_Help::Feature.choose({ nil => Fileselect.new(Data["system"]["@weapon_types"]) }) }
						VA_Help::Feature.radio(53, "固定装备") { VA_Help::Feature.choose( VA_Help::Feature.turn_to_dictionary("equipments") ) }
						VA_Help::Feature.radio(54, "禁用装备") { VA_Help::Feature.choose( VA_Help::Feature.turn_to_dictionary("equipments") ) }
						VA_Help::Feature.radio(55, "禁用装备") { VA_Help::Feature.choose( 0 => "双持武器" ) }
					end
					Builder.Add(:tab, { text: "特殊" }) do
						VA_Help::Feature.radio(61, "增加行动次数") { VA_Help::Feature.percent }
						VA_Help::Feature.radio(62, "特殊标志") { VA_Help::Feature.choose( VA_Help::Feature.turn_to_dictionary("battleflags") ) }
						VA_Help::Feature.radio(63, "消失效果") { VA_Help::Feature.choose( VA_Help::Feature.turn_to_dictionary("disappears") ) }
						VA_Help::Feature.radio(64, "队伍能力") { VA_Help::Feature.choose( VA_Help::Feature.turn_to_dictionary("outs") ) }
					end
				end
				Builder.Out
				window.Binding.Width = 400
				window.Binding.Height = 300
				window.Value = target
			end
			Builder.Add(:view, { 
				actual: :features,
				 label: 0,
				 columns: columns, 
				 catalogue: texts, 
				window: window, 
				width: 275,
				height: 350,
				new: RPG::BaseItem::Feature.new.to_fuzzy 
				 })
		end
		def self.choose(choices)
			Builder.Order
			Builder.Add(:choose, { actual: :data_id, text: "", label: 0, choice: choices })
			Builder.Next
		end
		def self.percent
			Builder.Add(:float, { actual: :value, label: 0, digits: 0, times: 100.0 })
			Builder.Text("%")
		end
		def self.radio(value, text, &block)
			Builder.Add(:radio, { actual: :code, key: value, text: text ,group: "VA_POPWINDOW_FEATURE_GROUP" }, &block)
		end
		def self.turn_to_dictionary(array, start = 0)
			ans = { }
			array = eval("@@#{ array }") if array.is_a?(String)
			array.each_index { |index| ans[index + start] = array[index] }
			ans
		end
	end
end