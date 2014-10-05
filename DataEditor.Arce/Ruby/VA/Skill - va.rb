# This file is in coding: utf-8
# Arce Script : Skill - va.rb
# Describe the user interface for Skill in RPG Maler VX Ace

require "Ruby/VA/File - va.rb"
require "Ruby/VA/Damage - va.rb"
require "Ruby/VA/Effect - va.rb"

tab = Builder.Add(:tab, { text: "技能" }) do
	Builder.Add(:list, { textbook: Help.Get_Default_Text ,text: "技能" }) do
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
				actual: :stype_id,
				text: "技能类型",
				choice: {
					0 => "（无）",
					nil => Fileselect.new(Data["system"]["@skill_types"])
				}})

			Builder.Add(:int, { actual: :mp_cost, text: "魔力值消耗" })
			Builder.Add(:int, { actual: :tp_cost, text: "特技值消耗" })
				Builder.Next
			Builder.Add(:choose, { actual: :scope, text: "效果范围", choice: {
				0 => "无",
				1 => "单个敌人",
				2 => "一个随机敌人",
				3 => "两个随机敌人",
				4 => "三个随机敌人",
				5 => "四个随机敌人",
				6 => "单个队友",
				7 => "全体队友",
				9 => "单个队友（无法战斗）",
				10 => "全体队友（无法战斗）",
				11 => "使用者"
				} })
			Builder.Add(:choose, { actual: :occasion, text: "使用场合", choice: {
				0 => "随时可用",
				1 => "仅战斗中",
				2 => "仅菜单中",
				3 => "不可使用"
				} })
		end
		Builder.Add(:metro, { text: "使用" }) do
				Builder.Order
			Builder.Add(:int, { actual: :speed, text: "速度修正" })
			Builder.Add(:int, { actual: :success_rate, text: "成功几率" })
			Builder.Add(:int, { actual: :repeats, text: "连续次数" })
			Builder.Add(:int, { actual: :tp_gain, text: "获取特技值" })
				Builder.Next
			Builder.Add(:choose, { actual: :hit_type, text: "命中类型", choice: {
				0 => "必定命中",
				1 => "魔法攻击",
				2 => "物理攻击"
				} })
			Builder.Add(:choose, { 
				actual: :animation_id ,
			 	text: "动画" ,
				choice: { nil => Filechoice.new("animation") } 
			 })
		end
		Builder.Add(:metro, { text: "使用者的信息" }) do
				Builder.Order
			Builder.Text("（使用者名称）")
			box = Builder.Add(:text, { actual: :message1, label: 0, width: 225 })
				Builder.Next
			Builder.Add(:text, { actual: :message2, label: 0, width: 320 })
				Builder.Next
			proc = Proc.new do |control, args|
				box = args[0]
				text = args[1]
				box.Binding.Text = text + control.Parent["@name"].Text + "!"
				box.Push
			end
			Builder.Add(:button, { text: "“吟唱了~”", parameter: [box, "吟唱了"], run: proc })
			Builder.Add(:button, { text: "“施放了~”", parameter: [box, "施放了"], run: proc })
			Builder.Add(:button, { text: "“使用了~”", parameter: [box, "使用了"], run: proc })
		end
		Builder.Add(:metro, { text: "武器类型" }) do
			Builder.Order
			Builder.Add(:choose, { 
				actual: :required_wtype_id1, 
				text: "武器类型 1",
				choice: {
					0 => "无",
					nil => Fileselect.new(Data["system"]["@weapon_types"])
			} })
			Builder.Add(:choose, { 
				actual: :required_wtype_id2, 
				text: "武器类型 2",
				choice: {
					0 => "无",
					nil => Fileselect.new(Data["system"]["@weapon_types"])
			} })
		end
			Builder.Next
		VA_Help::Damage.build_damage
		Builder.Add(:metro, :text => "使用效果") do
			VA_Help::Effect.build_effect
		end
		Builder.Add(:metro, :text => "备注") do
			Builder.Add(:text, { actual: :note, label: 0, width: 275, height: 165 })
		end
	end
end
tab.Value = Data["skill"]

=begin
stype_id 
スキルタイプ ID。

mp_cost 
消費 MP。

tp_cost 
消費 TP。

message1 
message2 
使用時メッセージ。

required_wtype_id1 
required_wtype_id2 
必要武器タイプ。

属性
id 
ID。

name 
名前。

icon_index 
アイコン番号。

description 
説明。

features 
特徴リスト。RPG::BaseItem::Feature の配列です。

note 
メモ。
属性
scope 
効果範囲。

0 : なし 
1 : 敵単体 
2 : 敵全体 
3 : 敵 1 体 ランダム 
4 : 敵 2 体 ランダム 
5 : 敵 3 体 ランダム 
6 : 敵 4 体 ランダム 
7 : 味方単体 
8 : 味方全体 
9 : 味方単体 (戦闘不能) 
10 : 味方全体 (戦闘不能) 
11 : 使用者 
occasion 
使用可能時。

0 : 常時 
1 : バトルのみ 
2 : メニューのみ 
3 : 使用不可 
speed 
速度補正。

success_rate 
成功率。

repeats 
連続回数。

tp_gain 
得 TP。

hit_type 
命中タイプ。

0 : 必中 
1 : 物理攻撃 
2 : 魔法攻撃 
animation_id 
アニメーション ID。

damage 
ダメージ (RPG::UsableItem::Damage) 。

effects 
使用効果リスト。RPG::UsableItem::Effect の配列です。

	
=end
