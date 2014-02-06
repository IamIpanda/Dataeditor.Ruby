# This file is in coding: UTF-8
# Arce Script : Help.rb
# All the Methods Needed in Running

class Help
	def self.Auto_Get_Text(*args)
		target = args[0]
		id = target["@id"]
		name = target["@name"]
		watch = args[1]
		watch.clear
		watch.push id
		watch.push name
		return sprintf("%03d:%s",id.Value,name.Text)
	end
	def self.System_Property_Text(*args)
		string = args[0]
		watch.clear
		watch.push string
		return sprintf("%s",string)
	end
	def self.Get_Default_Text
		return Text.new do |*args|
			Help.Auto_Get_Text(*args)
		end
	end
	def self.Get_Property_Text
		return Text.new do |*args|
			Help.System_Property_Text(*args)
		end
	end
	XP_IMAGE_SPLIT = { "" => Split.new(Split::COUNT,1,Split::COUNT,1) }
	XP_IMAGE_SHOW  = { "" => Split.new(Split::COUNT,4,Split::COUNT,4) }
	VX_IMAGE_SPLIT = 
	{
		""  => Split.new(Split::COUNT,4,Split::COUNT,2),
		"$" => Split.new(Split::COUNT,1,Split::COUNT,1)
	}	
	VX_IMAGE_SHOW = 
	{
		""  => Split.new(Split::COUNT,3,Split::COUNT,4,1,0),
		"!" => Split.new(Split::COUNT,1,Split::COUNT,4,0,0)		
	}

	def self.stop
	  System::Diagnostics::Debugger.Break
	end

end

Color = Struct.new(:red,:green,:blue,:alpha) do
	def initialize(r,g,b,a = 255)
		self.red = r
		self.green = g
		self.blue = b
		self.alpha = a
	end
	def to_i
		return self.alpha << 24 + self.red << 16 + self.green << 8 + self.blue
	end
	def to_c
		return System::Drawing::Color.FromArgb(self.alpha,self.red,self.green,self.blue)
	end
end
Rect = Struct.new(:x,:y,:width,:height)
Tone = Struct.new(:red,:green,:blue,:gray)
Filechoice = Struct.new(:data,:id,:filter,:text,:watch) do
	def initialize(data,id = :id,&filter)
		self.data = data
		self.id = id
		self.filter = filter
		self.text = Help.Get_Default_Text
		self.watch = []
	end
end

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
