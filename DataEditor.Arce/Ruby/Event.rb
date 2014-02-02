# Non-meaning science
# 空想科学
# 本文件属于空想，不可执行，亦不会被执行。

throw Exception.new("Running bad toys!")

class EventHelp
	module_function
	def get_switch_text(array, start_id)
	end
	def get_variable_text(array, start_id)
	end
end

# mode : 单行，多行，或者自定义。0 代表单行，1 代表多行，-1 代表自定义。
# 
EventNode = Struct.new(:mode, :id, :help_id, :command_name, :name, :text, :running, )

Talk = EventNode.new(1, 101, 601, "TALK", "显示对话", Text.new { "" }, Proc.new{  } )
Choice = EventNode.new(-1, 102, -1, "CHOICE", "显示选择项", Text.new { "" }, Proc.new{ } )




Builder.Add(:event , {:actual => :event_list , :text => "事件" ,})

