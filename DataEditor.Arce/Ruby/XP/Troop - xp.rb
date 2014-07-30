# This file is in coding: utf-8
# Arce script : Troop - xp.rb
# Describe the user interface for Troop

# WARNING troop_member is a limited control that has nothing to use but just show troop.

require "Ruby/XP/File - xp.rb"
tab = Builder.Add(:tab, { text: "队伍" }) do
	Builder.Add(:list, { textbook: Help.Get_Default_Text, text: "队伍", default: RPG::Troop.new }) do
		Builder.Add(:troop_member, { actual: :members, text: :name, data: Data["enemy"], textbook: Help.Get_Default_Text })
	end
end	
tab.Value = Data["troop"]