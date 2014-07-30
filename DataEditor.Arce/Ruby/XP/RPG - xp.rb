# Arce Script : RPG - xp.rb
# Load All the Data structure needed in RPG Maker XP

module RPG
	Klass = ::Class
	module Trap
		def const_missing(name)
			ans = RPG.name_check(self.name.to_s + "::#{name}")
			nodule = RPG::Klass.new do
				@@model = ans
				extend Trap
				def self.new
					return @@model.Clone
				end
			end
			const_set(name, nodule)
			return nodule
		end
	end
	extend Trap
	def self.initialize
		str = File.open("Ruby/XP/RPG - xp.data").read
		cache = Serialization.TryGetValue(str, "[m]")
		@@cache = {}
		cache.each { |pair| @@cache[pair.Key.to_s] = pair.Value}
	end
	def self.[](name)
		return @@cache[name]
	end
	def self.name_check(name)
		return self[name]
	end
end
RPG.initialize

# That's link out code.
# It describe how to output RPG modules from RPG Maker XP
=begin
hash = {}
record = ""
ObjectSpace.each_object do |klass|
	if klass.is_a?(Class) && klass.name[0, 5] == "RPG::"
		hash[klass.name] = begin
      eval("#{klass.name}.new")
    rescue
      nil
    end
    record += klass.name + "\n"
	end
end
print record
hash["RPG::Event"] = RPG::Event.new(1, 1)
hash["RPG::Map"] = RPG::Map.new(20, 15)
hash.delete "RPG::Sprite"
hash.delete "RPG::Weather"

File.open("RPGXP.data", "wb") do |f|
  Marshal.dump(hash, f)
end
exit
=end
