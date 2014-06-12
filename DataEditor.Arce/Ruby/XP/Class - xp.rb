# This File is in coding: utf-8
# Arce Script: class - xp.rb
# describe the user interface of class

require "Ruby/XP/File - xp.rb"

Builder.Add(:tab , { :text => "职业" }) do
	list = Builder.Add(:list, {:textbook => Help.Get_Default_Text, :text => "职业"}) do
		Builder.Add(:group, {:text => ""}) do
			Builder.Add(:text , {:actual => :name , :text => "名称"})
			Builder.Add(:checklist , {:actual => :weapon_set , :text => "可装备的武器", :data => Data["weapon"], :textbook => Help.Get_Silence_Text, :height => 420, :width => 130} )
				Builder.Next
			Builder.Add(:choose , {:actual => :position , :text => "位置" ,:choice => {0 => "前卫", 1 => "中卫", 2 => "后卫"}})
			Builder.Add(:checklist , {:actual => :armor_set , :text => "可装备的防具", :data => Data["armor"], :textbook => Help.Get_Silence_Text, :height => 420, :width => 130 } )
				Builder.Next
			Builder.Add(:group) do
					Builder.Order
					text = Text.new { |*args| args[0] }
				Builder.Add(:textlist , {:actual => :element_ranks, :text => "属性有效度", :textbook => text, 
					:choices => ["A","B","C","D","E","F"], :value => [1,2,3,4,5,6,7], :default => 3, :data => Data["system"]["@elements"], :width => 130, :height => 425 })
					text = Text.new { |*args| args[0]["@name"] }
				Builder.Add(:textlist , {:actual => :state_ranks , :text => "状态有效度" , :textbook => text, 
					:choices => ["A","B","C","D","E","F"], :value => [1,2,3,4,5,6,7], :default => 3, :data => Data["state"], :width => 130, :height => 425})
			end
				window = Proc.new do |window, value|
						Builder.In(window)
					Builder.Add(:int , {:actual => :level , :text => "等级"})
					Builder.Add(:choose , {:actual => :skill_id , :text => "学会的特技" , :choice => { nil => Filechoice.new("skill") }})
						Builder.Out
						window.Value = value
				end
				texts = []
				texts.push Text.new {|target, watch, i, j, k| "Lv.#{target["@level"].Value}" }
				texts.push( Text.new do |target, watch, i, j, k|
					skill_id = target["@skill_id"].Value
					target = Data["skill"][skill_id]
					ans = Help.Auto_Get_Text(target, watch)
					ans
				end )
			Builder.Add(:view , {:actual => :learnings , :text => "特技" ,:columns => ["等级","学会的特技"], 
				:catalogue => texts, :window => window, :window_type => 1, :new => nil, :width => 205, :height => 150})
		end
	end
	list.value = Data["class"]
end


=begin
属性id 
ID。

name 
名称。

position 
位置（0：前卫，1：中卫，2：后卫）。

weapon_set 
包含可装备武器 ID 的数组。

armor_set 
包含可装备防具 ID 的数组。

element_ranks 
属性有效度。是以属性 ID 为索引的一维数组（Table），其值分 6 级（0：A，1：B，2：C，3：D，4：E，5：F）。

state_ranks 
状态有效度。是以状态 ID 为索引的一维数组（Table），其值分 6 级（0：A，1：B，2：C，3：D，4：E，5：F）。

learnings 
学会的特技。RPG::Class::Learning 的数组。

RPG::Class::Learning : 

level 
等级。

skill_id 
学会的特技 ID。



=end