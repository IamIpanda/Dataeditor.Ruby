# This file is in coding: ascii
# Arce Script: event - xp.rb
# describe the parameters for event
# This file can't run lonely.

require "ruby/Event.rb"

$commands_xp = {}
$commands_xp[0] = Command.new(0, -1, "TAB", "空指令", Text.ret(""))
#=================================================================
# Code 101
# 显示文章
#-----------------------------------------------------------------
# Parameter : ["显示文章"]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  "文章 : ".encode + parameters[0].Text
end
target_window = Proc.new do |window, commands|
  Builder.In(window) do
  end
  Builder.Out
  window
end
target_with = Proc.new do |command|
  
end
$commands_xp[101] = Command.new(101, -1, "MESSAGE", "显示对话", target_text, "t", target_window, target_with, 0, 0)

#=================================================================
# Code 102
# 显示选择项
#-----------------------------------------------------------------
# Parameter : [["这是选择项的第一项", "这是选择项的第二项"], 2]
#=================================================================
target_text = Text.new do |parameters, *followings|
  array = parameters[0]
  text = array.collect { |f| f.Text }
  "显示选择项 : [".encode + text.join(", ") + "]"
end
target_window = Proc.new do |window, commands|
  Builder.In(window) do
  end
  Builder.Out
  window
end
target_with = Proc.new do |command|

end
$commands_xp[102] = Command.new(102, -1, "CHOOSE", "显示选择项", target_text, "ai", target_window, target_with, 0, 0)

#=================================================================
# Code 103
# 处理数字输入
#-----------------------------------------------------------------
# Parameter : [3, 3]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  "处理数字输入 : , ".encode + "#{parameters[1].Value}" + " 位".encode
end
target_window = Proc.new do |window, commands|
  Builder.In(window) do
  end
  Builder.Out
  window
end
$commands_xp[103] = Command.new(103, -1, "INPUTNUM", "处理数字输入", target_text, "ii", target_window, nil, 0, 0)

#=================================================================
# Code 104
# 更改文章选项
#-----------------------------------------------------------------
# Parameter : [1, 1]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  arg0 = parameters[0].Value
  arg1 = parameters[1].Value
  choice0 = ["上","中","下"]
  choice1 = ["透明","不透明"]
  "更改文章选项 : ".encode + "#{choice0[arg0]}, #{choice1[arg1]}"
end
target_window = Proc.new do |window, commands|
  Builder.In(window) do
  end
  Builder.Out
  window
end
$commands_xp[104] = Command.new(104, -1, "DIALOG", "更改文章选项", target_text, "ii", target_window, nil, 0, 0)

#=================================================================
# Code 105
# 处理按键输入
#-----------------------------------------------------------------
# Parameter : [5]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  "处理按键输入".encode
end
target_window = Proc.new do |window, commands|
  Builder.In(window) do
  end
  Builder.Out
  window
end
$commands_xp[105] = Command.new(105, -1, "INPUTKEY", "处理按键输入", target_text, "i", target_window, nil, 0, 0)

#=================================================================
# Code 106
# 等待
#-----------------------------------------------------------------
# Parameter : [6]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  "等待 #{parameters[0].Value} 帧".encode
end
target_window = Proc.new do |window, commands|
  Builder.In(window) do
  end
  Builder.Out
  window
end
$commands_xp[106] = Command.new(106, -1, "WAIT", "等待", target_text, "i", target_window, nil, 0, 0)

#=================================================================
# Code 108
# 注释
#-----------------------------------------------------------------
# Parameter : ["这是注释"]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  "注释 : ".encode + "#{parameters[0].Text}"
end
target_window = Proc.new do |window, commands|
  Builder.In(window) do
  end
  Builder.Out
  window
end
target_with = Proc.new do |command|

end
$commands_xp[108] = Command.new(108, -1, "REM", "注释", target_text, "t", target_window, target_with, 0, 0)

#=================================================================
# Code 111
# 条件分歧
#-----------------------------------------------------------------
# Parameter : [0, 1, 0]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  "条件分歧".encode
end
target_window = Proc.new do |window, commands|
  Builder.In(window) do
  end
  Builder.Out
  window
end
$commands_xp[111] = Command.new(111, -1, "IF", "条件分歧", target_text, "iii", target_window, nil, 0, 0)

#=================================================================
# Code 112
# 循环
#-----------------------------------------------------------------
# Parameter : []
#=================================================================
target_text = Text.ret("循环")
$commands_xp[112] = Command.new(112, -1, "DO", "循环", target_text, "", nil, nil, 0, 0)

#=================================================================
# Code 113
# 中断循环
#-----------------------------------------------------------------
# Parameter : []
#=================================================================
target_text = Text.ret("中断循环")
$commands_xp[113] = Command.new(113, -1, "LOOP", "中断循环", target_text, "", nil, nil, 0, 0)

#=================================================================
# Code 115
# 中断事件处理
#-----------------------------------------------------------------
# Parameter : []
#=================================================================
target_text = Text.ret("中断事件处理")
$commands_xp[115] = Command.new(115, -1, "YIELD", "中断事件处理", target_text, "", nil, nil, 0, 0)

#=================================================================
# Code 116
# 暂时消除事件
#-----------------------------------------------------------------
# Parameter : []
#=================================================================
target_text = Text.ret("暂时消除事件")
$commands_xp[116] = Command.new(116, -1, "ERASE", "暂时消除事件", target_text, "", nil, nil, 0, 0)

#=================================================================
# Code 117
# 公共事件
#-----------------------------------------------------------------
# Parameter : [1]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  "公共事件 : ".encode + Event_Help.value(parameters[0].Value, Data["commonevnet"])
end
target_window = Proc.new do |window, commands|
  Builder.In(window) do
  end
  Builder.Out
  window
end
$commands_xp[117] = Command.new(117, -1, "COMMONEVENT", "公共事件", target_text, "i", target_window, nil, 0, 0)

#=================================================================
# Code 118
# 标签
#-----------------------------------------------------------------
# Parameter : ["这是标签"]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  "标签 : ".encode + "#{parameters[0].Text}]"
end
target_window = Proc.new do |window, commands|
  Builder.In(window) do
  end
  Builder.Out
  window
end
$commands_xp[118] = Command.new(118, -1, "LABEL", "标签", target_text, "s", target_window, nil, 0, 0)

#=================================================================
# Code 119
# 跳转标签
#-----------------------------------------------------------------
# Parameter : ["这是跳转标签"]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  "跳转标签 : ".encode + "[#{parameters[0].Text}]"
end
target_window = Proc.new do |window, commands|
  Builder.In(window) do
  end
  Builder.Out
  window
end
$commands_xp[119] = Command.new(119, -1, "JMP", "跳转标签", target_text, "s", target_window, nil, 0, 0)

#=================================================================
# Code 121
# 开关操作
#-----------------------------------------------------------------
# Parameter : [10, 20, 0]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  "开关操作".encode
end
target_window = Proc.new do |window, commands|
  Builder.In(window) do
  end
  Builder.Out
  window
end
$commands_xp[121] = Command.new(121, -1, "SWITCH", "开关操作", target_text, "iii", target_window, nil, 0, 0)

#=================================================================
# Code 122
# 变量操作
#-----------------------------------------------------------------
# Parameter : [20, 30, 2, 2, 40, 50]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  "变量操作".encode
end
target_window = Proc.new do |window, commands|
  Builder.In(window) do
  end
  Builder.Out
  window
end
$commands_xp[122] = Command.new(122, -1, "VARIABLE", "变量操作", target_text, "iiiiii", target_window, nil, 0, 0)

#=================================================================
# Code 123
# 独立开关操作
#-----------------------------------------------------------------
# Parameter : ["C", 0]
#=================================================================
target_text = Text.new do |parameters, *followings|

  "独立开关".encode + parameters[0].Text + " = " 
end
target_window = Proc.new do |window, commands|
  Builder.In(window) do
  end
  Builder.Out
  window
end
$commands_xp[123] = Command.new(123, -1, "SINGLESWITCH", "独立开关", target_text, "si", target_window, nil, 0, 0)

#=================================================================
# Code 124
# 定时器操作
#-----------------------------------------------------------------
# Parameter : [0, 920]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  sec = parameters[0].Value
  min = sec / 60
  sec -= min * 60
  "定时器操作".encode + min.to_s + " 分 ".encode + sec.to_s + " 秒".encode
end
target_window = Proc.new do |window, commands|
  Builder.In(window) do
  end
  Builder.Out
  window
end
$commands_xp[124] = Command.new(124, -1, "TIMER", "定时器操作", target_text, "ii", target_window, nil, 0, 0)

#=================================================================
# Code 125
# 增减金钱
#-----------------------------------------------------------------
# Parameter : [0, 0, 666]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  "增减金钱"
end
target_window = Proc.new do |window, commands|
  Builder.In(window) do
  end
  Builder.Out
  window
end
$commands_xp[125] = Command.new(125, -1, "GOLD", "增减金钱", target_text, "iii", target_window, nil, 0, 0)

#=================================================================
# Code 126
# 增减物品
#-----------------------------------------------------------------
# Parameter : [21, 0, 0, 7]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  "增减物品"
end
target_window = Proc.new do |window, commands|
  Builder.In(window) do
  end
  Builder.Out
  window
end
$commands_xp[126] = Command.new(126, -1, "ITEM", "增减物品", target_text, "iiii", target_window, nil, 0, 0)

#=================================================================
# Code 127
# 增减武器
#-----------------------------------------------------------------
# Parameter : [8, 0, 0, 8]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  "增减武器"
end
target_window = Proc.new do |window, commands|
  Builder.In(window) do
  end
  Builder.Out
  window
end
$commands_xp[127] = Command.new(127, -1, "WEAPON", "增减武器", target_text, "iiii", target_window, nil, 0, 0)

#=================================================================
# Code 128
# 增减防具
#-----------------------------------------------------------------
# Parameter : [9, 1, 1, 9]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  "增减防具"
end
target_window = Proc.new do |window, commands|
  Builder.In(window) do
  end
  Builder.Out
  window
end
$commands_xp[128] = Command.new(128, -1, "ARMOR", "增减防具", target_text, "iiii", target_window, nil, 0, 0)

#=================================================================
# Code 129
# 替换队员
#-----------------------------------------------------------------
# Parameter : [5, 1, 1]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  "替换队员"
end
target_window = Proc.new do |window, commands|
  Builder.In(window) do
  end
  Builder.Out
  window
end
$commands_xp[129] = Command.new(129, -1, "ACTOR", "替换队员", target_text, "iii", target_window, nil, 0, 0)

#=================================================================
# Code 131
# 更改窗口外观
#-----------------------------------------------------------------
# Parameter : ["001-Blue01"]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  "更改窗口外观".encode + parameters[0].Text
end
target_window = Proc.new do |window, commands|
  Builder.In(window) do
  end
  Builder.Out
  window
end
$commands_xp[131] = Command.new(131, -1, "WINDOWSKIN", "更改窗口外观", target_text, "s", target_window, nil, 0, 0)

#=================================================================
# Code 132
# 更改战斗 BGM
#-----------------------------------------------------------------
# Parameter : [#<RPG::AudioFile:0x17d3560 @volume=35, @name="016-Theme05", @pitch=100>]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  ""
end
target_window = Proc.new do |window, commands|
  Builder.In(window) do
  end
  Builder.Out
  window
end
$commands_xp[132] = Command.new(132, -1, "BATTLEBGM", "更改战斗 BGM", target_text, "d", target_window, nil, 0, 0)

#=================================================================
# Code 133
# 更改战斗结束的 ME
#-----------------------------------------------------------------
# Parameter : [#<RPG::AudioFile:0x17d3470 @volume=100, @name="010-Item01", @pitch=100>]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  ""
end
target_window = Proc.new do |window, commands|
  Builder.In(window) do
  end
  Builder.Out
  window
end
$commands_xp[133] = Command.new(133, -1, "BATTLEME", "更改战斗结束的 ME", target_text, "d", target_window, nil, 0, 0)

#=================================================================
# Code 134
# 更改禁止存档
#-----------------------------------------------------------------
# Parameter : [0]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  ""
end
target_window = Proc.new do |window, commands|
  Builder.In(window) do
  end
  Builder.Out
  window
end
$commands_xp[134] = Command.new(134, -1, "SAVEENABLED", "更改禁止存档", target_text, "i", target_window, nil, 0, 0)

#=================================================================
# Code 135
# 更改禁止菜单
#-----------------------------------------------------------------
# Parameter : [1]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  ""
end
target_window = Proc.new do |window, commands|
  Builder.In(window) do
  end
  Builder.Out
  window
end
$commands_xp[135] = Command.new(135, -1, "MENUENABLED", "更改禁止菜单", target_text, "i", target_window, nil, 0, 0)

#=================================================================
# Code 136
# 更改禁止遇敌
#-----------------------------------------------------------------
# Parameter : [1]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  ""
end
target_window = Proc.new do |window, commands|
  Builder.In(window) do
  end
  Builder.Out
  window
end
$commands_xp[136] = Command.new(136, -1, "ENEMYENABLED", "更改禁止遇敌", target_text, "i", target_window, nil, 0, 0)

#=================================================================
# Code 201
# 场所移动
#-----------------------------------------------------------------
# Parameter : [1, 14, 15, 16, 4, 0]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  ""
end
target_window = Proc.new do |window, commands|
  Builder.In(window) do
  end
  Builder.Out
  window
end
$commands_xp[201] = Command.new(201, -1, "TRANSPORT", "场所移动", target_text, "iiiiii", target_window, nil, 0, 0)

#=================================================================
# Code 202
# 设置事件位置
#-----------------------------------------------------------------
# Parameter : [0, 0, 10, 10, 0]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  ""
end
target_window = Proc.new do |window, commands|
  Builder.In(window) do
  end
  Builder.Out
  window
end
$commands_xp[202] = Command.new(202, -1, "SETPOS", "设置事件位置", target_text, "iiiii", target_window, nil, 0, 0)

#=================================================================
# Code 203
# 画面卷动
#-----------------------------------------------------------------
# Parameter : [8, 67, 4]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  ""
end
target_window = Proc.new do |window, commands|
  Builder.In(window) do
  end
  Builder.Out
  window
end
$commands_xp[203] = Command.new(203, -1, "SCROLL", "画面卷动", target_text, "iii", target_window, nil, 0, 0)

#=================================================================
# Code 204
# 更改地图设置
#-----------------------------------------------------------------
# Parameter : [0, "004-CloudySky01", 0]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  ""
end
target_window = Proc.new do |window, commands|
  Builder.In(window) do
  end
  Builder.Out
  window
end
$commands_xp[204] = Command.new(204, -1, "MAPSET", "更改地图设置", target_text, "isi", target_window, nil, 0, 0)

#=================================================================
# Code 205
# 更改雾色调
#-----------------------------------------------------------------
# Parameter : [(88.000000, 88.000000, 88.000000, 88.000000), 88]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  ""
end
target_window = Proc.new do |window, commands|
  Builder.In(window) do
  end
  Builder.Out
  window
end
$commands_xp[205] = Command.new(205, -1, "FOGSET", "更改雾色调", target_text, "ui", target_window, nil, 0, 0)

#=================================================================
# Code 206
# 更改雾的不透明度
#-----------------------------------------------------------------
# Parameter : [99, 98]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  ""
end
target_window = Proc.new do |window, commands|
  Builder.In(window) do
  end
  Builder.Out
  window
end
$commands_xp[206] = Command.new(206, -1, "FOGTRANSPARENT", "更改雾的不透明度", target_text, "ii", target_window, nil, 0, 0)

#=================================================================
# Code 207
# 显示动画
#-----------------------------------------------------------------
# Parameter : [0, 20]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  ""
end
target_window = Proc.new do |window, commands|
  Builder.In(window) do
  end
  Builder.Out
  window
end
$commands_xp[207] = Command.new(207, -1, "ANIMATION", "显示动画", target_text, "ii", target_window, nil, 0, 0)

#=================================================================
# Code 208
# 更改透明状态
#-----------------------------------------------------------------
# Parameter : [0]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  ""
end
target_window = Proc.new do |window, commands|
  Builder.In(window) do
  end
  Builder.Out
  window
end
$commands_xp[208] = Command.new(208, -1, "TRANSPARENT", "更改透明状态", target_text, "i", target_window, nil, 0, 0)

#=================================================================
# Code 210
# 等待移动结束
#-----------------------------------------------------------------
# Parameter : []
#=================================================================
target_text = Text.ret("等待移动结束")
$commands_xp[210] = Command.new(210, -1, "WAITMOVE", "等待移动结束", target_text, "", nil, nil, 0, 0)

#=================================================================
# Code 221
# 准备渐变
#-----------------------------------------------------------------
# Parameter : []
#=================================================================
target_text = Text.ret("准备渐变")
$commands_xp[221] = Command.new(221, -1, "PREPARESEGUE", "准备渐变", target_text, "", nil, nil, 0, 0)

#=================================================================
# Code 222
# 执行渐变
#-----------------------------------------------------------------
# Parameter : ["009-Random01"]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  "执行渐变 :".encode + " #{parameters[0].Text}"
end
target_window = Proc.new do |window, commands|
  Builder.In(window) do
  end
  Builder.Out
  window
end
$commands_xp[222] = Command.new(222, -1, "SEGUE", "执行渐变", target_text, "s", target_window, nil, 0, 0)

#=================================================================
# Code 223
# 更改画面色调
#-----------------------------------------------------------------
# Parameter : [(111.000000, 112.000000, 113.000000, 114.000000), 20]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  ""
end
target_window = Proc.new do |window, commands|
  Builder.In(window) do
  end
  Builder.Out
  window
end
$commands_xp[223] = Command.new(223, -1, "TONESET", "更改画面色调", target_text, "ui", target_window, nil, 0, 0)

#=================================================================
# Code 224
# 画面闪烁
#-----------------------------------------------------------------
# Parameter : [(115.000000, 116.000000, 117.000000, 118.000000), 10]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  ""
end
target_window = Proc.new do |window, commands|
  Builder.In(window) do
  end
  Builder.Out
  window
end
$commands_xp[224] = Command.new(224, -1, "SCREENFLASH", "画面闪烁", target_text, "ci", target_window, nil, 0, 0)

#=================================================================
# Code 225
# 画面震动
#-----------------------------------------------------------------
# Parameter : [9, 1, 120]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  ""
end
target_window = Proc.new do |window, commands|
  Builder.In(window) do
  end
  Builder.Out
  window
end
$commands_xp[225] = Command.new(225, -1, "SCREENSHOCK", "画面震动", target_text, "iii", target_window, nil, 0, 0)

#=================================================================
# Code 231
# 显示图片
#-----------------------------------------------------------------
# Parameter : [1, "", 0, 0, 0, 0, 100, 100, 255, 0]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  ""
end
target_window = Proc.new do |window, commands|
  Builder.In(window) do
  end
  Builder.Out
  window
end
$commands_xp[231] = Command.new(231, -1, "SHOWPIC", "显示图片", target_text, "isiiiiiiii", target_window, nil, 0, 0)

#=================================================================
# Code 232
# 移动图片
#-----------------------------------------------------------------
# Parameter : [2, 20, 0, 0, 0, 0, 100, 100, 255, 0]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  ""
end
target_window = Proc.new do |window, commands|
  Builder.In(window) do
  end
  Builder.Out
  window
end
$commands_xp[232] = Command.new(232, -1, "MOVEPIC", "移动图片", target_text, "iiiiiiiiii", target_window, nil, 0, 0)

#=================================================================
# Code 233
# 旋转图片
#-----------------------------------------------------------------
# Parameter : [3, 5]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  ""
end
target_window = Proc.new do |window, commands|
  Builder.In(window) do
  end
  Builder.Out
  window
end
$commands_xp[233] = Command.new(233, -1, "233", "旋转图片", target_text, "ii", target_window, nil, 0, 0)

#=================================================================
# Code 234
# 更改图片色调
#-----------------------------------------------------------------
# Parameter : [4, (130.000000, 131.000000, 132.000000, 133.000000), 20]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  ""
end
target_window = Proc.new do |window, commands|
  Builder.In(window) do
  end
  Builder.Out
  window
end
$commands_xp[234] = Command.new(234, -1, "TONEPIC", "更改图片色调", target_text, "iui", target_window, nil, 0, 0)

#=================================================================
# Code 235
# 图片消失
#-----------------------------------------------------------------
# Parameter : [5]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  ""
end
target_window = Proc.new do |window, commands|
  Builder.In(window) do
  end
  Builder.Out
  window
end
$commands_xp[235] = Command.new(235, -1, "DISAPPEARPIC", "图片消失", target_text, "i", target_window, nil, 0, 0)

#=================================================================
# Code 236
# 天气设置
#-----------------------------------------------------------------
# Parameter : [1, 6, 140]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  ""
end
target_window = Proc.new do |window, commands|
  Builder.In(window) do
  end
  Builder.Out
  window
end
$commands_xp[236] = Command.new(236, -1, "WEATHERSET", "设置天候", target_text, "iii", target_window, nil, 0, 0)

#=================================================================
# Code 241
# 演奏 BGM
#-----------------------------------------------------------------
# Parameter : [#<RPG::AudioFile:0x17d2900 @volume=100, @name="008-Boss04", @pitch=100>]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  ""
end
target_window = Proc.new do |window, commands|
  Builder.In(window) do
  end
  Builder.Out
  window
end
$commands_xp[241] = Command.new(241, -1, "PLAYBGM", "演奏 BGM", target_text, "d", target_window, nil, 0, 0)

#=================================================================
# Code 242
# BGM 的淡入淡出
#-----------------------------------------------------------------
# Parameter : [4]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  "BGM 的淡入淡出：".encode + "#{parameters[0].Value}" + " 秒".encode
end
target_window = Proc.new do |window, commands|
  Builder.In(window) do
  end
  Builder.Out
  window
end
$commands_xp[242] = Command.new(242, -1, "FADEBGM", "BGM 的淡入淡出", target_text, "i", target_window, nil, 0, 0)

#=================================================================
# Code 245
# 演奏 BGS
#-----------------------------------------------------------------
# Parameter : [#<RPG::AudioFile:0x17d27e0 @volume=80, @name="013-Fire01", @pitch=100>]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  ""
end
target_window = Proc.new do |window, commands|
  Builder.In(window) do
  end
  Builder.Out
  window
end
$commands_xp[245] = Command.new(245, -1, "PLAYBGS", "演奏 BGS", target_text, "d", target_window, nil, 0, 0)

#=================================================================
# Code 246
# BGS 的淡入淡出
#-----------------------------------------------------------------
# Parameter : [2]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  ""
end
target_window = Proc.new do |window, commands|
  Builder.In(window) do
  end
  Builder.Out
  window
end
$commands_xp[246] = Command.new(246, -1, "FADEBGS", "BGS 的淡入淡出", target_text, "i", target_window, nil, 0, 0)

#=================================================================
# Code 247
# 记忆 BGM/BGS
#-----------------------------------------------------------------
# Parameter : []
#=================================================================
target_text = Text.ret("记忆 BGM/BGS")
$commands_xp[247] = Command.new(247, -1, "REMEMBERBGM", "记忆 BGM/BGS", target_text, "", nil, nil, 0, 0)

#=================================================================
# Code 248
# 还原 BGM/BGS
#-----------------------------------------------------------------
# Parameter : []
#=================================================================
target_text = Text.ret("还原 BGM/BGS")
$commands_xp[248] = Command.new(248, -1, "RECOVERBGM", "还原 BGM/BGS", target_text, "", nil, nil, 0, 0)

#=================================================================
# Code 249
# 演奏 ME
#-----------------------------------------------------------------
# Parameter : [#<RPG::AudioFile:0x17d2600 @volume=100, @name="009-Fanfare03", @pitch=100>]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  ""
end
target_window = Proc.new do |window, commands|
  Builder.In(window) do
  end
  Builder.Out
  window
end
$commands_xp[249] = Command.new(249, -1, "PLAYME", "演奏 ME", target_text, "d", target_window, nil, 0, 0)

#=================================================================
# Code 250
# 演奏 SE
#-----------------------------------------------------------------
# Parameter : [#<RPG::AudioFile:0x17d2540 @volume=80, @name="015-Jump01", @pitch=100>]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  ""
end
target_window = Proc.new do |window, commands|
  Builder.In(window) do
  end
  Builder.Out
  window
end
$commands_xp[250] = Command.new(250, -1, "PLAYSE", "演奏 SE", target_text, "d", target_window, nil, 0, 0)

#=================================================================
# Code 251
# 停止 SE
#-----------------------------------------------------------------
# Parameter : []
#=================================================================
target_text = Text.ret("停止 SE")
$commands_xp[251] = Command.new(251, -1, "STOPSE", "停止 SE", target_text, "", nil, nil, 0, 0)


#=================================================================
# Code 301
# 战斗处理
#-----------------------------------------------------------------
# Parameter : [22, false, false]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  ""
end
target_window = Proc.new do |window, commands|
  Builder.In(window) do
  end
  Builder.Out
  window
end
$commands_xp[301] = Command.new(301, -1, "BATTLE", "战斗处理", target_text, "ibb", target_window, nil, 0, 0)

#=================================================================
# Code 302
# 商店处理
#-----------------------------------------------------------------
# Parameter : [0, 1]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  ""
end
target_window = Proc.new do |window, commands|
  Builder.In(window) do
  end
  Builder.Out
  window
end
$commands_xp[302] = Command.new(302, -1, "SHOP", "商店处理", target_text, "ii", target_window, nil, 0, 0)

#=================================================================
# Code 303
# 名称输入处理
#-----------------------------------------------------------------
# Parameter : [5, 6]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  ""
end
target_window = Proc.new do |window, commands|
  Builder.In(window) do
  end
  Builder.Out
  window
end
$commands_xp[303] = Command.new(303, -1, "INPUTNAME", "名称输入处理", target_text, "ii", target_window, nil, 0, 0)

#=================================================================
# Code 311
# 增减 HP
#-----------------------------------------------------------------
# Parameter : [0, 1, 0, 400, true]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  ""
end
target_window = Proc.new do |window, commands|
  Builder.In(window) do
  end
  Builder.Out
  window
end
$commands_xp[311] = Command.new(311, -1, "SETHP", "增减 HP", target_text, "iiiib", target_window, nil, 0, 0)

#=================================================================
# Code 312
# 增减 SP
#-----------------------------------------------------------------
# Parameter : [4, 0, 0, 1]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  ""
end
target_window = Proc.new do |window, commands|
  Builder.In(window) do
  end
  Builder.Out
  window
end
$commands_xp[312] = Command.new(312, -1, "SETMP", "增减 SP", target_text, "iiii", target_window, nil, 0, 0)

#=================================================================
# Code 313
# 更改状态
#-----------------------------------------------------------------
# Parameter : [7, 0, 9]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  ""
end
target_window = Proc.new do |window, commands|
  Builder.In(window) do
  end
  Builder.Out
  window
end
$commands_xp[313] = Command.new(313, -1, "SETSTATE", "更改状态", target_text, "iii", target_window, nil, 0, 0)


#=================================================================
# Code 314
# 完全恢复
#-----------------------------------------------------------------
# Parameter : [0]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  ""
end
target_window = Proc.new do |window, commands|
  Builder.In(window) do
  end
  Builder.Out
  window
end
$commands_xp[314] = Command.new(314, -1, "RECOVERPLAYER", "完全回复", target_text, "i", target_window, nil, 0, 0)

#=================================================================
# Code 315
# 增减同伴
#-----------------------------------------------------------------
# Parameter : [5, 0, 0, 250]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  ""
end
$commands_xp[315] = Command.new(315, -1, "SETCOMPANIES", "增减同伴", target_text, "iiii", nil, nil, 0, 0)
#=================================================================
# Code 316
# 增减等级
#-----------------------------------------------------------------
# Parameter : [0, 1, 0, 56]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  ""
end
target_window = Proc.new do |window, commands|
  Builder.In(window) do
  end
  Builder.Out
  window
end
$commands_xp[316] = Command.new(316, -1, "SETLEVEL", "增减等级", target_text, "iiii", target_window, nil, 0, 0)

#=================================================================
# Code 317
# 增减能力值
#-----------------------------------------------------------------
# Parameter : [5, 0, 0, 0, 251]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  ""
end
target_window = Proc.new do |window, commands|
  Builder.In(window) do
  end
  Builder.Out
  window
end
$commands_xp[317] = Command.new(317, -1, "SETPARAMETER", "增减能力值", target_text, "iiiii", target_window, nil, 0, 0)

#=================================================================
# Code 318
# 增减特技
#-----------------------------------------------------------------
# Parameter : [1, 1, 20]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  ""
end
target_window = Proc.new do |window, commands|
  Builder.In(window) do
  end
  Builder.Out
  window
end
$commands_xp[318] = Command.new(318, -1, "SETSKILL", "增减特技", target_text, "iii", target_window, nil, 0, 0)

#=================================================================
# Code 319
# 变更装备
#-----------------------------------------------------------------
# Parameter : [1, 2, 8]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  ""
end
target_window = Proc.new do |window, commands|
  Builder.In(window) do
  end
  Builder.Out
  window
end
$commands_xp[319] = Command.new(319, -1, "SETBODY", "变更装备", target_text, "iii", target_window, nil, 0, 0)

#=================================================================
# Code 320
# 更改角色姓名
#-----------------------------------------------------------------
# Parameter : [1, "阿尔东斯"]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  ""
end
target_window = Proc.new do |window, commands|
  Builder.In(window) do
  end
  Builder.Out
  window
end
$commands_xp[320] = Command.new(320, -1, "SETACTORNAME", "更改角色姓名", target_text, "is", target_window, nil, 0, 0)

#=================================================================
# Code 321
# 更改角色职业
#-----------------------------------------------------------------
# Parameter : [1, 7]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  ""
end
target_window = Proc.new do |window, commands|
  Builder.In(window) do
  end
  Builder.Out
  window
end
$commands_xp[321] = Command.new(321, -1, "SETACTORCLASS", "更改角色职业", target_text, "ii", target_window, nil, 0, 0)

#=================================================================
# Code 322
# 更改角色图像
#-----------------------------------------------------------------
# Parameter : [1, "025-Cleric01", 200, "021-Hunter02", 290]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  ""
end
target_window = Proc.new do |window, commands|
  Builder.In(window) do
  end
  Builder.Out
  window
end
$commands_xp[322] = Command.new(322, -1, "SETACTORGRAPH", "更改角色图像", target_text, "isisi", target_window, nil, 0, 0)

#=================================================================
# Code 331
# 增减敌人 HP
#-----------------------------------------------------------------
# Parameter : [-1, 0, 1, 18, false]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  ""
end
target_window = Proc.new do |window, commands|
  Builder.In(window) do
  end
  Builder.Out
  window
end
$commands_xp[331] = Command.new(331, -1, "SETENEMYHP", "增减敌人 HP", target_text, "iiiib", target_window, nil, 0, 0)

#=================================================================
# Code 332
# 增减敌人 SP
#-----------------------------------------------------------------
# Parameter : [-1, 1, 1, 1]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  ""
end
target_window = Proc.new do |window, commands|
  Builder.In(window) do
  end
  Builder.Out
  window
end
$commands_xp[332] = Command.new(332, -1, "SETENEMYSP", "增减敌人 SP", target_text, "iiii", target_window, nil, 0, 0)

#=================================================================
# Code 333
# 增减敌人状态
#-----------------------------------------------------------------
# Parameter : [-1, 0, 1]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  ""
end
target_window = Proc.new do |window, commands|
  Builder.In(window) do
  end
  Builder.Out
  window
end
$commands_xp[333] = Command.new(333, -1, "SETENEMYSTATAE", "增减敌人状态", target_text, "iii", target_window, nil, 0, 0)

#=================================================================
# Code 334
# 敌人全体回复
#-----------------------------------------------------------------
# Parameter : [-1]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  ""
end
target_window = Proc.new do |window, commands|
  Builder.In(window) do
  end
  Builder.Out
  window
end
$commands_xp[334] = Command.new(334, -1, "RECOVERENEMY", "敌人全体回复", target_text, "i", target_window, nil, 0, 0)

#=================================================================
# Code 335
# 出现敌人
#-----------------------------------------------------------------
# Parameter : [5]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  ""
end
target_window = Proc.new do |window, commands|
  Builder.In(window) do
  end
  Builder.Out
  window
end
$commands_xp[335] = Command.new(335, -1, "SHOWENEMY", "出现敌人", target_text, "i", target_window, nil, 0, 0)

#=================================================================
# Code 336
# 敌人变身
#-----------------------------------------------------------------
# Parameter : [0, 20]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  ""
end
target_window = Proc.new do |window, commands|
  Builder.In(window) do
  end
  Builder.Out
  window
end
$commands_xp[336] = Command.new(336, -1, "CHANGEENEMY", "敌人变身", target_text, "ii", target_window, nil, 0, 0)

#=================================================================
# Code 337
# 显示动画
#-----------------------------------------------------------------
# Parameter : [1, 1, 20]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  ""
end
target_window = Proc.new do |window, commands|
  Builder.In(window) do
  end
  Builder.Out
  window
end
$commands_xp[337] = Command.new(337, -1, "BATTLEANIMATION", "显示动画", target_text, "iii", target_window, nil, 0, 0)

#=================================================================
# Code 338
# 伤害处理
#-----------------------------------------------------------------
# Parameter : [0, -1, 0, 600]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  ""
end
target_window = Proc.new do |window, commands|
  Builder.In(window) do
  end
  Builder.Out
  window
end
$commands_xp[338] = Command.new(338, -1, "DAMAGE", "伤害处理", target_text, "iiii", target_window, nil, 0, 0)

#=================================================================
# Code 339
# 强制行动
#-----------------------------------------------------------------
# Parameter : [0, 0, 0, 2, -1, 1]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  ""
end
target_window = Proc.new do |window, commands|
  Builder.In(window) do
  end
  Builder.Out
  window
end
$commands_xp[339] = Command.new(339, -1, "FORCEBEHAVE", "强制行动", target_text, "iiiiii", target_window, nil, 0, 0)

#=================================================================
# Code 340
# 战斗中断
#-----------------------------------------------------------------
# Parameter : []
#=================================================================
target_text = Text.ret("战斗中断")
$commands_xp[340] = Command.new(340, -1, "BREAKBATTLE", "战斗中断", target_text, "", nil, nil, 0, 0)

#=================================================================
# Code 351
# 呼叫菜单画面
#-----------------------------------------------------------------
# Parameter : []
#=================================================================
target_text = Text.ret("呼叫菜单画面")
$commands_xp[351] = Command.new(351, -1, "CALLMENU", "呼叫菜单画面", target_text, "", nil, nil, 0, 0)

#=================================================================
# Code 352
# 呼叫存档画面
#-----------------------------------------------------------------
# Parameter : []
#=================================================================
target_text = Text.ret("呼叫存档画面")
$commands_xp[352] = Command.new(352, -1, "CALLSAVE", "呼叫存档画面", target_text, "", nil, nil, 0, 0)

#=================================================================
# Code 353
# 游戏结束
#-----------------------------------------------------------------
# Parameter : []
#=================================================================
target_text = Text.ret("游戏结束")
$commands_xp[353] = Command.new(353, -1, "CALLGAMEOVER", "游戏结束", target_text, "", nil, nil, 0, 0)

#=================================================================
# Code 354
# 返回标题画面
#-----------------------------------------------------------------
# Parameter : []
#=================================================================
target_text = Text.ret("返回标题画面")
$commands_xp[354] = Command.new(354, -1, "CALLTITLE", "返回标题画面", target_text, "", nil, nil, 0, 0)

#=================================================================
# Code 355
# 执行脚本
#-----------------------------------------------------------------
# Parameter : ["这是脚本的第一行"]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  "执行脚本：".encode + "#{parameters[0].Text}"
end
target_window = Proc.new do |window, commands|
  Builder.In(window) do
  end
  Builder.Out
  window
end
target_with = Proc.new do |command|

end
$commands_xp[355] = Command.new(355, -1, "SHELL", "脚本", target_text, "t", target_window, target_with, 0, 0)

#=================================================================
# Code 401
#-----------------------------------------------------------------
# Parameter : ["这是显示文章的第二行"]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  ""
end
$commands_xp[401] = Command.new(401, 101, "_MESSAGE", "继续显示对话", target_text, "f")
#=================================================================
# Code 402
#-----------------------------------------------------------------
# Parameter : [0, "这是选择项的第一项"]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  ""
end
$commands_xp[402] = Command.new(402, 102, "CHOICE", "选择项", target_text, "is")
#=================================================================
# Code 403
#-----------------------------------------------------------------
# Parameter : []
#=================================================================
target_text = Text.ret("取消的场合")
$commands_xp[403] = Command.new(403, 102, "CANCELCHOICE", "取消的场合", target_text, "")
#=================================================================
# Code 404
#-----------------------------------------------------------------
# Parameter : []
#=================================================================
target_text = Text.ret("分歧结束")
$commands_xp[404] = Command.new(404, 102, "ENDCHOOSE", "分歧结束", target_text, "")
#=================================================================
# Code 408
#-----------------------------------------------------------------
# Parameter : ["这是注释的第二行"]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  ""
end
$commands_xp[408] = Command.new(408, 108, "_REM", "继续注释", target_text, "s")
#=================================================================
# Code 412
#-----------------------------------------------------------------
# Parameter : []
#=================================================================
target_text = Text.ret("分歧结束")
$commands_xp[412] = Command.new(412, 112, "ENDIF", "分歧结束", target_text, "")
#=================================================================
# Code 413
#-----------------------------------------------------------------
# Parameter : []
#=================================================================
target_text = Text.ret("以上反复")
$commands_xp[413] = Command.new(413, 113, "ENDLOOP", "以上反复", target_text, "")
#=================================================================
# Code 605
#-----------------------------------------------------------------
# Parameter : [1, 1]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  ""
end
$commands_xp[605] = Command.new(605, 305, "", "NAME", target_text, "ii")
#=================================================================
# Code 655
#-----------------------------------------------------------------
# Parameter : ["这是脚本的第二行"]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  ""
end
$commands_xp[655] = Command.new(655, 355, "_SHELL", "继续脚本", target_text, "s")


$groups_xp = []
group1 = Group.new("显示信息".encode, $commands_xp, (101..110).to_a + (401..410).to_a)
group2 = Group.new("流程控制".encode, $commands_xp, (111..120).to_a + (411..420).to_a)
group3 = Group.new("变量控制".encode, $commands_xp, (121..130).to_a + (421..430).to_a)
group4 = Group.new("外观控制".encode, $commands_xp, (131..140).to_a + (431..440).to_a)
group5 = Group.new("地图事件".encode, $commands_xp, (201..210).to_a + (501..510).to_a)
group6 = Group.new("画面变更".encode, $commands_xp, (221..230).to_a + (521..530).to_a)
group6 = Group.new("图片控制".encode, $commands_xp, (231..240).to_a + (531..540).to_a)
group7 = Group.new("播放音乐".encode, $commands_xp, (241..260).to_a + (541..560).to_a)
group8 = Group.new("界面变换".encode, $commands_xp, (301..310).to_a + (601..610).to_a)
group9 = Group.new("我方战斗".encode, $commands_xp, (301..330).to_a + (601..630).to_a)
group10 = Group.new("敌方战斗".encode, $commands_xp, (331..340).to_a + (631..640).to_a)
group11 = Group.new("流程控制".encode, $commands_xp, (351..360).to_a + (651..660).to_a)
$groups_xp.push group1
$groups_xp.push group2
$groups_xp.push group3
$groups_xp.push group4
$groups_xp.push group5
$groups_xp.push group6
$groups_xp.push group7
$groups_xp.push group8
$groups_xp.push group9
$groups_xp.push group10
$groups_xp.push group11

=begin
=end