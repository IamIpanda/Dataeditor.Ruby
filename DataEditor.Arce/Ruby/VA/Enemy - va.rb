# This file is in coding: utf-8
# Arce Script : State = xp.rb
# Describe the user interface for Item in RPG Maker VX Ace

require "Ruby/VA/File - va.rb"
require "Ruby/Fuzzy.rb"

class RPG
	class Enemy
		class Action
		  def initialize
		    @skill_id = 1
		    @condition_type = 0
		    @condition_param1 = 0
		    @condition_param2 = 0
		    @rating = 5
		  end
		  attr_accessor :skill_id
		  attr_accessor :condition_type
		  attr_accessor :condition_param1
		  attr_accessor :condition_param2
		  attr_accessor :rating
	  end
	end
end



Builder.Add(:tab , { :text => "敌人" }) do
	list = Builder.Add(:list, {:textbook => Help.Get_Default_Text, :text => "敌人"}) do
		Builder.Add(:metro, { :text => "基本设置" }) do
			Builder.Add(:text , {:actual => :name , :text => "名称" })
			Builder.Add(:image,{
			  :actual => {
					:name => :battler_name, 
					:hue => :battler_hue
					},
			 	:text => "战斗图",
			 	:path => "Graphics/Battlers",
			 	:version => "RPGVXAce",
			 	:show => Help::XP_IMAGE_SPLIT, 
			 	:split => Help::XP_IMAGE_SPLIT 
			 	})
				Builder.Next
			Builder.Add(:metro , {:actual => :params }) do
				Builder.Add(:int , {:actual => :INDEX0 , :text => "最大 HP" })
				Builder.Add(:int , {:actual => :INDEX2 , :text => "物理攻击" })
				Builder.Add(:int , {:actual => :INDEX4 , :text => "魔法攻击" })
				Builder.Add(:int , {:actual => :INDEX6 , :text => "敏捷值" })
					Builder.Next
				Builder.Add(:int , {:actual => :INDEX1 , :text => "最大 MP" })
				Builder.Add(:int , {:actual => :INDEX3 , :text => "物理防御" })
				Builder.Add(:int , {:actual => :INDEX5 , :text => "魔法防御" })
				Builder.Add(:int , {:actual => :INDEX7 , :text => "幸运值" })
			end
		end
		Builder.Add(:metro) do
			Builder.Order
			Builder.Add(:metro , { :text => "战利品" }) do
				Builder.Add(:int , {:actual => :exp , :text => "经验" })
				Builder.Add(:int , {:actual => :gold , :text => "掉落金钱" })
			end
			Builder.Add(:metro , {:actual => :drop_items , :text => "掉落物品" }) do
				text = Text.new do |target|
					id = target["@data_id"].Value
					denominator = target["@denominator"].Value
					case target["@kind"].Value
					when 0
						str = "（无）".encode
					when 1
						item_name = Data["item"][id]["@name"].Text
						str = "#{item_name} : 1 / #{denominator}"
					when 2
						weapon_name = Data["weapon"][id]["@name"].Text
						str = "#{weapon_name} : 1 / #{denominator}"
					when 3
						armor_name = Data["armor"][id]["@name"].Text
						str = "#{armor_name} : 1 / #{denominator}"
					end
					str
				end
				window = Proc.new do |window, target|
					Builder.In(window)
					Builder.Add(:radio , {
						:actual => :kind,
						:text => "无",
						:key => 0,
						:group => "VA_ENEMY_DROPITEM_POP" })
					Builder.Add(:radio , {
						:actual => :kind,
						:text => "物品",
						:key => 1,
						:group => "VA_ENEMY_DROPITEM_POP" }) do
							Builder.Add(:choose , {
								:actual => :data_id, 
								:label => 0, 
								:choice => { nil => Filechoice.new("item") }
								})
					end
					Builder.Add(:radio , {
						:actual => :kind,
						:text => "武器",
						:key => 2,
						:group => "VA_ENEMY_DROPITEM_POP" }) do
							Builder.Add(:choose , {
								:actual => :data_id, 
								:label => 0, 
								:choice => { nil => Filechoice.new("weapon") }
								})
					end
					Builder.Add(:radio , {
						:actual => :kind,
						:text => "防具",
						:key => 3,
						:group => "VA_ENEMY_DROPITEM_POP" }) do
							Builder.Add(:choose , {
								:actual => :data_id, 
								:label => 0, 
								:choice => { nil => Filechoice.new("armor") }
								})
					end
					Builder.Add(:metro , { :text => "掉率" }) do
						Builder.Order
						Builder.Text("1 / ")
						Builder.Add(:int , {:actual => :denominator , :label => 0 })
					end
					Builder.Out
					window.Value = target
				end
				Builder.Add(:drop , { :label => 0 , :textbook => text, :window => window, :actual => :INDEX0 })
				Builder.Add(:drop , { :label => 0 , :textbook => text, :window => window, :actual => :INDEX1 })
				Builder.Add(:drop , { :label => 0 , :textbook => text, :window => window, :actual => :INDEX2 })
			end
		end
		Builder.Add(:metro, { :text => "行为模式" }) do
			columns = ["技能", "行动条件", "R"]
			texts = []
			texts[0] = Text.new do |target, watch, i, j, k|
				Data["skill"][target["@skill_id"]]["@name"].Text
			end
			texts[2] = Text.new do |target, watch, i, j, k|
				target["@rating"].Value.to_s
			end
			texts[1] = Text.new do |target, watch, i, j, k|
				para1 = target["@condition_param1"].Value
				para2 = target["@condition_param2"].Value
				case target["@condition_type"].Value
				when 0
					"平时".encode
				when 1
					if para1 != 0
						if para2 != 1
							"回合 #{para1} + #{para2}X".encode
						else
							"回合 #{para1}".encode
						end
					elsif para2 != 1
						"回合 #{para2}X".encode
					end
				when 2
					"体力值 #{para1} ~ #{para2}".encode
				when 3
					"魔力值 #{para1} ~ #{para2}".encode
				when 4
					state = Data["state"][para1]["@name"].Text
					"状态 [".encode + state + "]".encode
				when 5
					"队伍等级 >= #{para1} ".encode
				when 6
					"开关 #{para1} == ON".encode
				end
			end
			window = Proc.new do |window, target|
				Builder.In(window)
					Builder.Order
					Builder.Add(:choose , {:actual => :skill_id , :text => "技能", :choice => { nil => Filechoice.new("skill") } })
					Builder.Add(:int , {:actual => :rating , :text => "优先级" })
					Builder.Next
					Builder.Add(:metro , { :text => "行动条件" }) do
						Builder.Add(:radio , {
							:actual => :condition_type,
							:text => "平时", 
							:group => "VX_ENEMY_ACTION_POP",
							:key => 0 
						})
						Builder.Add(:radio , {
							:actual => :condition_type,
						  :text => "回合数", 
						  :group => "VX_ENEMY_ACTION_POP",
						  :key => 1
						}) do
							Builder.Order
							Builder.Add(:int , {:actual => :condition_param1 , :label => 0 })
							Builder.Text(" + ")
							Builder.Add(:int , {:actual => :condition_param2 , :label => 0 })
							Builder.Text(" * n ")
						end
						Builder.Add(:radio , {
							:actual => :condition_type, 
							:text => "体力值", 
							:group => "VX_ENEMY_ACTION_POP",
							:key => 2
						}) do
							Builder.Order
							Builder.Add(:int , {:actual => :condition_param1 , :label => 0 })
							Builder.Text(" % ~ ")
							Builder.Add(:int , {:actual => :condition_param2 , :label => 0 })
							Builder.Text(" %")
						end
						Builder.Add(:radio , {
							:actual => :condition_type, 
							:text => "魔力值", 
							:group => "VX_ENEMY_ACTION_POP",
							:key => 3
						}) do
							Builder.Order
							Builder.Add(:int , {:actual => :condition_param1 , :label => 0 })
							Builder.Text(" % ~ ")
							Builder.Add(:int , {:actual => :condition_param2 , :label => 0 })
							Builder.Text(" %")
						end
						Builder.Add(:radio , {
							:actual => :condition_type, 
							:text => "状态", 
							:group => "VX_ENEMY_ACTION_POP",
							:key => 4
						}) do
							Builder.Add(:choose , {
								:actual => :condition_param1, 
								:label => 0, 
								:choice => { nil => Filechoice.new("state") } 
							})
						end
						Builder.Add(:radio , {
							:actual => :condition_type, 
							:text => "队伍等级", 
							:group => "VX_ENEMY_ACTION_POP",
							:key => 5
						}) do
							Builder.Order
							Builder.Add(:int , {:actual => :condition_param1 , :label => 0 })
							Builder.Text("或更高")
						end
					end
				Builder.Out
				window.Value = target
			end
			Builder.Add(:view , {
				:actual => :actions,
				:label => 0,
				:columns => columns, 
				:catalogue => texts,
				:window => window, 
				:new => RPG::Enemy::Action.new.to_fuzzy })
		end
			Builder.Next
		Builder.Add(:metro , { :text => "特性" }) do
			VA_Help::Feature.build_feature
		end
		Builder.Add(:metro, { :text => "备注" }) do
			Builder.Add(:text , {:actual => :note , :label => 0, :height => 400, :width => 600 })
		end
	end
	list.Value = Data["enemy"]
end

=begin
属性
battler_name 
戦闘グラフィックのファイル名。

battler_hue 
戦闘グラフィックの色相変化値 (0..360) 。

params 
能力値。以下の ID を添字とする整数の配列です。

0 : 最大HP 
1 : 最大MP 
2 : 攻撃力 
3 : 防御力 
4 : 魔法力 
5 : 魔法防御 
6 : 敏捷性 
7 : 運 
exp 
経験値。

gold 
お金。

drop_items 
ドロップアイテム。RPG::Enemy::DropItem の配列です。

actions 
行動パターン。RPG::Enemy::Action の配列です。

=end