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
	def self.Auto_Get_String(*args)
		target = args[0]
		name = target["@name"]
		watch = args[1]
		watch.clear
		watch.push name
		return name
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
	def self.Get_Silence_Text
		return Text.new do |*args|
			Help.Auto_Get_String(*args)
		end
	end
	XP_IMAGE_SPLIT = { "" => Split.new(Split::COUNT,1,Split::COUNT,1) }
	XP_IMAGE_SHOW = { "" => Split.new(Split::COUNT,4,Split::COUNT,4) }
	VX_IMAGE_SPLIT = 
	{ 
		"" => Split.new(Split::COUNT,4,Split::COUNT,2),
		"$" => Split.new(Split::COUNT,1,Split::COUNT,1)
	 }	
	VX_IMAGE_SHOW = 
	{ 
		"" => Split.new(Split::COUNT,3,Split::COUNT,4,1,0),
		"!" => Split.new(Split::COUNT,1,Split::COUNT,4,0,0)		
	 }
	FACE_SPLIT = { "" => Split.new(Split::COUNT, 4, Split::COUNT, 2) }
	ICON_SPLIT = Split.new(Split::VALUE, 24, Split::VALUE, 24 )
	def self.stop
	 System::Diagnostics::Debugger.Break
	end

end
# Color Class
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
	def _dump
		[self.red, self.green, self.blue, self.alpha].pack('d4')
	end
end
# Rect class
Rect = Struct.new(:x,:y,:width,:height) do
	def _dump
		[self.x, self.y, self.width, self.height].pack('d4')
	end
end
# Tone class
Tone = Struct.new(:red,:green,:blue,:gray) do
	def _dump
		[self.red, self.green, self.blue, self.gray].pack('d4')
	end
end
# Table class
class Table
 def initialize(x, y = 1, z = 1)
   @xsize, @ysize, @zsize = x, y, z
   @data = Array.new(x * y * z, 0)
 end
 def [](x, y = 0, z = 0)
   @data[x + y * @xsize + z * @xsize * @ysize]
 end
 def []=(*args)
   x = args[0]
   y = args.size > 2 ? args[1] :0
   z = args.size > 3 ? args[2] :0
   v = args.pop
   @data[x + y * @xsize + z * @xsize * @ysize] = v
 end
 def _dump(d = 0)
   s = [3].pack('L')
   s += [@xsize].pack('L') + [@ysize].pack('L') + [@zsize].pack('L')
   s += [@xsize * @ysize * @zsize].pack('L')
   for z in 0...@zsize
    for y in 0...@ysize
      for x in 0...@xsize
       s += [@data[x + y * @xsize + z * @xsize * @ysize]].pack('S')
      end
    end
   end
   s
 end
 attr_reader(:xsize, :ysize, :zsize, :data)
end
# Filechoice
Filechoice = Struct.new(:data,:id,:filter,:text,:watch) do
	def initialize(data,id = :id,&filter)
		self.data = data
		self.id = id
		self.filter = filter
		self.text = Help.Get_Default_Text
		self.watch = []
	end
end
# Fileselect
Fileselect = Struct.new(:data,:filter,:text,:watch) do
	def initialize(data,&filter)
		self.data = data
		self.filter = filter
		self.text = Text.new { |value| value.Text }
		self.watch = []
	end
end