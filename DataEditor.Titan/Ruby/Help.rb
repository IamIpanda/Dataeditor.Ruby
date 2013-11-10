#encoding:UTF-8
# Arce Script : Help.rb
# All the Methods Needed in Running

class Help
	def self.Auto_Get_Text(*args)
		watch = []
		id = args[0]["id"]
		name = args[0]["name"]
		watch.push id
		watch.push name
		return [sprintf("0:d3",id) + ":" + name,watch]
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
Filechoice = Struct.new(:data,:id,:filter,:watch) do
	def initialize(data,id = :id,&filter)
		self.data = data
		self.id = id
		self.filter = filter
		self.watch = []
	end
end