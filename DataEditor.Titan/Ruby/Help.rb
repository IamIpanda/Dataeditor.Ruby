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
	def self.Get_Default_Text
		return Text.new do |*args|
			Help.Auto_Get_Text(*args)
		end
	end
	def self.VX_Image_Split
		
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
		puts self.text
		self.watch = []
	end
end