# This file is in coding: utf-8
# Arce Script: event.rb
# It's a help file for events

# 约定
# code    给定的 key，唯一
# command 给定的指令名，可以有多个
# name    显示的指令名，唯一
# text    将此指令显示在列表上
# with    弹入此指令时，追加的指令，以 code 为约定
# type    类型检查值
# window  弹出窗口。若它为 nil，则不会弹出任何窗口直接压入。
# after   窗口弹出之后，进行指令修正

class DataEditor::Control::Event::EventCommand
	def initialize(code = -1, follow = -1, command = "Untitled",name = "Unknown", text = nil, parameters = "", window = nil, with = nil, up = 0, down = 0)
		self.Code = code
		self.UpIndent = up
		self.DownIndent = down
		self.Follow = follow
		self.Command = command.encode
		self.Name = name.encode
		self.Parameters = parameters
		self.Text = text
		self.Window = DataEditor::Ruby::Proc.new(window) if window != nil
		self.With = DataEditor::Ruby::Proc.new(with) if with != nil
		Log.log("添加了指令：[#{code}:#{name}]".encode)
	end
	class <<self
		alias :origin_new :new
		def new(*args)
			obj = origin_new(*args)
			return obj
		end
	end
end
Command = DataEditor::Control::Event::EventCommand
class DataEditor::Control::Event::CommandGroup
end
Group = DataEditor::Control::Event::CommandGroup
class Event_Help
	class <<self
		def switch(id)
			text = Data["system"]["@switches"][id].Text
			if text == "" || text == nil
				return sprintf("[%04d]", id)
			else
				return sprintf("[%04d:%s]",id,text)
			end
		end
		def switches(int1, int2)
			switches = Data["system"]["@switches"]
			if (int1 == int2)
				return Event_Help.switch(int1)
			else
				return sprintf("[%04d..%04d]",int1,int2)
			end
		end
		def variable(id)
			text = Data["system"]["@variables"][id].Text
			if text == "" || text == nil
				return sprintf("[%04d]", id)
			else
				return sprintf("[%04d:%s]",id,text).encode
			end
		end
		def variables(int1, int2)
			if (int1 == int2)
				return Event_Help.variable(int1)
			else
				return sprintf("[%04d..%04d]",int1,int2)
			end
		end
		def pos(type, int1, int2, int3)
			if (type == 0)
				mapname = nil
				for info in Data["mapinfo"].Keys
					if info.Value == int1
						mapname = Data["mapinfo"][info]["@name"].Text
					end
				end
				mapname = "不存在的地图".encode if mapname == nil
				return sprintf("[%03d: %s], (%03d, %03d)", int1, mapname, int2, int3)
			else
				return sprintf("变量[%04d][%04d][%04d]", int1, int2, int3).encode
			end
		end
		def direction(int)
			return["就这样","","下","","左","","右","","上"][int].encode
		end
		def press(int)
			return["","","下","","左","","右","","上","","","A","B","C","X","Y","Z","L","R"][int].encode
		end

		def tone(tone)
			return "(#{tone.red}, #{tone.green}, #{tone.blue}, #{tone.gray})"
		end
		def color(color)
			return "(#{color.red}, #{color.green}, #{color.blue}, #{color.alpha})"
		end
		def variable_or_value(int1, int2)
			return int1 == 0 ? int2.to_s : ("变量 ".encode + variable(int2))
		end
		def variable_or_values(type, *ints)
			return type == 0 ? ints.join(", ") : ("变量".encode + ints.collect{|i| sqrintf("[%04d]",i)}.join("") )
		end
		def value(id, data)
			return "（无）".encode if id == 0
			"[#{data[id]["@name"].Text}]"
		end
		def text(id, data)
			"[#{id}. #{data[id].Text}]"
		end
		def audio(target)
			return "\'#{target["@name"].Text}\', #{target["@volume"].Value}, #{target["@pitch"].Value}"
		end
		def add_or_sub(int)
			return int == 0 ? " + " : " - "
		end
		def allow_or_ban(int)
			return (int == 0 ? "禁止" : "允许").encode
		end
		def actor(id)
			if (id == 0) 
				return "全体同伴".encode
			else
				return value(id, Data["actor"])
			end
		end
		def enemy(id)
			if (id < 0) 
				return "全体队伍".encode
			else
				return "[#{id+1}.]"
			end
		end
		def shop(int1,int2)
			case int1
			when 0
				return value(int2,Data["item"])
			when 1
				return value(int2,Data["weapon"])
			when 2
				return value(int2,Data["armor"])
			end
		end
		def compare(int)
			return [" == ", " >= ", " <= ", " > ", " < ", " != "][int]
		end
		def switch_state(int)
			return int == 0 ? "ON" : "OFF"
		end
		def event(int)
			return "角色".encode if int == -1
			return "本事件".encode if int == 0
			return "#{int} 号事件" 
		end
	end # Untitle Class
end


class Builder
	class << self
		def get_symbol(index)
			return "INDEX" + index.to_sym
		end
	  	def Pop(sym, index)
	    	case sym
	    	when :count_or_variable
	    		Builder.Add(:group, {:text => "操作数"}) do
	    			Builder.Add(:radio, {:actual => get_symbol(index), :text => "常量", :key => 0, :group => "group_cov"}) do
	    				Builder.Add(:int, {:actual => get_symbol(index + 1)})
	    			end
	    			Builder.Add(:radio, {:actual => get_symbol(index), :text => "变量", :key => 1, :group => "group_cov"}) do
	    				Builder.Add(:variable, {:actual => get_symbol(index + 1)})
	    			end
	    		end
	    	when :group_switch_2
	    		Builder.Add(:group, {:text => "开关"}) do
	    			# ISON
	    			Builder.Add(:radio, {:actual => get_symbol(index), :text => "单独", :key => 0, :group => "group_switch_2"}) do
	    				Builder.Add(:variable, {:actual => get_symbol(index + 1)})
	    			end
	    			Builder.Add(:radio, {:actual => get_symbol(index), :text => "统一" ,:key => 1, :group => "group_switch_2"}) do
	    				Builder.Order
	    				Builder.Add(:int, {:actual => get_symbol(index + 1)})
	    				Builder.Text(" ~ ")
	    				Builder.Add(:int, {:actual => get_symbol(index + 2)})
	    			end
	    		end
	    	when :group_vairble_2
	    		# FUCK IT
	    	when :operate
	    		Builder.Add(:group, {:text => "操作"}) do
	    			Builder.Add(:single_radio, {:actual => get_symbol(index), :text => "增加", :key => 0, :group => "group_operate_1"})
	    			Builder.Add(:single_radio, {:actual => get_symbol(index), :text => "减少", :key => 1, :group => "group_operate_1"})
	    		end
	    	when :operate_number
	    		Builder.Add(:group, {:text => "操作数"}) do
	    			Builder.Add(:radio, {:actual => get_symbol(index), :text => "常量"}) do
	    				Builder.Add(:int, {:actual => get_symbol(index + 1)})
	    			end
	    			Builder.Add(:radio, {:actual => get_symbol(index), :text => "变量"}) do
	    				Builder.Add(:variable, {:actual => get_symbol(index + 1)})
	    			end
	    		end
	    	end
	    end
	end
end