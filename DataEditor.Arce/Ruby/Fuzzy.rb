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

class DataEditor::FuzzyData::FuzzyObject
	def to_ruby
		str = Serialization.TrySetValue(self, "[m]")
		return Marshal.load(str)
	end
end

FuzzyString = DataEditor::FuzzyData::FuzzyString
FuzzyArray = DataEditor::FuzzyData::FuzzyArray