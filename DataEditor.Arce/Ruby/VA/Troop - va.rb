# This file is in coding: utf-8
# Arce script : Troop - va.rb
# Describe the user interface for Troop in RPG Maker VX Ace

# WARNING this file is still in just inmagine.
# WARNING troop_member is a limited control that has nothing to use but just show troop.

require "Ruby/VA/File - va.rb"
Builder.Add(:tab, { text: "队伍" }) do
	list = Builder.Add(:list, { textbook: Help.Get_Default_Text, text: "队伍" }) do
		Builder.Add(:troop_member, { actual: :members, text: :name, data: Data["enemy"], textbook: Help.Get_Default_Text })
	end
	list.Value = Data["troop"]
end	