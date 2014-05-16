# This File is in coding: utf-8
# Arce Script: CommonEvent - xp.rb
# describe the user interface of Common Events

require "Ruby/XP/File - xp.rb"
require "Ruby/XP/Event - xp.rb"
Builder.Add(:tab , { :text => "公共事件" }) do
	list = Builder.Add(:list, {:textbook => Help.Get_Default_Text ,:text => "公共事件"}) do
		Builder.Add(:group) do
				Builder.Order
			Builder.Add(:text , {:actual => :name , :text => "名称"})
			Builder.Add(:choose , {:actual => :trigger , :text => "触发条件", :choice => { 0 => "无", 1 => "自动执行", 2 => "并行处理" }})
				Builder.Next
			Builder.Add(:event , {:actual => :list, :text => "未命名" , :commands => $commands_xp, :groups => $groups_xp, :width => 600, :height => 400})
		end
	end
	list.Value = Data["commonevent"]
end

