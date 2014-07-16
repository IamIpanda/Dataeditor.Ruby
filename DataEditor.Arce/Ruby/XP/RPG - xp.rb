# Arce Script : RPG - xp.rb
# Load All the Data structure needed in RPG Maker XP

module RPG
	class Actor
		def initialize
			@id = 0
			@name = ""
			@class_id = 1
			@initial_level = 1
			@final_level = 99
			@exp_basis = 30
			@exp_inflation = 30
			@character_name = ""
			@character_hue = 0
			@battler_name = ""
			@battler_hue = 0
			@parameters = Table.new(6,100)
			for i in 1..99
				@parameters[0,i] = 500+i*50
				@parameters[1,i] = 500+i*50
				@parameters[2,i] = 50+i*5
				@parameters[3,i] = 50+i*5
				@parameters[4,i] = 50+i*5
				@parameters[5,i] = 50+i*5
			end
			@weapon_id = 0
			@armor1_id = 0
			@armor2_id = 0
			@armor3_id = 0
			@armor4_id = 0
			@weapon_fix = false
			@armor1_fix = false
			@armor2_fix = false
			@armor3_fix = false
			@armor4_fix = false
		end
	end
	class Class
		def initialize
			@id = 0
			@name = ""
			@position = 0
			@weapon_set = []
			@armor_set = []
			@element_ranks = Table.new(1)
			@state_ranks = Table.new(1)
			@learnings = []
		end
		class Learning
			def initialize
				@level = 1
				@skill_id = 1
			end
		end
	end

end