# This file is in coding: utf-8
# Arce Script : Enemy - xp.rb
# Describe the user interface for enemy

require "Ruby/XP/File - xp.rb"
Builder.Add(:tab , { :text => "敌人" }) do
	list = Builder.Add(:list, {:textbook => Help.Get_Default_Text ,:text => "敌人"}) do
			Builder.Order
		Builder.Add(:metro) do
			Builder.Add(:text , {:actual => :name , :text => "名称" })			
			Builder.Add(:image , {:actual => {:name => :battler_name, :hue => :battler_hue } ,
			 	:text => "战斗图", :path => "Graphics/Battlers", :show => Help::XP_IMAGE_SPLIT, :split => Help::XP_IMAGE_SPLIT })
				choice = Filechoice.new("animation")
			Builder.Add(:choose , {:actual => :animation1_id , :text => "攻击方的动画", :choice => { nil => choice }})
				Builder.Order
			Builder.Add(:int , {:actual => :exp , :text => "EXP" })
			Builder.Add(:int , {:actual => :gold , :text => "掉落金钱" })
		end
		Builder.Add(:metro) do
			Builder.Order
			Builder.Add(:int , {:actual => :maxhp , :text => "MaxHP" })
			Builder.Add(:int , {:actual => :maxsp , :text => "MaxSP" })
				Builder.Next
			Builder.Add(:int , {:actual => :str , :text => "力量" })
			Builder.Add(:int , {:actual => :dex, :text => "灵巧" })
				Builder.Next
			Builder.Add(:int , {:actual => :agi , :text => "速度" })
			Builder.Add(:int , {:actual => :int , :text => "敏捷" })
				Builder.Next
			Builder.Add(:int , {:actual => :atk , :text => "攻击力" })
			Builder.Add(:int , {:actual => :pdef , :text => "物理防御" })
				Builder.Next
			Builder.Add(:int , {:actual => :mdef , :text => "魔法防御" })
			Builder.Add(:int , {:actual => :eva , :text => "回避修正" })
				Builder.Next
				choice = Filechoice.new("animation")
			Builder.Add(:choose , {:actual => :animation2_id , :text => "对象方的动画", :choice => { nil => choice } })
				Builder.Next
				text = Text.new do |target|
					item = target["@item_id"].Value
					weapon = target["@weapon_id"].Value
					armor = target["@armor_id"].Value
					percent = target["@treasure_prob"].Value
					if item != 0
						item_name = Data["item"][item]["@name"].Text
						str = "#{percent} % #{item_name}"
					elsif weapon != 0
						weapon_name = Data["weapon"][weapon]["@name"].Text
						str = "#{percent} % #{weapon_name}"
					elsif armor != 0
						armor_name = Data["armor"][armor]["@name"].Text
						str = "#{percent} % #{armor_name}"
					else
						str = "（无）".encode
					end
					str
				end
				window = Proc.new do |window, target|
					puts target
						Builder.In(window)
						ison = Proc.new do |value, parent, key|
							parent["@item_id"].Value == 0 && parent["@weapon_id"].Value == 0 && parent["@armor_id"].Value == 0
						end
					Builder.Add(:radio , {:text => "无", :ison => ison})
						ison = Proc.new do |value, parent, key|
							parent["@item_id"].Value != 0
						end
						deny = Proc.new do |value, parent, key|
							parent["@weapon_id"].Value = 0
							parent["@armor_id"].Value = 0
						end
					Builder.Add(:radio , {:text => "物品", :ison => ison, :deny => deny}) do
						Builder.Add(:choose , {:actual => :item_id , :label => 0 ,:choice => { nil => Filechoice.new("item") }})
					end
						ison = Proc.new do |value, parent, key|
							parent["@weapon_id"].Value != 0
						end
						deny = Proc.new do |value, parent, key|
							parent["@item_id"].Value = 0
							parent["@armor_id"].Value = 0
						end
					Builder.Add(:radio , {:text => "武器", :ison => ison, :deny => deny}) do
						Builder.Add(:choose , {:actual => :weapon_id , :label => 0 ,:choice => { nil => Filechoice.new("weapon") }})
					end
						ison = Proc.new do |value, parent, key|
							parent["@armor_id"].Value != 0
						end
						deny = Proc.new do |value, parent, key|
							parent["@item_id"].Value = 0
							parent["@weapon_id"].Value = 0
						end
					Builder.Add(:radio , {:text => "防具", :ison => ison, :deny => deny}) do
						Builder.Add(:choose , {:actual => :armor_id , :label => 0 ,:choice => { nil => Filechoice.new("armor") }})
					end
					Builder.Add(:int , {:actual => :treasure_prob , :text => "掉落概率" })
						Builder.Out
						window.value = target
				end
			Builder.Add(:drop , { :text => "掉落物品" , :textbook => text, :window => window})
		end
				text = Text.new { |*args| args[0] }
			Builder.Add(:textlist , {:actual => :element_ranks, :text => "属性有效度", :textbook => text, :height => 350,
				:choices => ["A","B","C","D","E","F"], :value => [1,2,3,4,5,6,7], :default => 3, :data => Data["system"]["@elements"] })
				text = Text.new { |*args| args[0]["@name"] }
			Builder.Add(:textlist , {:actual => :state_ranks , :text => "状态有效度" , :textbook => text, :height => 350,
				:choices => ["A","B","C","D","E","F"], :value => [1,2,3,4,5,6,7], :default => 3, :data => Data["state"]})
				Builder.Next
			# 下面是坑爹的敌人行动的生成代码
			# 栏位
			columns = ["行为","事件出现条件"]
			# 文档
			texts = []
			texts[0] = Text.new do |target, watch, i, j, k|
				kind = target["@kind"].Value
				if kind == 0 
					str = ["攻击","防御","逃跑","什么也不做"][target["@basic"].Value]
				elsif kind == 1 
					str = Data["skill"][target["@skill_id"].Value]["@name"].Text
				else 
					str = "未知行动"
				end
				str.encode
			end
			texts[1] = Text.new do |target, watch, i, j, k|
				str = []
				# 回合
				condition_a = target["@condition_turn_a"].Value
				condition_b = target["@condition_turn_b"].Value
				if condition_a != 0
					if condition_b != 1
						str.push "回合 #{condition_a} + #{condition_b} *".encode
					else
						str.push "回合 #{condition_a}".encode
					end
				else
					if condition_b != 1
						str.push "回合 #{condition_b} *".encode
					end
				end
				# HP
				condition_hp = target["@condition_hp"].Value
				str.push("HP #{condition_hp} 以下".encode) if condition_hp != 100
				# Level
				condition_level = target["@condition_level"].Value
				str.push("等级 #{condition_level} 以上".encode) if condition_level != 1
				# Switch (Fixme!)
				condition_switch_id = target["@condition_switch_id"].Value
				str.push("开关 #{condition_switch_id} 为 ON".encode) if condition_switch_id != 0
				ans = str.join(" & ")
				ans = "（无）".encode if ans == ""
				ans
			end
			# 窗口
			window = Proc.new do |window, target|
				Builder.In(window)
					# 四个事件框
					Builder.Add(:group, { :text => "事件出现条件" }) do
							proc = Proc.new do |target| 
								target["@condition_turn_a"].Value = 0
								target["@condition_turn_b"].Value = 1
							end
							ison = Proc.new do |target|
								target["@condition_turn_a"].Value != 0 && target["@condition_turn_b"].Value != 1
							end
						Builder.Add(:check_container , { :text => "回合" , :deny => proc, :ison => ison } ) do
								Builder.Order
							Builder.Add(:int , {:actual => :condition_turn_a , :label => 0 })
							Builder.Text("+")
							Builder.Add(:int , {:actual => :condition_turn_b , :label => 0})
							Builder.Text("X")
						end
							proc = Proc.new { |target| target["@condition_hp"].Value = 100 }
							ison = Proc.new { |target| target["@condition_hp"].Value != 100 }
						Builder.Add(:check_container, {:text => "HP", :deny => proc, :ison => ison}) do
								Builder.Order
							Builder.Add(:int , {:actual => :condition_hp , :label => 0 })
							Builder.Text(" \% 以下".encode)
						end
							proc = Proc.new { |target| target["@condition_level"].Value = 1 }
							ison = Proc.new { |target| target["@condition_level"].Value != 1 }
						Builder.Add(:check_container, {:text => "等级", :deny => proc, :ison => ison }) do
								Builder.Order
							Builder.Add(:int , {:actual => :condition_level , :label => 0 })
							Builder.Text("以上".encode)
						end
							proc = Proc.new { |target| target["@condition_switch_id"].Value = 0 }
							ison = Proc.new { |target| target["@condition_switch_id"].Value != 0 }
						Builder.Add(:check_container, {:text => "开关", :deny => proc, :ison => ison }) do
							Builder.Text("为 ON".encode)
						end
					end
					Builder.Add(:group, { :text => "行为" }) do
						Builder.Add(:radio, {:actual => :kind, :group => "Enemy_Action_Behave", :text => "基本", :key => 0}) do
							Builder.Add(:choose, {:actual => :basic , :label => 0, :choice => { 0 => "攻击", 1 => "逃跑", 2 => "防御", 3 => "什么也不做"}})
						end
						Builder.Add(:radio, {:actual => :kind, :group => "Enemy_Action_Behave", :text => "特技", :key => 1}) do
							Builder.Add(:choose , {:actual => :skill_id , :label => 0, :choice => { nil => Filechoice.new("skill")} })
						end
					end
					Builder.Add(:group, { :text => "概率" }) do
						Builder.Add(:scrollint , {:actual => :rating , :label => 0, :minvalue => 0, :maxvalue => 10 })
					end
				Builder.Out
				window.Value = target
			end
		Builder.Add(:view , {:actual => :actions , :text => "行动" ,:columns => columns, 
			:catalogue => texts, :window => window, :new => nil })
	end
	list.Value = Data["enemy"]
end
=begin
id 
ID。

name 
名称。

battler_name 
战斗者图像的文件名。

battler_hue 
战斗者图像的色相变化值（0..360）。

maxhp 
MaxHP。

maxsp 
MaxSP。

str 
力量。

dex 
灵巧。

agi 
速度。

int 
魔力。

atk 
攻击力。

pdef 
物理防御。

mdef 
魔法防御。

eva 
回避修正。

animation1_id 
攻击方的动画 ID。

animation2_id 
对象方的动画 ID。

element_ranks 
属性有效度。是以属性 ID 为索引的一维数组（Table），其值分 6 级（0：A，1：B，2：C，3：D，4：E，5：F）。

state_ranks 
状态有效度。是以状态 ID 为索引的一维数组（Table），其值分 6 级（0：A，1：B，2：C，3：D，4：E，5：F）。

actions 
行为。RPG::Enemy::Action 的数组。

exp 
EXP。

gold 
金钱。

item_id 
宝物的物品 ID。

weapon_id 
宝物的武器 ID。

armor_id 
宝物的防具 ID。

treasure_prob 
宝物出现率。


RPG::Enemy::Action
敌人「行为」的数据类。
 
kind 
种类（0：基本，1：特技）。

basic 
种类为「基本」时，其内容（0：攻击，1：防御，2：逃跑，3：什么也不做）。

skill_id 
种类为「特技」时，其 ID。

condition_turn_a 
condition_turn_b 
条件「回合」指定的 a、b 的值。输入为 a + bx 的形式。

以回合为条件但未指定值时默认为 a = 0、b = 1。

condition_hp 
条件「HP」指定的比率（%）。

以 HP 为条件但未指定值时默认为 100。

condition_level 
条件「等级」指定的标准值。

以等级为条件但未指定值时默认为 1。

condition_switch_id 
条件「开关」指定的开关 ID。

以开关为条件但未指定值时默认为 0（所以需要检查是否为 0）。

rating 
额定值（1..10）。


=end