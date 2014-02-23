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
			return sprintf("%03d:%s",id,text)
		end
		def switches(type, int1, int2)
			switches = Data["system"]["switches"]
			if (type == 0)
				return Event_Help.switch(int1)
			else
				return sprintf("%03d..%03d",int1,int2)
			end
		end
		def variable(id)
			text = Data["system"]["@variables"][id].Text.to_s
			if text == ""
				return sprintf("[%04d]", id)
			else
				return sprintf("[%04d:%s]",id,text).encode
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
	end # Untitle Class
end