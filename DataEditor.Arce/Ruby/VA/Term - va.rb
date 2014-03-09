# This file is in coding: utf-8
# Arce Script: System - va.rb
# Descrobe the Actor page in RPG Maker VX Ace

require "Ruby/VA/File - va.rb"

page = Builder.Add(:tab , { :text => "用语" }) do
		text = Text.new { |i, target| sprintf("%02d:%s", i, target) }
	Builder.Add(:metro , { :text => "属性" }) do
		Builder.Add(:paper , {:actual => :elements, :label => 0, :textbook => text})
	end
	Builder.Add(:metro , { :text => "技能类型" }) do
		Builder.Add(:paper , {:actual => :skill_types , :label => 0, :textbook => text})
	end
	Builder.Next
	Builder.Add(:metro , { :text => "武器类型" }) do
		Builder.Add(:paper , {:actual => :weapon_types , :label => 0, :textbook => text})
	end
	Builder.Add(:metro , { :text => "护甲类型" }) do
		Builder.Add(:paper , {:actual => :armor_types , :label => 0, :textbook => text})
	end
	Builder.Next
	Builder.Add(:metro , {:actual => :terms }) do
		Builder.Add(:metro , {:actual => :basic , :text => "基本用语"}) do
			Builder.Add(:text , {:actual => :INDEX0 , :text => "等级" })
			Builder.Add(:text , {:actual => :INDEX2 , :text => "体力值" })
			Builder.Add(:text , {:actual => :INDEX4 , :text => "魔力值" })
			Builder.Add(:text , {:actual => :INDEX6 , :text => "特技值" })
			Builder.Next
			Builder.Add(:text , {:actual => :INDEX1 , :text => "等级（缩写）" })
			Builder.Add(:text , {:actual => :INDEX3 , :text => "体力值（缩写）" })
			Builder.Add(:text , {:actual => :INDEX5 , :text => "魔力值（缩写）" })
			Builder.Add(:text , {:actual => :INDEX7 , :text => "特技值（缩写）" })
		end
		Builder.Add(:metro , {:actual => :params , :text => "能力" }) do
			Builder.Order
			Builder.Add(:text , {:actual => :INDEX0 , :text => "体力上限" })
			Builder.Add(:text , {:actual => :INDEX1 , :text => "魔力上限" })
			Builder.Next
			Builder.Add(:text , {:actual => :INDEX2 , :text => "物理攻击" })
			Builder.Add(:text , {:actual => :INDEX3 , :text => "物理防御" })
			Builder.Next
			Builder.Add(:text , {:actual => :INDEX4 , :text => "魔法攻击" })
			Builder.Add(:text , {:actual => :INDEX5 , :text => "魔法防御" })
			Builder.Next
			Builder.Add(:text , {:actual => :INDEX6 , :text => "敏捷值" })
			Builder.Add(:text , {:actual => :INDEX7 , :text => "幸运值" })
			Builder.Next
		end
		Builder.Add(:metro , {:actual => :etypes , :text => "装备位置" }) do
			Builder.Order
			Builder.Add(:text , {:actual => :INDEX0 , :text => "武器" })
			Builder.Add(:text , {:actual => :INDEX1 , :text => "盾" })
			Builder.Next
			Builder.Add(:text , {:actual => :INDEX2 , :text => "头盔" })
			Builder.Add(:text , {:actual => :INDEX3 , :text => "铠甲" })
			Builder.Next
			Builder.Add(:text , {:actual => :INDEX4 , :text => "饰品" })
		end
		Builder.Next
		Builder.Add(:metro , {:actual => :commands , :text => "指令" }) do
			Builder.Order
			Builder.Add(:text , {:actual => :INDEX0 , :text => "战斗" })
			Builder.Add(:text , {:actual => :INDEX1 , :text => "撤退" })
			Builder.Next
			Builder.Add(:text , {:actual => :INDEX2 , :text => "攻击" })
			Builder.Add(:text , {:actual => :INDEX3 , :text => "防御" })
			Builder.Next
			Builder.Add(:text , {:actual => :INDEX4 , :text => "物品" })
			Builder.Add(:text , {:actual => :INDEX5 , :text => "技能" })
			Builder.Next
			Builder.Add(:text , {:actual => :INDEX6 , :text => "装备" })
			Builder.Add(:text , {:actual => :INDEX7 , :text => "状态" })
			Builder.Next
			Builder.Add(:text , {:actual => :INDEX8 , :text => "整队" })
			Builder.Add(:text , {:actual => :INDEX9 , :text => "存档" })
			Builder.Next
			Builder.Add(:text , {:actual => :INDEX10 , :text => "结束游戏" })
			Builder.Next
			Builder.Add(:text , {:actual => :INDEX12 , :text => "武器" })
			Builder.Add(:text , {:actual => :INDEX13 , :text => "护甲" })
			Builder.Next
			Builder.Add(:text , {:actual => :INDEX14 , :text => "贵重物品" })
			Builder.Add(:text , {:actual => :INDEX15 , :text => "更换装备" })
			Builder.Next
			Builder.Add(:text , {:actual => :INDEX16 , :text => "最强装备" })
			Builder.Add(:text , {:actual => :INDEX17 , :text => "全部卸下" })
			Builder.Next
			Builder.Add(:text , {:actual => :INDEX18 , :text => "开始游戏" })
			Builder.Add(:text , {:actual => :INDEX19 , :text => "继续游戏" })
			Builder.Next
			Builder.Add(:text , {:actual => :INDEX20 , :text => "退出游戏" })
			Builder.Add(:text , {:actual => :INDEX21 , :text => "回到标题" })
			Builder.Next
			Builder.Add(:text , {:actual => :INDEX22 , :text => "取消" })
			Builder.Next
		end
	end
end
page.Value = Data["system"]

=begin
basic 
基本ステータス。以下を添字とする文字列の配列です。

0 : レベル 
1 : レベル (短) 
2 : HP 
3 : HP (短) 
4 : MP 
5 : MP (短) 
6 : TP 
7 : TP (短) 
params 
能力値。以下を添字とする文字列の配列です。

0 : 最大HP 
1 : 最大MP 
2 : 攻撃力 
3 : 防御力 
4 : 魔法力 
5 : 魔法防御 
6 : 敏捷性 
7 : 運 
etypes 
装備タイプ。以下を添字とする文字列の配列です。

0 : 武器 
1 : 盾 
2 : 頭 
3 : 身体 
4 : 装飾品 
commands 
コマンド。以下を添字とする文字列の配列です。

0 : 戦う 
1 : 逃げる 
2 : 攻撃 
3 : 防御 
4 : アイテム 
5 : スキル 
6 : 装備 
7 : ステータス 
8 : 並び替え 
9 : セーブ 
10 : ゲーム終了 
11 : (欠番) 
12 : 武器 
13 : 防具 
14 : 大事なもの 
15 : 装備変更 
16 : 最強装備 
17 : 全て外す 
18 : ニューゲーム 
19 : コンティニュー 
20 : シャットダウン 
21 : タイトルへ 
22 : やめる 
=end