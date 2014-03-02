# This file is in coding: utf-8
# Arce Script : Actor - va.rb
# Descrobe the Actor page in RPG Maker VA

require "Ruby/VA/File - va.rb"
require "Ruby/VA/Feature - va.rb"

Builder.Add(:tab , {:text => "角色" }) do
	list = Builder.Add(:list , { :text => "角色" ,:textbook => Help.Get_Default_Text}) do 
		Builder.Add(:metro, {:text => "基本设置" }) do
				Builder.Order
			Builder.Add(:text , {:actual => :name , :text => "角色" })
			Builder.Add(:text , {:actual => :nickname , :text => "称号" })
				Builder.Next
			Builder.Add(:choose , {
				:actual => :class_id , 
				:text => "职业" ,
				:choice => { nil => Filechoice.new("class") }
				})
			Builder.Add(:int , {:actual => :initial_level , :text => "初始等级", :width => 26})
			Builder.Add(:int , {:actual => :max_level , :text => "最终等级", :width => 26})
				Builder.Next
			Builder.Add(:text , {:actual => :description , :text => "说明" })
		end
		Builder.Add(:metro , { :text => "图像" }) do	
				Builder.Order
			Builder.Add(:image , {
				:actual => {
				:name => :character_name, 
				:index => :character_index } , 
				:text => "",
				:path => "Graphics/Characters",
				:show => Help::VX_IMAGE_SHOW,
				:split => Help::VX_IMAGE_SPLIT 
				})
			Builder.Add(:image , {
				:actual => {
				:name => :face_name, 
				:index => :face_index } , 
				:text => "",
				:path => "Graphics/Faces",
				:show => Help::XP_IMAGE_SPLIT,
				:split => Help::FACE_SPLIT 
				})
		end
		Builder.Add(:metro, :text => "初期装备", :actual => :equips) do 
			Builder.Add(:lazy_choose , {
				:actual => :INDEX0 ,
				:label => 2,
				:textbook => Help.Get_Default_Text ,
				:choice => { 0 => "（无）" } , 
				:text => "武器", 
				:source => Proc.new do |target, parent, control|
					VA_Help.search_weapon(control)
			end })			
			Builder.Add(:lazy_choose , {
				:actual => :INDEX1,  
				:label => 2, 
				:textbook => Help.Get_Default_Text , 
				:choice => { 0 => "（无）" }, 
				:text => "副手",
				:source => Proc.new do |target, parent, control|
					if (VA_Help.isDouble(control))
						VA_Help.search_weapon(control)
					else
						VA_Help.search_armor(control, 1)
					end
			end })
			Builder.Add(:lazy_choose , {
				:actual => :INDEX2,  
				:label => 2, 
				:textbook => Help.Get_Default_Text , 
				:choice => { 0 => "（无）" }, 
				:text => "头盔",
				:source => Proc.new do |target, parent, control|
					VA_Help.search_armor(control, 2)
			end })
			Builder.Add(:lazy_choose , {
				:actual => :INDEX3,  
				:label => 2, 
				:textbook => Help.Get_Default_Text , 
				:choice => { 0 => "（无）" }, 
				:text => "铠甲",
				:source => Proc.new do |target, parent, control|
					VA_Help.search_armor(control, 3)
			end })
			Builder.Add(:lazy_choose , {
				:actual => :INDEX4,  
				:label => 2, 
				:textbook => Help.Get_Default_Text , 
				:choice => { 0 => "（无）" }, 
				:text => "饰品",
				:source => Proc.new do |target, parent, control|
					VA_Help.search_armor(control, 4)
			end })
		end
			Builder.Next
		Builder.Add(:metro , { :text => "特性" }) do
			VA_Help::Feature.build_feature
		end
		Builder.Add(:metro, { :text => "备注" }) do
			Builder.Add(:text , {:actual => :note , :label => 0, :height => 400, :width => 600 })
		end
	end
	list.Value = Data["actor"]
end

class VA_Help
	class << self
		def search_weapon(control)
			actor_class = Data["class"][control.Container.Container.Value["@class_id"]]
			actor_features = control.Container.Container.Value["@features"]
			class_features = actor_class["@features"]
			type = []
			banish = false
			actor_features.each do |feature| 
				type.push(feature["@data_id"]) if feature["@code"].Value == 51 
				banish = true if feature["@code"].Value == 54 && feature["@data_id"] == 0
			end
			class_features.each do |feature| 
				type.push(feature["@data_id"]) if feature["@code"].Value == 51 
				banish = true if feature["@code"].Value == 54 && feature["@data_id"] == 0
			end
			return [] if banish
			return Data["weapon"].select {|weapon| type.include?(weapon["@wtype_id"]) }
		end
		def search_armor(control, id)
			actor_class = Data["class"][control.Container.Container.Value["@class_id"]]
			actor_features = control.Container.Container.Value["@features"]
			class_features = actor_class["@features"]
			type = []
			banish = false
			actor_features.each do |feature| 
				type.push(feature["@data_id"]) if feature["@code"].Value == 52
				banish = true if feature["@code"].Value == 54 && feature["@data_id"] == id
			end
			class_features.each do |feature| 
				type.push(feature["@data_id"]) if feature["@code"].Value == 52
				banish = true if feature["@code"].Value == 54 && feature["@data_id"] == id
			end
			return [] if banish
			return Data["armor"].select {|weapon| type.include?(weapon["@atype_id"]) && weapon["@etype_id"].Value == id }
		end
		def isDouble(control)
			actor_class = Data["class"][control.Container.Container.Value["@class_id"]]
			actor_features = control.Container.Container.Value["@features"]
			class_features = actor_class["@features"]
			for feature in class_features
				return true if feature["@code"].Value == 55
			end
			for feature in actor_features
				return true if feature["@code"].Value == 55
			end
			false
		end
	end
end

=begin
属性
nickname 
二つ名。

class_id 
職業 ID。

initial_level 
初期レベル。

max_level 
最高レベル。

character_name 
歩行グラフィックのファイル名。

character_index 
歩行グラフィックのインデックス (0..7) 。

face_name 
顔グラフィックのファイル名。

face_index 
顔グラフィックのインデックス (0..7) 。

equips 
初期装備。以下を添字とする、武器 ID または防具 ID の配列です。

0 : 武器 
1 : 盾 
2 : 頭 
3 : 身体 
4 : 装飾品 
=end