# This file is in coding: utf-8
# Arce Script : Armor - va.rb
# Describe the user interface for Armor in RPG Maker VX Ace

require "Ruby/VA/File - va.rb"
require "Ruby/VA/Feature - va.rb"

tab = Builder.Add(:tab, {  text: "护甲" }) do
	Builder.Add(:list, { textbook: Help.Get_Default_Text ,text: "护甲" }) do
		Builder.Add(:metro, { text: "基本设置" }) do
			Builder.Order
			Builder.Add(:text, { actual: :name, text: "名称" })
			Builder.Add(:icon, { 
				actual: :icon_index,
				image: "Graphics/System/Iconset",
				text: "图标",
				version: "RPGVXAce",
				split: Help::ICON_SPLIT,
				label: 2
				})
			Builder.Next
			Builder.Add(:text, { actual: :description, text: "说明", width: 320, Height: 35 })
			Builder.Next
			Builder.Add(:choose,{ 
				actual: :atype_id,
				text: "护甲类型",
				choice: {
					0 => "无",
					nil => Fileselect.new(Data["system"]["@armor_types"])
					} })
			Builder.Add(:int, { actual: :price, text: "价格" })
			Builder.Next
			Builder.Add(:choose, { 
				actual: :etype_id,
				text: "装备位置", 
				choice: {
					1 => "盾牌",
					2 => "头盔",
					3 => "盔甲",
					4 => "装饰品"
					} })
		end
		Builder.Add(:metro, { text: "能力的变化值", actual: :params }) do
			Builder.Order
			Builder.Add(:int, { actual: :INDEX0, text: "物理攻击" })
			Builder.Add(:int, { actual: :INDEX1, text: "物理防御" })
			Builder.Add(:int, { actual: :INDEX2, text: "魔法攻击" })
			Builder.Add(:int, { actual: :INDEX3, text: "魔法防御" })
			Builder.Next
			Builder.Add(:int, { actual: :INDEX4, text: "敏捷值" })
			Builder.Add(:int, { actual: :INDEX5, text: "幸运值" })
			Builder.Add(:int, { actual: :INDEX6, text: "体力上限" })
			Builder.Add(:int, { actual: :INDEX7, text: "魔力上限" })
		end
		Builder.Next
		Builder.Add(:metro, { text: "特性" }) do
			VA_Help::Feature.build_feature
		end
		Builder.Add(:metro, { text: "备注" }) do
			Builder.Add(:text, { actual: :note, label: 0, width: 275, height: 165 })
		end
	end
end
tab.Value = Data["armor"]
