# Arce Sctipt : Initialize.rb
# Initialize all the Constant Ruby need from C#

Link       = DataEditor::Help::Link.Instance
Window     = DataEditor::Help::Window.Instance
Monitor    = DataEditor::Help::Monitor

Data       = DataEditor::Help::Data.Instance
Path       = DataEditor::Help::Path.Instance
Log        = DataEditor::Help::Log

Text       = DataEditor::Help::Parameter::Text
Split      = DataEditor::Help::Parameter::Split

Builder    = DataEditor::Ruby::RubyBuilder
Engine     = DataEditor::Ruby::RubyEngine

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