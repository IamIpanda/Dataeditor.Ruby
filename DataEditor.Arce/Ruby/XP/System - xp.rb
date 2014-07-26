# This file is in coding: utf-8
# Arce Script : System - xp.rb
# Describe the user interface for system

require "Ruby/XP/File - xp.rb"
require "Ruby/Fuzzy.rb"

Builder.Add(:tab, { text: "系统" }) do
		group = Builder.Add(:group) do
				columns = ["角色"]
				texts = []
				texts[0] = Text.new do |target, watch, i, j, k|
					actor = Data["actor"][target]
					Help.Auto_Get_Text(actor, watch)
				end
				window = Proc.new do |window, target|
						Builder.In(window)
						Builder.Space(5, 5)
						window.Text = "初期同伴".encode
					Builder.Add(:choose, { actual: "", text: "角色", choice: { nil => Filechoice.new("actor")} })
						Builder.Out
						window.value = target
				end
				after = Proc.new do |array, new|
					i = array.Count - 1
					for j in array
						i = -1 if j.Value == new.Value
					end
					i
				end
			Builder.Add(:view, { actual: :party_members ,text: "初期同伴", catalogue: texts, after: after, 
				columns: columns, catalogue: texts, window: window, window_type: 1, new: 1.to_fuzzy, width: 140, height: 100  })
				text = Text.new { |i, target| sprintf("%03d:%s", i, target) }
			Builder.Add(:paper, { actual: :elements, text: "属性", textbook: text })
				Builder.Next
			Builder.Add(:group, { text: "系统 图形 \/ BGM \/ ME \/ SE" }) do
				Builder.Add(:oldimage, { actual: {:name => :windowskin_name}, text: "窗口外观图形" ,path: "Graphics/WindowSkins/" })
				Builder.Add(:oldimage, { actual: {:name => :title_name}, text: "标题画面图形" ,path: "Graphics/Titles" })
				Builder.Add(:oldimage, { actual: {:name => :gameover_name}, text: "游戏结束图形" ,path: "Graphics/GameOvers" })
				Builder.Add(:oldimage, { actual: {:name => :battle_transition}, text: "战斗渐变图形" ,path: "Graphics/Transitions" })
				Builder.Add(:audio, { actual: :title_bgm, text: "标题 BGM" ,type: "BGM" })
				Builder.Add(:audio, { actual: :battle_bgm, text: "战斗 BGM" ,type: "BGM" })
				Builder.Add(:audio, { actual: :battle_end_me, text: "战斗结束 ME" ,type: "ME" })
				Builder.Add(:audio, { actual: :gameover_me, text: "游戏结束 ME" ,type: "ME" })
				Builder.Add(:audio, { actual: :cursor_se, text: "光标 SE" ,type: "SE" })
				Builder.Add(:audio, { actual: :decision_se, text: "确定 SE" ,type: "SE" })
					Builder.Next
				Builder.Add(:audio, { actual: :cancel_se, text: "取消 SE" ,type: "SE" })
				Builder.Add(:audio, { actual: :buzzer_se, text: "警告 SE" ,type: "SE" })
				Builder.Add(:audio, { actual: :equip_se, text: "装备 SE" ,type: "SE" })
				Builder.Add(:audio, { actual: :shop_se, text: "商店 SE" ,type: "SE" })
				Builder.Add(:audio, { actual: :save_se, text: "存档 SE" ,type: "SE" })
				Builder.Add(:audio, { actual: :load_se, text: "读档 SE" ,type: "SE" })
				Builder.Add(:audio, { actual: :battle_start_se, text: "战斗开始 SE" ,type: "SE" })
				Builder.Add(:audio, { actual: :escape_se, text: "逃跑 SE" ,type: "SE" })
				Builder.Add(:audio, { actual: :actor_collapse_se, text: "角色受伤 SE" ,type: "SE" })
				Builder.Add(:audio, { actual: :enemy_collapse_se, text: "敌人受伤 SE" ,type: "SE" })
			end
				Builder.Next
			Builder.Add(:group, { text: "用语", actual: :words }) do
				Measurement.Set("text", 65, 20)
				Builder.Add(:text, { actual: :gold, text: "G（货币单位）" })
				Builder.Add(:text, { actual: :hp, text: "HP" })
				Builder.Add(:text, { actual: :sp, text: "SP" })
				Builder.Add(:text, { actual: :str, text: "力量" })
				Builder.Add(:text, { actual: :dex, text: "灵巧" })
				Builder.Add(:text, { actual: :agi, text: "速度" })
				Builder.Add(:text, { actual: :int, text: "魔力" })
				Builder.Add(:text, { actual: :atk, text: "攻击力" })
				Builder.Add(:text, { actual: :pdef, text: "物理防御" })
				Builder.Add(:text, { actual: :mdef, text: "魔法防御" })
					Builder.Next
				Builder.Add(:text, { actual: :weapon, text: "武器" })
				Builder.Add(:text, { actual: :armor1, text: "盾" })
				Builder.Add(:text, { actual: :armor2, text: "头" })
				Builder.Add(:text, { actual: :armor3, text: "身体" })
				Builder.Add(:text, { actual: :armor4, text: "装饰品" })
				Builder.Add(:text, { actual: :attack, text: "攻击" })
				Builder.Add(:text, { actual: :skill, text: "特技" })
				Builder.Add(:text, { actual: :guard, text: "防御" })
				Builder.Add(:text, { actual: :item, text: "物品" })
				Builder.Add(:text, { actual: :equip, text: "装备" })
			end
		end
	group.Value = Data["system"]
end

=begin
属性magic_number 
更新检查用的幻数。每次 Maker 保存数据都会写入不同的值。

party_members 
初期同伴。是角色 ID 的数组。

elements 
属性名称目录。是以属性 ID 为索引的字符串数组，其 0 号单元为 nil。

switches 
开关名称目录。是以开关 ID 为索引的字符串数组，其 0 号单元为 nil。

variables 
变量名称目录。是以变量 ID 为索引的字符串数组，其 0 号单元为 nil。

windowskin_name 
窗口皮肤图像的文件名。

title_name 
标题图像的文件名。

gameover_name 
游戏结束图像的文件名。

battle_transition 
战斗切换时显示的渐变图像的文件名。

title_bgm 
标题 BGM（RPG::AudioFile）。

battle_bgm 
战斗 BGM（RPG::AudioFile）。

battle_end_me 
战斗结束 ME（RPG::AudioFile）。

gameover_me 
游戏结束 ME（RPG::AudioFile）。

cursor_se 
光标 SE（RPG::AudioFile）。

decision_se 
确定 SE（RPG::AudioFile）。

cancel_se 
取消 SE（RPG::AudioFile）。


buzzer_se 
警告 SE（RPG::AudioFile）。


equip_se 
装备 SE（RPG::AudioFile）。


shop_se 
商店 SE（RPG::AudioFile）。


save_se 
存档 SE（RPG::AudioFile）。


load_se 
读档 SE（RPG::AudioFile）。


battle_start_se 
战斗开始 SE（RPG::AudioFile）。


escape_se 
逃跑 SE（RPG::AudioFile）。


actor_collapse_se 
角色受伤 SE（RPG::AudioFile）。


enemy_collapse_se 
敌人受伤 SE（RPG::AudioFile）。


words 
用语（RPG::System::Words）。

start_map_id 
主角初期位置的地图 ID。

start_x 
主角初期位置的地图 X 座标。

start_y 
主角初期位置的地图 Y 座标。

test_battlers 
战斗测试的同伴设定。RPG::System::TestBattler 的数组。

test_troop_id 
战斗测试的队伍 ID。

battleback_name 
战斗测试和 Maker 内部使用的战斗背景图像的文件名。

battler_name 
Maker 内部使用的战斗者图像的文件名。

battler_hue 
Maker 内部使用的战斗者图像的色相变化值（0..360）。

edit_map_id 
Maker 内部使用的现在编辑的地图 ID。

=end