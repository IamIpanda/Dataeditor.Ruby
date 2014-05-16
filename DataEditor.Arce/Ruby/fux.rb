# This file is in coding: utf-8
# Arce Script specified for Fux2 : main.rb
require "Ruby/XP/File - xp.rb"
Builder.In(Window["main"])
	list = Builder.Add(:list, {:textbook => Help.Get_Default_Text, :text => "卖萌的小黄鸡"}) do
		Builder.Add(:group, {:text => "", :dock => 5}) do
			Builder.Add(:actor_parameters, :actual => :parameters) do
				Builder.Order
				Builder.Add(:actor , {:index => 0 , :text => "MaxHP" ,:color => Color.new(183,40,103).to_c , :max_number => 9999 })
				Builder.Add(:actor , {:index => 1 , :text => "MaxSP" ,:color => Color.new(46,95,194).to_c , :max_number => 9999 })
					Builder.Next
				Builder.Add(:actor , {:index => 2 , :text => "力量" ,:color => Color.new(185,102,37).to_c , :max_number => 999 })
				Builder.Add(:actor , {:index => 3 , :text => "速度" ,:color => Color.new(109,193,7).to_c , :max_number => 999 })
					Builder.Next
				Builder.Add(:actor , {:index => 4 , :text => "灵巧" ,:color => Color.new(62,192,91).to_c , :max_number => 999 })
				Builder.Add(:actor , {:index => 5 , :text => "敏捷" ,:color => Color.new(96,26,196).to_c , :max_number => 999 })
			end
		end
	end
	list.Value = Data["actor"]
Builder.Out