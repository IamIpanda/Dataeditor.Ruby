# xp - project.rb 
# Load All Need in a RPG Maker Xp Project.

require "File - xp.rb"

window = Window["main"]
Builder.In(window)
Builder.Add(:tabs) do
	Builder.Add(:tab, {:text => "角色"}) { require "Actor.rxdata.rb" }
	Builder.Add(:tab, {:text => "职业"}) { require "Classes.rxdata.rb" }
	Builder.Add(:tab, {:text => "物品"}) { require "Items.rxdata.rb" }
	Builder.Add(:tab, {:text => "武器"}) { require "Weapons.rxdata.rb" }

end