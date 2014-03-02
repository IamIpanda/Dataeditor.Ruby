# This file is in coding: utf-8
# Arce Script : Item - va.rb
# Describe the user interface for Item in RPG Maker VX Ace

require "Ruby/VA/File - va.rb"
require "Ruby/VA/Damage - va.rb"
require "Ruby/VA/Effect - va.rb"

Builder.Add(:tab , {  :text => "物品" }) do
	list = Builder.Add(:list, {:textbook => Help.Get_Default_Text ,:text => "物品"}) do
		Builder.Add(:metro, { :text => "基本设置" }) do
				Builder.Order
			Builder.Add(:text, {:actual => :name , :text => "名称" })
			Builder.Add(:icon, {
				:actual => :icon_index,
				:image => "Graphics/System/Iconset",
				:text => "图标",
				:split => Help::ICON_SPLIT
			})
				Builder.Next
			Builder.Add(:text, {:actual => :description, :text => "说明" })
				Builder.Next
			Builder.Add(:choose,{
				:actual => :itype_id,
				:text => "物品类型",
				:choice => {
					1 => "普通物品",
					2 => "贵重物品"
				}})
			Builder.Add(:int , {:actual => :price , :text => "价格" })
			Builder.Add(:bool_choose , {:actual => :consumable , :text => "消耗品" })
				Builder.Next
			Builder.Add(:choose, {:actual => :scope, :text => "效果范围", :choice => {
				0 => "无",
				1 => "单个敌人",
				2 => "一个随机敌人",
				3 => "两个随机敌人",
				4 => "三个随机敌人",
				5 => "四个随机敌人",
				6 => "单个队友",
				7 => "全体队友",
				9 => "单个队友（无法战斗）",
				10 => "全体队友（无法战斗）",
				11 => "使用者"
				}})
			Builder.Add(:choose , {:actual => :occasion , :text => "使用场合" , :choice => {
				0 => "随时可用",
				1 => "仅战斗中",
				2 => "仅菜单中",
				3 => "不可使用"
				}})
		end
		Builder.Add(:metro, {:text => "使用"}) do
				Builder.Order
			Builder.Add(:int , {:actual => :speed , :text => "速度修正" })
			Builder.Add(:int , {:actual => :success_rate , :text => "成功几率" })
			Builder.Add(:int , {:actual => :repeats , :text => "连续次数" })
			Builder.Add(:int , {:actual => :tp_gain, :text => "获取特技值" })
				Builder.Next
			Builder.Add(:choose , {:actual => :hit_type , :text => "命中类型" , :choice => {
				0 => "必定命中",
				1 => "魔法攻击",
				2 => "物理攻击"
				}})
			Builder.Add(:choose , {
				:actual => :animation_id ,
			 	:text => "动画" ,
				:choice => { nil => Filechoice.new("animation") }  
			})
		end
			Builder.Next
		VA_Help::Damage.build_damage
		Builder.Add(:metro, :text => "使用效果") do
			VA_Help::Effect.build_effect
		end
		Builder.Add(:metro, :text => "备注") do
			Builder.Add(:text , {:actual => :note , :label => 0})
		end
	end
	list.Value = Data["item"]
end
