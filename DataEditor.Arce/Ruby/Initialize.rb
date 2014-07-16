# Arce Sctipt : Initialize.rb
# Initialize all the Constant Ruby need from C#

Bash			 = DataEditor::Help::Bash
Link       = DataEditor::Help::Link.Instance
Window     = DataEditor::Help::Window.Instance
Monitor    = DataEditor::Help::Monitor
Serialization = DataEditor::Help::Serialization
Data       = DataEditor::Help::Data.Instance
Path       = DataEditor::Help::Path.Instance
Log        = DataEditor::Help::Log
Painter    = DataEditor::Help::Painter.Instance
Palette    = DataEditor::Help::Palette
Text       = DataEditor::Help::Parameter::Text
Split      = DataEditor::Help::Parameter::Split
Builder    = DataEditor::Ruby::RubyBuilder
Engine     = DataEditor::Ruby::RubyEngine
Measurement	= DataEditor::Help::Measurement.Instance

# Builder 修正。
# 为 Builder 添加了块的支持。
def Builder.Add(type,parameter = {},&block)
	editor = Builder.Push(type,parameter,block)
	return editor
end
# Text 修正。
# 为 Text 添加了块的支持。
class <<Text
	alias :origin_new :new
	def new(&block)
		ans = origin_new(DataEditor::Ruby::Proc.new(block))
		return ans
	end
	def ret(text)
		ans = origin_new(text.encode)
		return ans	
	end
end
# 全局方法 puts 修正
# 改为一个 MessageBox
def puts(*obj)
	System::Windows::Forms::MessageBox.Show obj.inspect
end
class Split
	COUNT = DataEditor::Help::Parameter::Split::SplitType.Count
	VALUE = DataEditor::Help::Parameter::Split::SplitType.Value
end
class String
	def encode
		return self.ToString(System::Text::Encoding.UTF8)
	end
end

# Array 修正。
# 这部分修正是为了在LazyChoose部分更加美观
class DataEditor::FuzzyData::FuzzyArray
	alias __get_value []
	def each(&block)
		count = self.Count
		for i in 0...count
			block.call(self[i])
		end
	end
	def [](index)
		return __get_value(index.Value) if (index.is_a?(DataEditor::FuzzyData::FuzzyFixnum))
		if (index.is_a?(DataEditor::FuzzyData::FuzzyArray))
			ans = []
			index.each { |item| ans.push self[item] }
			return ans
		end
		return __get_value(index)
	end
	def select(&block)
		ans = []
		for i in 0...self.count
			ans.push self[i] if block.call(self[i])
		end
		ans
	end
end

class Proc
	def to_p
		return DataEditor::Ruby::Proc.new(self)
	end
end