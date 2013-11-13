# Arce Sctipt : Initialize.rb
# Initialize all the Constant Ruby need from C#

Link       = DataEditor::Help::Link.Instance
Window     = DataEditor::Help::Window.Instance
Monitor    = DataEditor::Help::Monitor

Data       = DataEditor::Help::Data.Instance
Path       = DataEditor::Help::Path.Instance
Log        = DataEditor::Help::Log

Text       = DataEditor::Help::Parameter::Text

Builder    = DataEditor::Ruby::RubyBuilder
Engine     = DataEditor::Ruby::RubyEngine

# Builder 修正。
# 为 Builder 添加了块的支持。
def Builder.Add(type,parameter,&block)
	Builder.Push(type,parameter,block)
end
# Text 修正。
# 为 Text 添加了块的支持。
class <<Text
	alias :origin_new :new
	def new(&block)
		ans = origin_new(DataEditor::Ruby::Proc.new(block))
		return ans
	end
end
# Data 修正。
# 为 Data 修正了 []
class <<Data
	alias :old_get_value :[]
	def [](index)
		index = index.ToStirng() if index.is_a(String)
		return old_get_value(index)
	end
end
# FuzzyObject 修正
# 提供了便捷的 []
class DataEditor::FuzzyData::FuzzyObject
	alias :old_get_value :[]
	def [](index)
		index = index.ToString() if(index.is_a?(Symbol))
		index = "@" + index if index[0,1] != "@"
		return old_get_value(index)
	end
end
# 全局方法 puts 修正
# 改为一个 MessageBox
def puts(obj)
	System::Windows::Forms::MessageBox.Show obj.inspect
end

