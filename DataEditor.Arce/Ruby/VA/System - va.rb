# This file is in coding: utf-8
# Arce Script: System - va.rb
# Descrobe the System page in RPG Maker VX Ace

require "Ruby/VA/File - va.rb"
require "Ruby/Fuzzy.rb"

page = Builder.Add(:tab , { :text => "系统" }) do
	Builder.Order
	Builder.Add(:metro , { :text => "初始同伴" }) do			
		columns = ["角色"]
		texts = []
		texts[0] = Text.new do |target, watch, i, j, k|
			actor = Data["actor"][target]
			Help.Auto_Get_Text(actor, watch)
		end
		window = Proc.new do |window, target|
			Builder.In(window)
			Builder.Add(:choose , {:actual => "", :text => "角色", :choice => { nil => Filechoice.new("actor")} })
			Builder.Out
			window.value = target
		end
		Builder.Add(:view , {
				:actual => :party_members ,
				:label => 0,
				:catalogue => texts ,
				:columns => columns, 
				:texts => texts, 
				:window => window, 
				:window_style => 0, 
				:new => [1].to_fuzzy,
				:width => 250
			})
	end
	Builder.Add(:metro) do
		Builder.Order
		Builder.Add(:metro , { :text => "游戏标题" }) do
			Builder.Add(:text , {:actual => :game_title , :label => 0})
		end
		Builder.Add(:metro , { :text => "货币单位" }) do
			Builder.Add(:text , {:actual => :currency_unit , :label => 0})
		end
		Builder.Next
		Builder.Add(:metro , { :text => "载具图像" }) do
			Builder.Order
			Builder.Add(:metro, {:actual => :boat }) do			
				Builder.Add(:image , {
				:actual => {
				:name => :character_name, 
				:index => :character_index } , 
				:text => "小舟",
				:path => "Graphics/Characters",
				:show => Help::VX_IMAGE_SHOW,
				:split => Help::VX_IMAGE_SPLIT 
				})
			end
			Builder.Add(:metro, {:actual => :ship }) do			
				Builder.Add(:image , {
				:actual => {
				:name => :character_name, 
				:index => :character_index } , 
				:text => "大船",
				:path => "Graphics/Characters",
				:show => Help::VX_IMAGE_SHOW,
				:split => Help::VX_IMAGE_SPLIT 
				})
			end
			Builder.Add(:metro, {:actual => :airship }) do			
				Builder.Add(:image , {
				:actual => {
				:name => :character_name, 
				:index => :character_index } , 
				:text => "飞艇",
				:path => "Graphics/Characters",
				:show => Help::VX_IMAGE_SHOW,
				:split => Help::VX_IMAGE_SPLIT 
				})
			end
		end
		Builder.Add(:metro , { :text => "窗口颜色" }) do
			
		end
	end
	Builder.Add(:metro , { :text => "选项" }) do
		Builder.Add(:check , {:actual => :opt_use_midi, :text => "启动时注册 MIDI" })
		Builder.Add(:check , {:actual => :opt_transparent, :text => "初始时玩家行走图透明" })
		Builder.Add(:check , {:actual => :opt_followers, :text => "队列行进" })
		Builder.Add(:check , {:actual => :opt_slip_death, :text => "慢性伤害可导致无法战斗" })
		Builder.Add(:check , {:actual => :opt_floor_death, :text => "地形伤害可导致无法战斗" })
		Builder.Add(:check , {:actual => :opt_display_tp, :text => "在战斗画面显示特技值" })
		Builder.Add(:check , {:actual => :opt_extra_exp, :text => "替补队员可获得经验值" })
	end
	Builder.Next
	Builder.Add(:metro , { :text => "音乐" }) do
		Builder.Add(:audio, { :actual => :title_bgm, :text => "标题画面", :type => "BGM" })
		Builder.Add(:audio, { :actual => :battle_bgm, :text => "战斗画面", :type => "BGM" })
		Builder.Add(:audio, { :actual => :battle_end_me, :text => "战斗结束", :type => "ME" })
		Builder.Add(:audio, { :actual => :gameover_me, :text => "游戏结束", :type => "ME" })
		Builder.Add(:metro, { :actual => :boat }) do
			Builder.Add(:audio , {:actual => :bgm , :text => "乘坐小舟" , :type => "BGM"})
		end
		Builder.Add(:metro, { :actual => :ship }) do
			Builder.Add(:audio , {:actual => :bgm , :text => "乘坐大船" , :type => "BGM"})
		end
		Builder.Add(:metro, { :actual => :airship }) do
			Builder.Add(:audio , {:actual => :bgm , :text => "乘坐飞艇" , :type => "BGM"})
		end
	end
	Builder.Add(:metro , { :text => "声音", :actual => :sounds }) do
		Builder.Add(:audio , {:actual => :INDEX0 , :type => "SE" , :text => "光标移动" })
		Builder.Add(:audio , {:actual => :INDEX1 , :type => "SE" , :text => "确定" })
		Builder.Add(:audio , {:actual => :INDEX2 , :type => "SE" , :text => "取消" })
		Builder.Add(:audio , {:actual => :INDEX3 , :type => "SE" , :text => "无效" })
		Builder.Add(:audio , {:actual => :INDEX4 , :type => "SE" , :text => "装备" })
		Builder.Add(:audio , {:actual => :INDEX5 , :type => "SE" , :text => "存档" })
		Builder.Add(:audio , {:actual => :INDEX6 , :type => "SE" , :text => "读档" })
		Builder.Add(:audio , {:actual => :INDEX7 , :type => "SE" , :text => "战斗开始" })
		Builder.Next
		Builder.Add(:audio , {:actual => :INDEX8 , :type => "SE" , :text => "撤退" })
		Builder.Add(:audio , {:actual => :INDEX9 , :type => "SE" , :text => "敌人普通攻击" })
		Builder.Add(:audio , {:actual => :INDEX10 , :type => "SE" , :text => "敌人受到伤害" })
		Builder.Add(:audio , {:actual => :INDEX11 , :type => "SE" , :text => "敌人被消灭" })
		Builder.Add(:audio , {:actual => :INDEX12 , :type => "SE" , :text => "首领被消灭 1" })
		Builder.Add(:audio , {:actual => :INDEX13 , :type => "SE" , :text => "首领被消灭 2" })
		Builder.Add(:audio , {:actual => :INDEX14 , :type => "SE" , :text => "队友受到伤害" })
		Builder.Add(:audio , {:actual => :INDEX15 , :type => "SE" , :text => "队友无法战斗" })
		Builder.Next
		Builder.Add(:audio , {:actual => :INDEX16 , :type => "SE" , :text => "回复" })
		Builder.Add(:audio , {:actual => :INDEX17 , :type => "SE" , :text => "落空" })
		Builder.Add(:audio , {:actual => :INDEX18 , :type => "SE" , :text => "闪避普通攻击" })
		Builder.Add(:audio , {:actual => :INDEX19 , :type => "SE" , :text => "闪避魔法攻击" })
		Builder.Add(:audio , {:actual => :INDEX20 , :type => "SE" , :text => "反射魔法攻击" })
		Builder.Add(:audio , {:actual => :INDEX21 , :type => "SE" , :text => "商店" })
		Builder.Add(:audio , {:actual => :INDEX22 , :type => "SE" , :text => "使用物品" })
		Builder.Add(:audio , {:actual => :INDEX23 , :type => "SE" , :text => "使用技能" })
		Builder.Next
	end
	Builder.Add(:metro , { :text => "初始位置" }) do

	end

	Builder.Add(:metro , { :text => "标题画面" }) do
		Builder.Add(:layers , {:actual => {:battleback1_name, :battleback2_name} , :text => "图像" })
		Builder.Add(:check , {:actual => :opt_draw_title , :text => "绘制标题文字" })
	end
end
page.Value = Data["system"]

=begin
属性
game_title 
ゲームタイトル。

version_id 
更新チェック用の乱数。ツクールでデータを保存するたびに異なる値に書き換えられます。

japanese 
日本語版では常に true になります。

party_members 
初期パーティ。アクター ID の配列です。

currency_unit 
通貨単位。

window_tone 
ウィンドウカラー (Tone)。

elements 
属性名のリスト。属性 ID を添字に取る文字列の配列で、0 番目の要素は nil です。

skill_types 
スキルタイプ名のリスト。スキルタイプ ID を添字に取る文字列の配列で、0 番目の要素は nil です。

weapon_types 
武器タイプ名のリスト。武器タイプ ID を添字に取る文字列の配列で、0 番目の要素は nil です。
bi
armor_types 
防具タイプ名のリスト。防具タイプ ID を添字に取る文字列の配列で、0 番目の要素は nil です。

switches 
スイッチ名のリスト。スイッチ ID を添字に取る文字列の配列で、0 番目の要素は nil です。

variables 
変数名のリスト。変数 ID を添字に取る文字列の配列で、0 番目の要素は nil です。

boat 
小型船の設定 (RPG::System::Vehicle) 。

ship 
大型船の設定 (RPG::System::Vehicle) 。

airship 
飛行船の設定 (RPG::System::Vehicle) 。

title1_name 
タイトル（背景）グラフィックのファイル名。

title2_name 
タイトル（枠）グラフィックのファイル名。

opt_draw_title 
オプション［ゲームタイトルの描画］の真偽値。

opt_use_midi 
オプション［起動時に MIDI を初期化］の真偽値。

opt_transparent 
オプション［透明状態で開始］の真偽値。

opt_followers 
オプション［パーティの隊列歩行］の真偽値。

opt_slip_death 
オプション［スリップダメージで戦闘不能］の真偽値。

opt_floor_death 
オプション［床ダメージで戦闘不能］の真偽値。

opt_display_tp 
オプション［バトル画面で TP を表示］の真偽値。

opt_extra_exp 
オプション［控えメンバーも経験値を獲得］の真偽値。

title_bgm 
タイトル BGM (RPG::BGM) 。

battle_bgm 
戦闘 BGM (RPG::BGM) 。

battle_end_me 
戦闘終了 ME (RPG::ME) 。

gameover_me 
ゲームオーバー ME (RPG::ME) 。

sounds 
効果音。RPG::SE の配列です。

start_map_id 
プレイヤーの初期位置の、マップ ID。

start_x 
プレイヤーの初期位置の、マップ X 座標。

start_y 
プレイヤーの初期位置の、マップ Y 座標。

terms 
用語 (RPG::System::Terms) 。

test_battlers 
戦闘テストのパーティ設定。RPG::System::TestBattler の配列です。

test_troop_id 
戦闘テストの敵グループ ID。

battleback1_name 
敵グループの編集および戦闘テストで使用する、戦闘背景（床）グラフィックのファイル名。

battleback2_name 
敵グループの編集および戦闘テストで使用する、戦闘背景（壁）グラフィックのファイル名。

battler_name 
アニメーションの編集で使用する、戦闘グラフィックのファイル名。

battler_hue 
アニメーションの編集で使用する、戦闘グラフィックの色相変化値 (0..360) 。

edit_map_id 
内部用。現在編集しているマップの ID。


=end