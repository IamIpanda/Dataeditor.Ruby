# This file is in coding: utf-8
# Arce Script : Fuzzy.rb
# Support a method to turn Ruby data to FuzzyData

require "Ruby/Initialize.rb"
require "Ruby/Help.rb"

class Object
	def to_fuzzy
		str = Marshal.dump(self)
		Serialization.TryGetValue(str, "[m]")
	end
end