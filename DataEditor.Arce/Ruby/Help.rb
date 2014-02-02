#encoding:UTF-8
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
	XP_IMAGE_SPLIT = { "" => Split.new(Split::COUNT,4,Split::COUNT,4) }
	XP_IMAGE_SHOW  = { "" => Split.new(Split::COUNT,1,Split::COUNT,1) }
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

Color = Struct.new(:red,:green,:blue,:alpha)
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

def choice(str)
	return Filechoice.new(str)
end
