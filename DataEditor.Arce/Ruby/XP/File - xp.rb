# Arce Script : File - xp.rb
# Load All the File needed in RPG Maker XP Environment

require "Ruby/Help.rb"
require "Ruby/XP/RPG - xp.rb"

Data["actor"] = "Data/Actors.rxdata"
Data["class"] = "Data/Classes.rxdata"
Data["skill"] = "Data/Skills.rxdata"
Data["item"] = "Data/Items.rxdata"
Data["weapon"] = "Data/Weapons.rxdata"
Data["armor"] = "Data/Armors.rxdata"
Data["enemy"] = "Data/Enemies.rxdata"
Data["troop"] = "Data/Troops.rxdata"
Data["state"] = "Data/States.rxdata"
Data["animation"] = "Data/Animations.rxdata"
Data["tileset"] = "Data/Tilesets.rxdata"
Data["commonevent"] = "Data/CommonEvents.rxdata"
Data["system"] = "Data/System.rxdata"
Data["mapinfo"] = "Data/MapInfos.rxdata"
Data["map"] = "Data/Map*.rxdata"

Data["focus_troop"] = Data["troop"][1];
Data["focus_map"] = Data["map"][0];

Measurement.Set("audio", 130, 20)
Measurement.Set("int", 65, 20)
Measurement.Set("choose", 130, 24)
Measurement.Set("drop", 130, 20)
Measurement.Set("oldimage", 130, 20)
Measurement.Set("exp", 130, 20)
Measurement.Set("text", 130, 20)
Measurement.Set("bool_choose", 65, 20)
Measurement.Set("checklist", 130, 320)
Measurement.Set("bufflist", 130, 320)
Measurement.Set("scrollint", 250, 42)


=begin
  $data_actors    = load_data("Data/Actors.rxdata")
  $data_classes    = load_data("Data/Classes.rxdata")
  $data_skills    = load_data("Data/Skills.rxdata")
  $data_items     = load_data("Data/Items.rxdata")
  $data_weapons    = load_data("Data/Weapons.rxdata")
  $data_armors    = load_data("Data/Armors.rxdata")
  $data_enemies    = load_data("Data/Enemies.rxdata")
  $data_troops    = load_data("Data/Troops.rxdata")
  $data_states    = load_data("Data/States.rxdata")
  $data_animations  = load_data("Data/Animations.rxdata")
  $data_tilesets   = load_data("Data/Tilesets.rxdata")
  $data_common_events = load_data("Data/CommonEvents.rxdata")
  $data_system    = load_data("Data/System.rxdata")
=end