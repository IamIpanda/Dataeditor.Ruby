# This file is in coding: utf-8
# Arce Script: event - xp.rb
# describe the parameters for event
# This file can't run lonely.

require "ruby/Event.rb"
require "ruby/Fuzzy.rb"


$commands_xp = {}
$commands_xp[0] = Command.new(0, -1, "TAB", "空指令", Text.ret(""))
#=================================================================
# Code 101
# 显示文章
#-----------------------------------------------------------------
# Parameter : ["显示文章"]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  parameters[0].Text
end

target_window = Proc.new do |window, commands|
  window = Builder.Add(:dialog_text, {:actual => :INDEX0 })
end
target_with = Proc.new do |window, oldwith|
  $commands_xp[101].SperateText(window.Value, $commands_xp[401])
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
  text = []
  for str in array
    text.push str.Text
  end
  "[".encode + text.join(", ") + "]"
end
target_window = Proc.new do |window, commands|
  Builder.In(window)
    Builder.Order
    Builder.Add(:metro, {:actual => :INDEX0 }) do
      Builder.Add(:text, {:actual => :INDEX0, :text => "选择项 1"})
      Builder.Add(:text, {:actual => :INDEX1, :text => "选择项 2"})
      Builder.Add(:text, {:actual => :INDEX2, :text => "选择项 3"})
      Builder.Add(:text, {:actual => :INDEX3, :text => "选择项 4"})
    end
    Builder.Add(:group, {:text => "取消的场合"}) do
      Builder.Add(:single_radio, {:actual => :INDEX1, :text => "无", :key => 1, :group => "window_code_102"})
      Builder.Add(:single_radio, {:actual => :INDEX1, :text => "选择项 1", :key => 2, :group => "window_code_102"})
      Builder.Add(:single_radio, {:actual => :INDEX1, :text => "选择项 2", :key => 3, :group => "window_code_102"})
      Builder.Add(:single_radio, {:actual => :INDEX1, :text => "选择项 3", :key => 4, :group => "window_code_102"})
      Builder.Add(:single_radio, {:actual => :INDEX1, :text => "选择项 4", :key => 5, :group => "window_code_102"})
      Builder.Add(:single_radio, {:actual => :INDEX1, :text => "分歧", :key => 6, :group => "window_code_102"})
    end
  Builder.Out
  window
end
target_with = Proc.new do |window, oldwith|
  window.Value[0].Clear
  texts = window.SearchChild("metro").SearchChilds("text")
  radios = window.SearchChild("group").SearchChilds("single_radio")
  count = 0
  for i in 1..3
    count = i if texts[i].Binding.Text != "" 
  end
  count = [count, window.Value[1].Value - 1].max
  puts texts[0].Binding.Text
  puts texts[0].Binding.Text.GetType.ToString
  puts FuzzyString.new(texts[i].Binding.Text)
  for i in 0..count
    window.Value[0].Add(FuzzyString.new(texts[i].Binding.Text))
  end
  ans = []
  for i in 0..count
    para = $commands_xp[402].GetStruct
    para["@parameters"][0].Value = i
    para["@parameters"][1].Text = FuzzyString.new(texts[i].Binding.Text)
    ans.push para
    ans.push $commands_xp[0].GetStruct
  end
  if (radios[5].Binding.Checked)
    ans.push $commands_xp[403].GetStruct
    ans.push $commands_xp[0].GetStruct
  end
  ans.push $commands_xp[404].GetStruct
  ans
end
$commands_xp[102] = Command.new(102, -1, "CHOOSE", "显示选择项", target_text, "ai", target_window, target_with, 0, 0)

#=================================================================
# Code 103
# 处理数字输入
#-----------------------------------------------------------------
# Parameter : [3, 3]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  Event_Help.variable(parameters[0].Value) + " , ".encode + "#{parameters[1].Value}" + " 位".encode
end
target_window = Proc.new do |window, commands|
  Builder.In(window)
    Builder.Add(:variable , {:actual => :INDEX0 , :text => "接受数值的变量", :data => Data["system"]["@variables"] })
    Builder.Add(:int , {:actual => :INDEX1 , :text => "位数" })
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
  "#{choice0[arg0].encode}, #{choice1[arg1].encode}"
end
target_window = Proc.new do |window, commands|
  Builder.In(window)
    Builder.Order
    Builder.Add(:group, {:text => "显示位置"}) do
      Builder.Add(:radio, {:actual => :INDEX0, :text => "上", :key => 0, :group => "window_code_104_1"})
      Builder.Add(:radio, {:actual => :INDEX0, :text => "中", :key => 0, :group => "window_code_104_1"})
      Builder.Add(:radio, {:actual => :INDEX0, :text => "下", :key => 0, :group => "window_code_104_1"})
    end
    Builder.Add(:group, {:text => "窗口显示"}) do
      Builder.Add(:radio, {:actual => :INDEX1, :text => "显示", :key => 0, :group => "window_code_104_2"})
      Builder.Add(:radio, {:actual => :INDEX1, :text => "不显示", :key => 0, :group => "window_code_104_2"})
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
  Event_Help.variable(parameters[0].Value)
end
target_window = Proc.new do |window, commands|
  Builder.In(window)
    Builder.Add(:variable , {:actual => :INDEX0 , :text => "接受按键编号的变量", :data => Data["system"]["@variables"]})
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
  " #{parameters[0].Value} 帧".encode
end
target_window = Proc.new do |window, commands|
  Builder.In(window)
    Builder.Add(:int , {:actual => :INDEX0 , :text => "时间"})
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
  "#{parameters[0].Text}"
end
target_window = Proc.new do |window, commands|
  Builder.In(window)
  Builder.Out
  window
end
target_with = Proc.new do |window, oldwith|

end
$commands_xp[108] = Command.new(108, -1, "REM", "注释", target_text, "t", target_window, target_with, 0, 0)

#=================================================================
# Code 111
# 条件分歧
# 变长参数
#-----------------------------------------------------------------
# Parameter : [0, 1, 0]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  arg0 = parameters[0].Value
  case arg0
  when 0
    "开关 ".encode + Event_Help.switch(parameters[1].Value) + " == " + Event_Help.switch_state(parameters[2].Value)
  when 1
    "变量 ".encode + Event_Help.variable(parameters[1].Value) + Event_Help.compare(parameters[4].Value) + Event_Help.variable_or_value(parameters[2].Value, parameters[3].Value)
  when  2
    "独立开关 ".encode + parameters[1].Text + " == " + Event_Help.switch_state(parameters[2].Value)
  when 3
    sec = parameters[1].Value
    min = sec / 60
    sec -= min * 60
    "计时器 ".encode + min.to_s + " 分 ".encode + sec.to_s + " 秒 ".encode + ["以上","以下"][parameters[2].Value].encode
  when 4
    arg1 = parameters[2].Value
    case arg1
    when 0 
      action = " 在同伴中".encode
    when 1
      action = " 名称为 ".encode + parameters[3].Text
    when 2
      action = " 已经学会技能 ".encode + Event_Help.value(parameters[3].Value, Data["skill"])
    when 3
      action = " 装备了武器 ".encode + Event_Help.value(parameters[3].Value, Data["weapon"])
    when 4
      action = " 装备了防具 ".encode + Event_Help.value(parameters[3].Value, Data["armor"])
    when 5
      action = " 具有状态 ".encode + Event_Help.value(parameters[3].Value, Data["state"])
    end
    "角色 ".encode + Event_Help.actor(parameters[1].Value) + action
  when 5
    arg1 = parameters[2].Value
    case arg1
    when 0
      action = "出现".encode
    when 1
      action = Event_Help.value(parameters[3].Value, Data["state"])
    end
    " 敌人 ".encode + Event_Help.enemy(parameters[1].Value) + action
  when 6
    action = Event_Help.event(parameters[1].Value) + " 为  朝向 ".encode + Event_Help.direction(parameters[2].Value) 
  when 7
    "金钱 #{parameters[1].Value} ".encode + (parameters[2].Value == 0 ? "以上" : "以下").encode
  when 8
    item = Event_Help.value(parameters[1].Value, Data["item"])
    "持有 ".encode + item 
  when 9
    item = Event_Help.value(parameters[1].Value, Data["weapon"])
    "持有 ".encode + item 
  when 10
    item = Event_Help.value(parameters[1].Value, Data["armor"])
    "持有 ".encode + item
  when 11
    "按钮 ".encode + Event_Help.press(parameters[1].Value) + " 被按下时".encode
  when 12
    "脚本 ".encode + parameters[1].Text
  end
end
target_window = Proc.new do |window, commands| 
  $commands_xp[111].ResetUniform
  Builder.In(window)
    Builder.Add(:tabs) do
      Builder.Add(:tab , { :text => "1" }) do
        accept_0 = Proc.new { |value, parent, radio_key| $commands_xp[111].ReUniform(parent, "ii") }
        Builder.Add(:radio, {:actual => :INDEX0, :text => "开关", :key => 0, :group => "window_code_111", :accept => accept_0}) do
          Builder.Order
          Builder.Add(:switch, {:actual => :INDEX1, :label => 0, :data => Data["system"]["@switches"]})
          Builder.Text("值")
          Builder.Add(:choose, {:actual => :INDEX2, :label => 0, :choice => { 0 => "ON", 1 => "OFF"}})
        end
        accept_1 = Proc.new { |value, parent, radio_key| $commands_xp[111].ReUniform(parent, "iiii") }
        Builder.Add(:radio, {:actual => :INDEX0, :text => "变量", :key => 1, :group => "window_code_111", :accept => accept_1}) do
          Builder.Order
          Builder.Add(:variable, {:actual => :INDEX1, :label => 0, :data => Data["system"]["@variables"]})
          Builder.Text("值")
          Builder.Next
          Builder.Add(:radio, {:actual => :INDEX2, :text => "常量", :key => 0, :group => "window_code_111#2"}) do
            Builder.Add(:int, {:actual => :INDEX3, :label => 0})
          end
          Builder.Next
          Builder.Add(:radio, {:actual => :INDEX2, :text => "变量", :key => 1, :group => "window_code_111#2"}) do
            Builder.Add(:variable, {:actual => :INDEX3, :data => Data["system"]["@variables"], :label => 0})
          end
          Builder.Next
          Builder.Add(:choose, {:actual => :INDEX4, :label => 0, :choice => {
            0 => "相等",
            1 => "以上",
            2 => "以下",
            3 => "超过",
            4 => "未满",
            5 => "以外"
            }})
        end
        accept_2 = Proc.new { |value, parent, radio_key| $commands_xp[111].ReUniform(parent, "ii") }
        Builder.Add(:radio, {:actual => :INDEX0, :text => "独立开关", :key => 2, :group => "window_code_111", :accept => accept_2}) do
          Builder.Order
          Builder.Add(:self_switch, {:actual => :INDEX1, :label => 0})
          Builder.Text("值")
          Builder.Add(:choose, {:actual => :INDEX2, :label => 0, :choice => { 0 => "ON", 1 => "OFF" }})
        end
        accept_3 = Proc.new { |value, parent, radio_key| $commands_xp[111].ReUniform(parent, "ii") }
        Builder.Add(:radio, {:actual => :INDEX0, :text => "计时器", :key => 3, :group => "window_code_111", :accept => accept_3}) do
          Builder.Order
          Builder.Add(:timer, {:actual => :INDEX1, :label => 0})
          Builder.Add(:choose, {:actual => :INDEX2, :label => 0, :choice => { 0 => "以上", 1 => "以下" }})
        end
      end
      Builder.Add(:tab , { :text => "2" }) do
        accept_4 = Proc.new { |value, parent, radio_key| $commands_xp[111].ReUniform(parent, "iu") }
        deny_4 = Proc.new{ |value, parent, radio_key| $commands_xp[111].PopUniform }
        Builder.Add(:radio, {:actual => :INDEX0, :text => "角色", :key => 4, :group => "window_code_111", :accept => accept_4, :deny => deny_4}) do
          Builder.Add(:choose, {:actual => :INDEX1, :label => 0, :choice => { nil => Filechoice.new("actor") }})
          accept_40 = Proc.new { |value, parent, radio_key| $commands_xp[111].ReUniform(parent, "") }
          accept_41 = Proc.new { |value, parent, radio_key| $commands_xp[111].ReUniform(parent, "s") }
          accept_42 = Proc.new { |value, parent, radio_key| $commands_xp[111].ReUniform(parent, "i") }
          Builder.Add(:radio, {:actual => :INDEX1, :text => "在同伴中", :key => 0, :group => "window_code_111#3", :accept => accept_40})
          Builder.Add(:radio, {:actual => :INDEX1, :text => "姓名", :key => 1, :group => "window_code_111#3", :accept => accept_41}) do
            Builder.Add(:text, {:actual => :INDEX2, :label => 0})
          end
          Builder.Add(:radio, {:actual => :INDEX1, :text => "特技", :key => 2, :group => "window_code_111#3", :accept => accept_42}) do
            Builder.Order
            Builder.Add(:choose, {:actual => :INDEX2, :label => 0, :choice => { nil => Filechoice.new("skill") }})
            Builder.Text("已经学会")
          end
          Builder.Add(:radio, {:actual => :INDEX1, :text => "武器", :key => 3, :group => "window_code_111#3", :accept => accept_42}) do
            Builder.Order
            Builder.Add(:choose, {:actual => :INDEX2, :label => 0, :choice => { nil => Filechoice.new("weapon") }})
            Builder.Text("装备中")
          end
          Builder.Add(:radio, {:actual => :INDEX1, :text => "防具", :key => 4, :group => "window_code_111#3", :accept => accept_42}) do
            Builder.Order
            Builder.Add(:choose, {:actual => :INDEX2, :label => 0, :choice => { nil => Filechoice.new("armor") }})
            Builder.Text("装备中")
          end
          Builder.Add(:radio, {:actual => :INDEX1, :text => "状态", :key => 5, :group => "window_code_111#3", :accept => accept_42}) do
            Builder.Order
            Builder.Add(:choose, {:actual => :INDEX2, :label => 0, :choice => { nil => Filechoice.new("state") }})
            Builder.Text("与其相同")
          end
        end
      end
      Builder.Add(:tab , { :text => "3" }) do
        accept_5 = Proc.new { |value, parent, radio_key| $commands_xp[111].ReUniform(parent, "ii") }
        Builder.Add(:radio, {:actual => :INDEX0, :text => "敌人", :key => 5, :group => "window_code_111", :accept => accept_5}) do
          Builder.Pop(:no_troop_enemy, 1)
          Builder.Add(:radio, {:actual => :INDEX1, :text => "出现", :key => 0, :group => "window_code_111#4"})
          Builder.Add(:radio, {:actual => :INDEX1, :text => "拥有状态", :key => 1, :group => "window_code_111#4"}) do
            Builder.Add(:choose, {:actual => :INDEX2, :label => 0, :choice => { nil => Filechoice.new("state") }})
          end
        end
        accept_6 = Proc.new { |value, parent, radio_key| $commands_xp[111].ReUniform(parent, "ii") }
        Builder.Add(:radio, {:actual => :INDEX0, :text => "角色", :key => 6, :group => "window_code_111", :accept => accept_6}) do
          Builder.Order
          Builder.Pop(:event, 0)
          Builder.Text("面向")
          Builder.Next
          Builder.Add(:choose, {:actual => :INDEX1, :label => 0, :choice => { 2 => "下", 4 => "左", 6 => "右", 8 => "上" }})
        end
      end
      Builder.Add(:tab , { :text => "4" }) do
        accept_7 = Proc.new { |value, parent, radio_key| $commands_xp[111].ReUniform(parent, "ii") }
        Builder.Add(:radio, {:actual => :INDEX0, :text => "金钱", :key => 7, :group => "window_code_111", :accept => accept_7}) do
          Builder.Order
          Builder.Add(:int, {:actual => :INDEX1, :label => 0 })
          Builder.Add(:choose, {:actual => :INDEX2, :label => 0, :choice => { 0 => "以上", 1 => "以下" }})
        end
        accept_8 = Proc.new { |value, parent, radio_key| $commands_xp[111].ReUniform(parent, "i") }
        Builder.Add(:radio, {:actual => :INDEX0, :text => "物品", :key => 8, :group => "window_code_111", :accept => accept_8}) do
          Builder.Order
          Builder.Add(:choose, {:actual => :INDEX1, :label => 0, :choice => { nil => Filechoice.new("item") }})
          Builder.Text("携带时")
        end
        accept_9 = Proc.new { |value, parent, radio_key| $commands_xp[111].ReUniform(parent, "i") }
        Builder.Add(:radio, {:actual => :INDEX0, :text => "武器", :key => 9, :group => "window_code_111", :accept => accept_9}) do
          Builder.Order
          Builder.Add(:choose, {:actual => :INDEX1, :label => 0, :choice => { nil => Filechoice.new("weapon") }})
          Builder.Text("携带时")
        end
        accept_10 = Proc.new { |value, parent, radio_key| $commands_xp[111].ReUniform(parent, "i") }
        Builder.Add(:radio, {:actual => :INDEX0, :text => "防具", :key => 10, :group => "window_code_111", :accept => accept_10}) do
          Builder.Order
          Builder.Add(:choose, {:actual => :INDEX1, :label => 0, :choice => { nil => Filechoice.new("armor") }})
          Builder.Text("携带时")
        end
        accept_11 = Proc.new { |value, parent, radio_key| $commands_xp[111].ReUniform(parent, "i") }
        Builder.Add(:radio, {:actual => :INDEX0, :text => "按钮", :key => 11, :group => "window_code_111", :accept => accept_11}) do
          Builder.Order
          Builder.Add(:choose, {:actual => :INDEX1, :label => 0, :choice => {
            2 => "下",
            4 => "左",
            6 => "右",
            8 => "上",
            11 => "A",
            12 => "B",
            13 => "C",
            14 => "X",
            15 => "Y",
            16 => "Z",
            17 => "L",
            18 => "R"
            }})
          Builder.Text("被按下时")
        end
        accept_12 = Proc.new { |value, parent, radio_key| $commands_xp[111].ReUniform(parent, "s") }
        Builder.Add(:radio, {:actual => :INDEX0, :text => "脚本", :key => 12, :group => "window_code_111", :accept => accept_12}) do
          Builder.Add(:text, {:actual => :INDEX1, :label => 0})
        end
      end

    end
  Builder.Out
  window
end
$commands_xp[111] = Command.new(111, -1, "IF", "条件分歧", target_text, "iu", target_window, nil, 0, 0)

#=================================================================
# Code 112
# 循环
#-----------------------------------------------------------------
# Parameter : []
#=================================================================
target_text = Text.ret("")
$commands_xp[112] = Command.new(112, -1, "DO", "循环", target_text, "", nil, nil, 0, 0)

#=================================================================
# Code 113
# 中断循环
#-----------------------------------------------------------------
# Parameter : []
#=================================================================
target_text = Text.ret("")
$commands_xp[113] = Command.new(113, -1, "LOOP", "中断循环", target_text, "", nil, nil, 0, 0)

#=================================================================
# Code 115
# 中断事件处理
#-----------------------------------------------------------------
# Parameter : []
#=================================================================
target_text = Text.ret("")
$commands_xp[115] = Command.new(115, -1, "BREAK", "中断事件处理", target_text, "", nil, nil, 0, 0)

#=================================================================
# Code 116
# 暂时消除事件
#-----------------------------------------------------------------
# Parameter : []
#=================================================================
target_text = Text.ret("")
$commands_xp[116] = Command.new(116, -1, "YIELD", "暂时消除事件", target_text, "", nil, nil, 0, 0)

#=================================================================
# Code 117
# 公共事件
#-----------------------------------------------------------------
# Parameter : [1]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  Event_Help.value(parameters[0].Value, Data["commonevent"])
end
target_window = Proc.new do |window, commands|
  window = Builder.Add(:dialog_r) do
    Builder.Add(:choose , {:actual => :INDEX0 , :text => "公共事件", :choice => { nil => Filechoice.new("commonevent") } })
  end
end
$commands_xp[117] = Command.new(117, -1, "COMMONEVENT", "公共事件", target_text, "i", target_window, nil, 0, 0)

#=================================================================
# Code 118
# 标签
#-----------------------------------------------------------------
# Parameter : ["这是标签"]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  "[#{parameters[0].Text}]"
end
target_window = Proc.new do |window, commands|
  window = Builder.Add(:dialog_r) do
    Builder.Add(:text , {:actual => :INDEX0 , :text => "标签名" })
  end
end
$commands_xp[118] = Command.new(118, -1, "LABEL", "标签", target_text, "s", target_window, nil, 0, 0)

#=================================================================
# Code 119
# 跳转标签
#-----------------------------------------------------------------
# Parameter : ["这是跳转标签"]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  "[#{parameters[0].Text}]"
end
target_window = Proc.new do |window, commands|
  window = Builder.Add(:dialog_r) do
    Builder.Add(:text , {:actual => :INDEX0 , :text => "标签名" })
  end
end
$commands_xp[119] = Command.new(119, -1, "JMP", "跳转标签", target_text, "s", target_window, nil, 0, 0)

#=================================================================
# Code 121
# 开关操作
#-----------------------------------------------------------------
# Parameter : [10, 20, 0]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  Event_Help.switches(parameters[0].Value, parameters[1].Value) + " = " + Event_Help.switch_state(parameters[2].Value)
end
target_window = Proc.new do |window, commands|
  Builder.In(window)
    Builder.Pop(:group_switch_2, 0)
    Builder.Add(:group, {:text => "操作"}) do
      Builder.Add(:single_radio, {:actual => :INDEX1, :text => "ON", :key => 0, :group => "window_code_121"})
      Builder.Add(:single_radio, {:actual => :INDEX1, :text => "OFF", :key => 0, :group => "window_code_121"})
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
   part0 = Event_Help.variables(parameters[0].Value, parameters[1].Value)
   part1 = [" = "," += "," -= "," *= "," /= "," %= "][parameters[2].Value]
   arg0 = parameters[3].Value
   case arg0
   when 0
      part2 = parameters[4].Value.to_s
    when 1
      part2 = Event_Help.variable(parameters[4].Value)
    when 2
      part2 = "随机数 ( ".encode + parameters[4].Value.to_s + ".." + parameters[5].Value.to_s + " )"
    when 3
      part2 = "物品 ".encode + Event_Help.value(parameters[4].Value, Data["item"]) + " 的所持".encode
    when 4
      part2 = "角色 ".encode + Event_Help.actor(parameters[4].Value) + " 的 ".encode + ["等级","EXP","HP","SP","MaxHP","MaxSP","力量","灵巧","速度","魔力","攻击","物理防御","魔法防御","回避修正"][parameters[5].Value].encode
    when 5
      part2 = "敌人 ".encode + Event_Help.enemy(parameters[4].Value) + " 的 ".encode + ["等级","EXP","HP","SP","MaxHP","MaxSP","力量","灵巧","速度","魔力","攻击","物理防御","魔法防御","回避修正"][parameters[5].Value].encode
    when 6
      part2 = "角色 ".encode + Event_Help.event(parameters[4].Value) + " 的 ".encode + ["X坐标","Y坐标","朝向","画面X坐标","画面Y坐标","地形标志"][parameters[5].Value].encode
    when 7
      part2 = ["地图 ID","同伴人数","金钱","步数","游戏时间","计时器","存档次数"][parameters[4].Value].encode
   end
   part0 + part1 + part2
end
target_window = Proc.new do |window, commands|
  Builder.In(window)
    Builder.Pop(:group_variable_2, 0)
      Builder.Add(:group, {:text => "操作"}) do
        Builder.Order
        Builder.Add(:single_radio, {:actual => :INDEX2, :text => "代入", :key => 0, :group => "window_code_122"})
        Builder.Add(:single_radio, {:actual => :INDEX2, :text => "加法", :key => 1, :group => "window_code_122"})
        Builder.Add(:single_radio, {:actual => :INDEX2, :text => "减法", :key => 2, :group => "window_code_122"})
        Builder.Add(:single_radio, {:actual => :INDEX2, :text => "乘法", :key => 3, :group => "window_code_122"})
        Builder.Add(:single_radio, {:actual => :INDEX2, :text => "除法", :key => 4, :group => "window_code_122"})
        Builder.Add(:single_radio, {:actual => :INDEX2, :text => "剩余", :key => 5, :group => "window_code_122"})
      end
      Builder.Add(:group, {:text => "操作数"}) do
        accept_1 = Proc.new { |value, parent, radio_key | $commands_xp[122].ReUniform(parent, "i") }
        accept_2 = Proc.new { |value, parent, radio_key | $commands_xp[122].ReUniform(parent, "ii") }
        Builder.Add(:radio, {:actual => :INDEX3, :text => "常量", :key => 0, :group => "window_code_122#2", :accept => accept_1}) do
          Builder.Add(:int, {:actual => :INDEX4, :label => 0})
        end
        Builder.Add(:radio, {:actual => :INDEX3, :text => "变量", :key => 1, :group => "window_code_122#2", :accept => accept_1}) do
          Builder.Add(:variable, {:actual => :INDEX4, :label => 0, :data => Data["system"]["@variables"]})
        end
        Builder.Add(:radio, {:actual => :INDEX3, :text => "随机数", :key => 2, :group => "window_code_122#2", :accept => accept_2}) do
          Builder.Order
          Builder.Add(:int, {:actual => :INDEX4, :label => 0})
          Builder.Text("~")
          Builder.Add(:int, {:actual => :INDEX5, :label => 0})
        end
        Builder.Add(:radio, {:actual => :INDEX3, :text => "物品", :key => 3, :group => "window_code_122#2", :accept => accept_1}) do
          Builder.Order
          Builder.Add(:choose, {:actual => :INDEX4, :label => 0, :choice => { nil => Filechoice.new("item")}})
          Builder.Text("的所持")
        end
        Builder.Add(:radio, {:actual => :INDEX3, :text => "角色", :key => 4, :group => "window_code_122#2", :accept => accept_2}) do
          Builder.Order
          Builder.Add(:choose, {:actual => :INDEX4, :label => 0, :choice => { nil => Filechoice.new("actor")}})
          Builder.Text("的")
          Builder.Add(:choose, {:actual => :INDEX5, :label => 0, :choice => {
            0 => "等级",
            1 => "EXP",
            2 => "HP",
            3 => "SP",
            4 => "MaxHP",
            5 => "MaxSP",
            6 => "力量",
            7 => "灵巧",
            8 => "速度",
            9 => "魔力",
            10 => "攻击",
            11 => "物理防御",
            12 => "魔法防御",
            13 => "回避修正"
            }})
        end
        Builder.Add(:radio, {:actual => :INDEX3, :text => "敌人", :key => 5, :group => "window_code_122#2", :accept => accept_2}) do
          Builder.Order
          Builder.Pop(:no_troop_enemy, 4)
          Builder.Text("的")
          Builder.Add(:choose, {:actual => :INDEX5, :label => 0, :choice => {
            0 => "等级",
            1 => "EXP",
            2 => "HP",
            3 => "SP",
            4 => "MaxHP",
            5 => "MaxSP",
            6 => "力量",
            7 => "灵巧",
            8 => "速度",
            9 => "魔力",
            10 => "攻击",
            11 => "物理防御",
            12 => "魔法防御",
            13 => "回避修正"
            }})
        end
        Builder.Add(:radio, {:actual => :INDEX3, :text => "角色", :key => 6, :group => "window_code_122#2", :accept => accept_2}) do
          Builder.Order
          Builder.Text("的")
          Builder.Add(:choose, {:actual => :INDEX5, :label => 0, :choice => {
            0 => "X 坐标",
            1 => "Y 坐标",
            2 => "朝向",
            3 => "画面 X 坐标",
            4 => "画面 Y 坐标",
            5 => "地形标志"
            }})
        end
        Builder.Add(:radio, {:actual => :INDEX3, :text => "其他", :key => 7, :group => "window_code_122#2", :accept => accept_1}) do
          Builder.Add(:choose, {:actual => :INDEX4, :label => 0, :choice => {
            0 => "地图 ID",
            1 => "同伴人数",
            2 => "金钱",
            3 => "步数",
            4 => "游戏时间",
            5 => "计时器",
            6 => "存档次数"
            }})
        end
      end
  Builder.Out
  window
end
$commands_xp[122] = Command.new(122, -1, "VARIABLE", "变量操作", target_text, "iiiiu", target_window, nil, 0, 0)

#=================================================================
# Code 123
# 独立开关操作
#-----------------------------------------------------------------
# Parameter : ["C", 0]
#=================================================================
target_text = Text.new do |parameters, *followings|
  parameters[0].Text + " = " + (parameters[1].Value == 0 ? "ON" : "OFF")
end
target_window = Proc.new do |window, commands|
  window = Builder.Add(:dialog_r) do
    Builder.Add(:self_switch, {:actual => :INDEX0, :text => "独立开关"})
    Builder.Add(:group, {:text => "操作"}) do
      Builder.Order
      Builder.Add(:single_radio, {:actual => :INDEX1, :text => "ON", :key => 0, :group => "window_code_123"})
      Builder.Add(:single_radio, {:actual => :INDEX1, :text => "OFF", :key => 1, :group => "window_code_123"})
    end
  end
end
$commands_xp[123] = Command.new(123, -1, "SINGLESWITCH", "独立开关", target_text, "si", target_window, nil, 0, 0)

#=================================================================
# Code 124
# 定时器操作
#-----------------------------------------------------------------
# Parameter : [0, 920]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  action = parameters[0].Value
  if (action > 0)
    "停止".encode
  else
    sec = parameters[1].Value
    min = sec / 60
    sec -= min * 60
    "开始 （".encode + min.to_s + " 分 ".encode + sec.to_s + " 秒）".encode
  end
end
target_window = Proc.new do |window, commands|
  window = Builder.Add(:dialog_r) do
    Builder.Add(:group, {:text => "操作"}) do
      Builder.Add(:single_radio, {:actual => :INDEX0, :text => "开始", :key => 0, :group => "window_code_124"})
      Builder.Add(:single_radio, {:actual => :INDEX0, :text => "停止", :key => 1, :group => "window_code_124"})
    end
    Builder.Add(:timer, {:actual => :INDEX1, :label => 0})
  end
end
$commands_xp[124] = Command.new(124, -1, "TIMER", "定时器操作", target_text, "ii", target_window, nil, 0, 0)

#=================================================================
# Code 125
# 增减金钱
#-----------------------------------------------------------------
# Parameter : [0, 0, 666]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  aos = parameters[0].Value
  value1 = parameters[1].Value
  value2 = parameters[2].Value
  Event_Help.add_or_sub(aos) + Event_Help.variable_or_value(value1, value2)
end
target_window = Proc.new do |window, commands|
  Builder.In(window)
    
    Builder.Pop(:operate, 0)
    Builder.Pop(:variable_or_value, 1)
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
  itemid = parameters[0].Value
  aos = parameters[1].Value
  value1 = parameters[2].Value
  value2 = parameters[3].Value
    Event_Help.value(itemid, Data["item"]) + 
    Event_Help.add_or_sub(aos) + 
    Event_Help.variable_or_value(value1, value2)
end
target_window = Proc.new do |window, commands|
  Builder.In(window)
    Builder.Add(:choose, {:actual => :INDEX0, :text => "物品", :choice => { nil => Filechoice.new(Data["item"]) } })
    Builder.Pop(:operate, 1)
    Builder.Pop(:variable_or_value, 2)
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
  itemid = parameters[0].Value
  aos = parameters[1].Value
  value1 = parameters[2].Value
  value2 = parameters[3].Value
    Event_Help.value(itemid, Data["weapon"]) + 
    Event_Help.add_or_sub(aos) + 
    Event_Help.variable_or_value(value1, value2)
end
target_window = Proc.new do |window, commands|
  Builder.In(window)
    Builder.Add(:choose, {:actual => :INDEX0, :text => "武器", :choice => { nil => Filechoice.new(Data["weapon"])}})
    Builder.Pop(:operate, 1)
    Builder.Pop(:variable_or_value, 2)
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
  itemid = parameters[0].Value
  aos = parameters[1].Value
  value1 = parameters[2].Value
  value2 = parameters[3].Value
    Event_Help.value(itemid, Data["armor"]) + 
    Event_Help.add_or_sub(aos) + 
    Event_Help.variable_or_value(value1, value2)
end
target_window = Proc.new do |window, commands|
  Builder.In(window)
    Builder.Add(:choose, {:actual => :INDEX0, :text => "防具", :choice => { nil => Filechoice.new(Data["armor"])}})
    Builder.Pop(:operate, 1)
    Builder.Pop(:variable_or_value, 2)
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
  actorid = parameters[0].Value
  aos = parameters[1].Value
  init = parameters[2].Value
  Event_Help.value(actorid, Data["actor"]) + 
  (aos == 0 ? " 加入" : " 离开").encode +
  (init == 0 ? ", 初始化" : "").encode
end
target_window = Proc.new do |window, commands|
  window = Builder.Add(:dialog) do
    Builder.Add(:choose, {:actual => :INDEX0, :text => "角色", :choice => { nil => Filechoice.new("actor")}})
    Builder.Pop(:operate, 1)
    Builder.Add(:int_check, {:actual => :INDEX2, :text => "初始化"})
  end
end
$commands_xp[129] = Command.new(129, -1, "ACTOR", "替换队员", target_text, "iii", target_window, nil, 0, 0)

#=================================================================
# Code 131
# 更改窗口外观
#-----------------------------------------------------------------
# Parameter : ["001-Blue01"]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  "\'" + parameters[0].Text + "\'"
end
target_window = Proc.new do |window, commands|
  Builder.In(window)
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
  Event_Help.audio(parameters[0])
end
target_window = Proc.new do |window, commands|
  Builder.In(window)
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
  Event_Help.audio(parameters[0])
end
target_window = Proc.new do |window, commands|
  Builder.In(window)
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
   Event_Help.allow_or_ban(parameters[0].Value)
end
target_window = Proc.new do |window, commands|
  window = Builder.Add(:dialog_r) do
    Builder.Pop(:permission, 0)
  end
end
$commands_xp[134] = Command.new(134, -1, "SAVEENABLED", "更改禁止存档", target_text, "i", target_window, nil, 0, 0)

#=================================================================
# Code 135
# 更改禁止菜单
#-----------------------------------------------------------------
# Parameter : [1]
#=================================================================
target_text = Text.new do |parameters, *followings| 
   Event_Help.allow_or_ban(parameters[0].Value)
end
target_window = Proc.new do |window, commands|
  window = Builder.Add(:dialog_r) do
    Builder.Pop(:permission, 0)
  end
end
$commands_xp[135] = Command.new(135, -1, "MENUENABLED", "更改禁止菜单", target_text, "i", target_window, nil, 0, 0)

#=================================================================
# Code 136
# 更改禁止遇敌
#-----------------------------------------------------------------
# Parameter : [1]
#=================================================================
target_text = Text.new do |parameters, *followings| 
   Event_Help.allow_or_ban(parameters[0].Value)
end
target_window = Proc.new do |window, commands|
  window = Builder.Add(:dialog_r) do
    Builder.Pop(:permission, 0)
  end
end
$commands_xp[136] = Command.new(136, -1, "ENEMYENABLED", "更改禁止遇敌", target_text, "i", target_window, nil, 0, 0)

#=================================================================
# Code 201
# 场所移动
#-----------------------------------------------------------------
# Parameter : [1, 14, 15, 16, 4, 0]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  part1 = Event_Help.pos(parameters[0].Value,parameters[1].Value,parameters[2].Value,parameters[3].Value)
  part2 = Event_Help.direction(parameters[4].Value)
  part3 = (parameters[5].Value > 0) ? ", 不淡出" : ""
  part1 + ", " + part2 + part3
end
target_window = Proc.new do |window, commands|
  Builder.In(window)
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
  Builder.In(window)
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
  "#{Event_Help.direction(parameters[0].Value)}, #{parameters[1].Value}, #{parameters[2].Value}"
end
target_window = Proc.new do |window, commands|
  window = Builder.Add(:dialog_r) do
    Builder.Order
    Builder.Pop(:force_direction, 0)
    Builder.Add(:int, {:actual => :INDEX1, :text => "距离"})
    Builder.Pop(:speed, 2)
  end
end
$commands_xp[203] = Command.new(203, -1, "SCROLL", "画面卷动", target_text, "iii", target_window, nil, 0, 0)

#=================================================================
# Code 204
# 更改地图设置
#-----------------------------------------------------------------
# Parameter : [0, "004-CloudySky01", 0]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  type = parameters[0].Value
  prefix = ["远景","雾","战斗背景"][type].encode
  prefix += " = \'#{parameters[1].Text}\'"
  part = ""
  case type
  when 0
    part = ", #{parameters[2].Value}"
  when 1
    part = ", #{parameters[2].Value}, #{parameters[3].Value}, " + ["普通","加法","减法"][parameters[4].Value].encode + ", #{parameters[5].Value}, #{parameters[6].Value}, #{parameters[7].Value}"
  end
  prefix + part
end
target_window = Proc.new do |window, commands|
  Builder.In(window)
  Builder.Out
  window
end
$commands_xp[204] = Command.new(204, -1, "MAPSET", "更改地图设置", target_text, "is", target_window, nil, 0, 0)

#=================================================================
# Code 205
# 更改雾色调
#-----------------------------------------------------------------
# Parameter : [(88.000000, 88.000000, 88.000000, 88.000000), 88]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  Event_Help.tone(parameters[0]) + ", @#{parameters[1].Value}"
end
target_window = Proc.new do |window, commands|
  Builder.In(window)
    Builder.Order
    Builder.Add(:tone, {:actual => :INDEX0, :label => 0})
    Builder.Next
    Builder.Add(:int, {:actual => :INDEX1, :text => "时间", :label => 2})
    Builder.Text("帧")
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
  "#{parameters[0].Value}, #{parameters[1].Value}"
end
target_window = Proc.new do |window, commands|
  window = Builder.Add(:dialog_r) do
    Builder.Add(:int, {:actual => :INDEX0, :text => "不透明度"})
    Builder.Add(:int, {:actual => :INDEX1, :text => "时间"})
  end
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
  Builder.In(window)
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
  parameters[0].Value == 0 ? "透明".encode : "不透明".encode
end
target_window = Proc.new do |window, commands|
  Builder.In(window)
    Builder.Add(:group, {:text => "透明状态"}) do
      Builder.Add(:single_radio, {:actual => :INDEX0, :text => "透明", :key => 0, :group => "window_code_208" })
      Builder.Add(:single_radio, {:actual => :INDEX0, :text => "普通", :key => 1, :group => "window_code_208" })
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
target_text = Text.ret("")
$commands_xp[210] = Command.new(210, -1, "WAITMOVE", "等待移动结束", target_text, "", nil, nil, 0, 0)

#=================================================================
# Code 221
# 准备渐变
#-----------------------------------------------------------------
# Parameter : []
#=================================================================
target_text = Text.ret("")
$commands_xp[221] = Command.new(221, -1, "PREPARESEGUE", "准备渐变", target_text, "", nil, nil, 0, 0)

#=================================================================
# Code 222
# 执行渐变
#-----------------------------------------------------------------
# Parameter : ["009-Random01"]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  "\"#{parameters[0].Text}\""
end
target_window = Proc.new do |window, commands|
  window = Builder.Add(:dialog_image, {:actual => {:name => :INDEX0}, :path => "Graphics/Transitions"})
end
$commands_xp[222] = Command.new(222, -1, "SEGUE", "执行渐变", target_text, "s", target_window, nil, 0, 0)

#=================================================================
# Code 223
# 更改画面色调
#-----------------------------------------------------------------
# Parameter : [(111.000000, 112.000000, 113.000000, 114.000000), 20]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  Event_Help.tone(parameters[0]) + ", @#{parameters[1].Value}"
end
target_window = Proc.new do |window, commands|
  Builder.In(window)
    Builder.Order
    Builder.Add(:tone, {:actual => :INDEX0, :label => 0})
    Builder.Next
    Builder.Add(:int, {:actual => :INDEX1, :text => "时间", :label => 2})
    Builder.Text("帧")
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
  Event_Help.color(parameters[0]) + ", @#{parameters[1].Value}"
end
target_window = Proc.new do |window, commands|
  Builder.In(window)
    Builder.Order
    Builder.Add(:color, {:actual => :INDEX0, :label => 0})
    Builder.Next
    Builder.Add(:int, {:actual => :INDEX1, :text => "时间", :label => 2})
    Builder.Text("帧")
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
  "#{parameters[0].Value}, #{parameters[1].Value}, @#{parameters[2].Value}"
end
target_window = Proc.new do |window, commands|
  Builder.In(window)
    Builder.Add(:scrollint, {:actual => :INDEX0, :text => "强度", :label => 2, :maxvalue => 9})
    Builder.Add(:scrollint, {:actual => :INDEX1, :text => "速度", :label => 2, :maxvalue => 9})
    Builder.Add(:int, {:actual => :INDEX2, :text => "时间"})
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
  pos = ["左上","中心"][parameters[2].Value].encode + ", "
  pos += Event_Help.variable_or_values(parameters[3].Value, parameters[4].Value, parameters[5].Value)
  mix = ["普通","加法","减法"][parameters[9].Value].encode
   "#{parameters[0].Value}, #{parameters[1].Text}, #{pos}, (#{parameters[6].Value}\%, #{parameters[7].Value}\%), #{parameters[8].Value}, #{mix} "
end
target_window = Proc.new do |window, commands|
  Builder.In(window)
    Builder.Order
    Builder.Add(:int, {:actual => :INDEX0, :text => "编号"})
    Builder.Add(:image , {:actual => {:name => :INDEX1 } ,
        :text => "图片图形", :path => "Graphics/Pictures", :show => Help::XP_IMAGE_SPLIT, :split => Help::XP_IMAGE_SPLIT })
    Builder.Next
    Builder.Add(:group, {:text => "显示位置"}) do
      Builder.Add(:group, {:text => "原点"}) do
        Builder.Order
        Builder.Add(:single_radio, {:actual => :INDEX2, :text => "左上", :key => 0, :group => "window_code_231" })
        Builder.Add(:single_radio, {:actual => :INDEX2, :text => "中心", :key => 1, :group => "window_code_231" })
      end
      Builder.Add(:radio, {:actual => :INDEX3, :text => "直接指定", :key => 0, :group => "window_code_231#2"}) do
        Builder.Add(:int, {:actual => :INDEX4, :text => "X: ", :label => 2})
        Builder.Add(:int, {:actual => :INDEX5, :text => "Y: ", :label => 2})
      end
      Builder.Add(:radio, {:actual => :INDEX3, :text => "使用变量指定", :key => 1, :group => "window_code_231#2"}) do
        Builder.Add(:variable, {:actual => :INDEX4, :text => "X: ", :label => 2, :data => Data["system"]["@variables"]})
        Builder.Add(:variable, {:actual => :INDEX5, :text => "Y: ", :label => 2, :data => Data["system"]["@variables"]})
      end
    end
    Builder.Next
    Builder.Add(:group, {:text => "放大率"}) do
      Builder.Add(:int, {:actual => :INDEX6, :text => "X", :label => 2})
      Builder.Add(:int, {:actual => :INDEX7, :text => "Y", :label => 2})
    end
    Builder.Add(:int, {:actual => :INDEX8, :text => "不透明度"})
    Builder.Add(:choose, {:actual => :INDEX9, :text => "合成方式",:choice => {0 => "普通", 1 => "加法", 2 => "减法"}})
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
  pos = ["左上","中心"][parameters[2].Value].encode
  pos += Event_Help.variable_or_values(parameters[3].Value, parameters[4].Value, parameters[5].Value)
  mix = ["普通","加法","减法"][parameters[9].Value].encode
   "#{parameters[0].Value}, @#{parameters[1].Value}, #{pos}, (#{parameters[6].Value}\%, #{parameters[7].Value}\%), #{parameters[8].Value}, #{mix} "
end
target_window = Proc.new do |window, commands|
  Builder.In(window)    
  Builder.Order
    Builder.Add(:int, {:actual => :INDEX0, :text => "编号"})
    Builder.Add(:int, {:actual => :INDEX1, :text => "时间"})
    Builder.Text("帧")
    Builder.Next
    Builder.Add(:group, {:text => "显示位置"}) do
      Builder.Add(:group, {:text => "原点"}) do
        Builder.Order
        Builder.Add(:single_radio, {:actual => :INDEX2, :text => "左上", :key => 0, :group => "window_code_231" })
        Builder.Add(:single_radio, {:actual => :INDEX2, :text => "中心", :key => 1, :group => "window_code_231" })
      end
      Builder.Add(:radio, {:actual => :INDEX3, :text => "直接指定", :key => 0, :group => "window_code_231#2"}) do
        Builder.Add(:int, {:actual => :INDEX4, :text => "X: ", :label => 2})
        Builder.Add(:int, {:actual => :INDEX5, :text => "Y: ", :label => 2})
      end
      Builder.Add(:radio, {:actual => :INDEX3, :text => "使用变量指定", :key => 1, :group => "window_code_231#2"}) do
        Builder.Add(:variable, {:actual => :INDEX4, :text => "X: ", :label => 2, :data => Data["system"]["@variables"]})
        Builder.Add(:variable, {:actual => :INDEX5, :text => "Y: ", :label => 2, :data => Data["system"]["@variables"]})
      end
    end
    Builder.Next
    Builder.Add(:group, {:text => "放大率"}) do
      Builder.Add(:int, {:actual => :INDEX6, :text => "X", :label => 2})
      Builder.Add(:int, {:actual => :INDEX7, :text => "Y", :label => 2})
    end
    Builder.Add(:int, {:actual => :INDEX8, :text => "不透明度"})
    Builder.Add(:choose, {:actual => :INDEX9, :text => "合成方式",:choice => {0 => "普通", 1 => "加法", 2 => "减法"}})
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
  "#{parameters[0].Value}, #{parameters[1].Value}"
end
target_window = Proc.new do |window, commands|
  window = Builder.Add(:dialog_r) do
    Builder.Add(:int, {:actual => :INDEX0, :text => "编号"})
    Builder.Add(:int, {:actual => :INDEX1, :text => "旋转速度"})
  end
end
$commands_xp[233] = Command.new(233, -1, "233", "旋转图片", target_text, "ii", target_window, nil, 0, 0)

#=================================================================
# Code 234
# 更改图片色调
#-----------------------------------------------------------------
# Parameter : [4, (130.000000, 131.000000, 132.000000, 133.000000), 20]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  "#{parameters[0].Value}, #{Event_Help.tone(parameters[1])}, @#{parameters[2].Value}"
end
target_window = Proc.new do |window, commands|
  Builder.In(window)
    Builder.Order
    Builder.Add(:int, {:actual => :INDEX0, :text => "编号"})
    Builder.Next
    Builder.Add(:tone, {:actual => :INDEX0, :label => 0})
    Builder.Next
    Builder.Add(:int, {:actual => :INDEX1, :text => "时间", :label => 2})
    Builder.Text("帧")
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
  parameters[0].Value.to_s
end
target_window = Proc.new do |window, commands|
  window = Builder.Add(:dialog_r) do
    Builder.Add(:int, {:actual => :INDEX0, :text => "编号"})
  end
end
$commands_xp[235] = Command.new(235, -1, "DISAPPEARPIC", "图片消失", target_text, "i", target_window, nil, 0, 0)

#=================================================================
# Code 236
# 天气设置
#-----------------------------------------------------------------
# Parameter : [1, 6, 140]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  type = ["无","雨","风","雪"][parameters[0].Value].encode
  "#{type}, #{parameters[1].Value}, #{parameters[2].Value}"
end
target_window = Proc.new do |window, commands|
  Builder.In(window)
    Builder.Add(:group, { :text => "天候"}) do
      Builder.Order
      Builder.Add(:single_radio, {:actual => :INDEX0, :text => "无", :key => 0, :group => "window_code_236"})
      Builder.Add(:single_radio, {:actual => :INDEX0, :text => "风", :key => 1, :group => "window_code_236"})
      Builder.Add(:single_radio, {:actual => :INDEX0, :text => "雨", :key => 2, :group => "window_code_236"})
      Builder.Add(:single_radio, {:actual => :INDEX0, :text => "雪", :key => 3, :group => "window_code_236"})
    end
    Builder.Add(:scrollint, {:actual => :INDEX1, :text => "强度", :label => 2, :maxvalue => 9})
    Builder.Add(:int, {:actual => :INDEX2, :text => "时间", :label => 2})
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
  Event_Help.audio(parameters[0])
end
target_window = Proc.new do |window, commands|
  window = Builder.Add(:dialog_audio, {:actual => :INDEX0, :type => "BGM"})
end
$commands_xp[241] = Command.new(241, -1, "PLAYBGM", "演奏 BGM", target_text, "d", target_window, nil, 0, 0)

#=================================================================
# Code 242
# BGM 的淡入淡出
#-----------------------------------------------------------------
# Parameter : [4]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  "#{parameters[0].Value}" + " 秒".encode
end
target_window = Proc.new do |window, commands|
  window = Builder.Add(:dialog_r) do
    Builder.Order
    Builder.Add(:int, {:actual => :INDEX0, :text => "时间" })
    Builder.Text("秒")
  end
end
$commands_xp[242] = Command.new(242, -1, "FADEBGM", "BGM 的淡入淡出", target_text, "i", target_window, nil, 0, 0)

#=================================================================
# Code 245
# 演奏 BGS
#-----------------------------------------------------------------
# Parameter : [#<RPG::AudioFile:0x17d27e0 @volume=80, @name="013-Fire01", @pitch=100>]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  Event_Help.audio(parameters[0])
end
target_window = Proc.new do |window, commands|
  window = Builder.Add(:dialog_audio, {:actual => :INDEX0, :type => "BGS"})
end
$commands_xp[245] = Command.new(245, -1, "PLAYBGS", "演奏 BGS", target_text, "d", target_window, nil, 0, 0)

#=================================================================
# Code 246
# BGS 的淡入淡出
#-----------------------------------------------------------------
# Parameter : [2]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  parameters[0].Value.to_s + " 秒".encode
end
target_window = Proc.new do |window, commands|
  window = Builder.Add(:dialog_r) do
    Builder.Order
    Builder.Add(:int, {:actual => :INDEX0, :text => "时间" })
    Builder.Text("秒")
  end
end
$commands_xp[246] = Command.new(246, -1, "FADEBGS", "BGS 的淡入淡出", target_text, "i", target_window, nil, 0, 0)

#=================================================================
# Code 247
# 记忆 BGM/BGS
#-----------------------------------------------------------------
# Parameter : []
#=================================================================
target_text = Text.ret("")
$commands_xp[247] = Command.new(247, -1, "REMEMBERBGM", "记忆 BGM/BGS", target_text, "", nil, nil, 0, 0)

#=================================================================
# Code 248
# 还原 BGM/BGS
#-----------------------------------------------------------------
# Parameter : []
#=================================================================
target_text = Text.ret("")
$commands_xp[248] = Command.new(248, -1, "RECOVERBGM", "还原 BGM/BGS", target_text, "", nil, nil, 0, 0)

#=================================================================
# Code 249
# 演奏 ME
#-----------------------------------------------------------------
# Parameter : [#<RPG::AudioFile:0x17d2600 @volume=100, @name="009-Fanfare03", @pitch=100>]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  Event_Help.audio(parameters[0])
end
target_window = Proc.new do |window, commands|
  window = Builder.Add(:dialog_audio, {:actual => :INDEX0, :type => "ME"})
end
$commands_xp[249] = Command.new(249, -1, "PLAYME", "演奏 ME", target_text, "d", target_window, nil, 0, 0)

#=================================================================
# Code 250
# 演奏 SE
#-----------------------------------------------------------------
# Parameter : [#<RPG::AudioFile:0x17d2540 @volume=80, @name="015-Jump01", @pitch=100>]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  Event_Help.audio(parameters[0])
end
target_window = Proc.new do |window, commands|
  window = Builder.Add(:dialog_audio, {:actual => :INDEX0, :type => "SE"})
end
$commands_xp[250] = Command.new(250, -1, "PLAYSE", "演奏 SE", target_text, "d", target_window, nil, 0, 0)

#=================================================================
# Code 251
# 停止 SE
#-----------------------------------------------------------------
# Parameter : []
#=================================================================
target_text = Text.ret("")
$commands_xp[251] = Command.new(251, -1, "STOPSE", "停止 SE", target_text, "", nil, nil, 0, 0)


#=================================================================
# Code 301
# 战斗处理
#-----------------------------------------------------------------
# Parameter : [22, false, false]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  Event_Help.value(parameters[0].Value, Data["troop"])
end
target_window = Proc.new do |window, commands|
  window = Builder.Add(:dialog_r) do
    Builder.Add(:choose, {:actual => :INDEX0, :text => "敌队伍", :choice => { nil => Filechoice.new("troop") }})
    Builder.Add(:check, {:actual => :INDEX1, :text => "可以逃跑" })
    Builder.Add(:check, {:actual => :INDEX2, :text => "失败的话继续"})
  end
end
$commands_xp[301] = Command.new(301, -1, "BATTLE", "战斗处理", target_text, "ibb", target_window, nil, 0, 0)

#=================================================================
# Code 302
# 商店处理
#-----------------------------------------------------------------
# Parameter : [0, 1]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  Event_Help.shop(parameters[0].Value,parameters[1].Value)
end
target_window = Proc.new do |window, commands|
  Builder.In(window)
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
  Event_Help.value(parameters[0].Value, Data["actor"]) + ", " + parameters[1].Value.to_s + " 文字".encode
end
target_window = Proc.new do |window, commands|
  window = Builder.Add(:dialog_r) do
    Builder.Add(:choose, {:actual => :INDEX0, :text => "角色", :choice => { nil => Filechoice.new("actor") }})
    Builder.Add(:int, {:actual => :INDEX1, :text => "最大文字数"})
  end
end
$commands_xp[303] = Command.new(303, -1, "INPUTNAME", "名称输入处理", target_text, "ii", target_window, nil, 0, 0)

#=================================================================
# Code 311
# 增减 HP
#-----------------------------------------------------------------
# Parameter : [0, 1, 0, 400, true]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  part0 = Event_Help.actor(parameters[0].Value)
  part1 = parameters[1].Value > 0 ? " - " : " + "
  part2 = Event_Help.variable_or_value(parameters[2].Value,parameters[3].Value)
  part0 + "," + part1 + part2
end
target_window = Proc.new do |window, commands|
  Builder.In(window)
    Builder.Pop(:actor, 0)
    Builder.Pop(:operate, 1)
    Builder.Pop(:variable_or_value, 2)
    Builder.Add(:check, {:actual => :INDEX4, :text => "允许死亡"})
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
  part0 = Event_Help.actor(parameters[0].Value)
  part1 = parameters[1].Value > 0 ? " - " : " + "
  part2 = Event_Help.variable_or_value(parameters[2].Value,parameters[3].Value)
  part0 + ", " + part1 + part2
end
target_window = Proc.new do |window, commands|
  Builder.In(window)
    Builder.Pop(:actor, 0)
    Builder.Pop(:operate, 1)
    Builder.Pop(:variable_or_value, 2)
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
  part0 = Event_Help.actor(parameters[0].Value)
  part1 = parameters[1].Value > 0 ? " - " : " + "
  part2 = Event_Help.value(parameters[2].Value, Data["state"])
  part0 + "," + part1 + part2
end
target_window = Proc.new do |window, commands|
  Builder.In(window)
    Builder.Pop(:actor, 0)
    Builder.Pop(:operate, 1)
    Builder.Add(:choose, {:actual => :INDEX2, :text => "状态", :choice => { nil => Filechoice.new("state") }})
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
  Event_Help.actor(parameters[0].Value)
end
target_window = Proc.new do |window, commands|
  window = Builder.Add(:dialog_r) do
    Builder.Pop(:actor, 0)
  end
end
$commands_xp[314] = Command.new(314, -1, "RECOVERPLAYER", "完全回复", target_text, "i", target_window, nil, 0, 0)

#=================================================================
# Code 315
# 增减 EXP
#-----------------------------------------------------------------
# Parameter : [5, 0, 0, 250]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  part0 = Event_Help.actor(parameters[0].Value)
  part1 = Event_Help.add_or_sub(parameters[1].Value)
  part2 = Event_Help.variable_or_value(parameters[2].Value, parameters[3].Value)
  part0 + ", " + part1 + part2
end
target_window = Proc.new do |window, commands|
  Builder.In(window)
    Builder.Pop(:actor, 0)
    Builder.Pop(:operate, 1)
    Builder.Pop(:variable_or_value, 2)
  Builder.Pop
end
$commands_xp[315] = Command.new(315, -1, "SETCOMPANIES", "增减同伴", target_text, "iiii", target_window, nil, 0, 0)
#=================================================================
# Code 316
# 增减等级
#-----------------------------------------------------------------
# Parameter : [0, 1, 0, 56]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  part0 = Event_Help.actor(parameters[0].Value)
  part1 = Event_Help.add_or_sub(parameters[1].Value)
  part2 = Event_Help.variable_or_value(parameters[2].Value, parameters[3].Value)
  part0 + ", " + part1 + part2
end
target_window = Proc.new do |window, commands|
  Builder.In(window)
    Builder.Pop(:actor, 0)
    Builder.Pop(:operate, 1)
    Builder.Pop(:variable_or_value, 2)
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
  part0 = Event_Help.actor(parameters[0].Value)
  part1 = ["MaxHP","MaxSP","力量","灵巧","速度","魔力"][parameters[1].Value].encode
  part2 = Event_Help.add_or_sub(parameters[2].Value)
  part3 = Event_Help.variable_or_value(parameters[3].Value, parameters[4].Value)
  part0 + ", " + part1 + part2 + part3
end
target_window = Proc.new do |window, commands|
  Builder.In(window)
    Builder.Pop(:actor, 0)
    Builder.Add(:choose, {:actual => :INDEX1, :text => "能力值", :choice => {
      0 => "MaxHP",
      1 => "MaxSP",
      2 => "力量",
      3 => "灵巧",
      4 => "速度",
      5 => "魔力"
      }})
    Builder.Pop(:operate, 2)
    Builder.Pop(:variable_or_value, 3)
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
  part0 = Event_Help.actor(parameters[0].Value)
  part1 = Event_Help.add_or_sub(parameters[1].Value)
  part2 = Event_Help.value(parameters[2], Data["skill"])
  part0 + part1 + part2
end
target_window = Proc.new do |window, commands|
  Builder.In(window)
    Builder.Add(:choose, {:actual => :INDEX0, :text => "角色", :choice => { nil => Filechoice.new("actor") }})
    Builder.Add(:group, {:text => "操作"}) do
      Builder.Order
      Builder.Add(:single_radio, {:actual => :INDEX1, :text => "领悟", :key => 0, :group => "window_code_318" })
      Builder.Add(:single_radio, {:actual => :INDEX1, :text => "遗忘", :key => 1, :group => "window_code_318" })
    end
    Builder.Add(:choose, {:actual => :INDEX2, :text => "特技", :choice => { nil => Filechoice.new("skill") }})
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
  type = parameters[1].Value
  type_str = ["武器","盾","头部","身体","装饰品"][type].encode
  part0 = Event_Help.actor(parameters[0].Value)
  part1 = Event_Help.value(parameters[2].Value, (type == 0 ? Data["weapon"] : Data["armor"]))
  part0 + ", " + type_str + " = " + part1
end
target_window = Proc.new do |window, commands|
  Builder.In(window)

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
  part0 = Event_Help.actor(parameters[0].Value)
  "#{part0}, \'#{parameters[1].Text}\'"
end
target_window = Proc.new do |window, commands|
  window = Builder.Add(:dialog_r) do
    Builder.Add(:choose, {:actual => :INDEX0, :text => "角色", :choice => { nil => Filechoice.new("actor") }})
    Builder.Add(:text, {:actual => :INDEX1, :text => "姓名"})
  end
end
$commands_xp[320] = Command.new(320, -1, "SETACTORNAME", "更改角色姓名", target_text, "is", target_window, nil, 0, 0)

#=================================================================
# Code 321
# 更改角色职业
#-----------------------------------------------------------------
# Parameter : [1, 7]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  "#{Event_Help.actor(parameters[0].Value)}, #{Event_Help.value(parameters[1].Value, Data["class"])}"
end
target_window = Proc.new do |window, commands|
  window = Builder.Add(:dialog_r) do
    Builder.Add(:choose, {:actual => :INDEX0, :text => "角色", :choice => { nil => Filechoice.new("actor") }})
    Builder.Add(:choose, {:actual => :INDEX1, :text => "职业", :choice => { nil => Filechoice.new("class") }})
  end
end
$commands_xp[321] = Command.new(321, -1, "SETACTORCLASS", "更改角色职业", target_text, "ii", target_window, nil, 0, 0)

#=================================================================
# Code 322
# 更改角色图像
#-----------------------------------------------------------------
# Parameter : [1, "025-Cleric01", 200, "021-Hunter02", 290]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  "#{Event_Help.actor(parameters[0].Value)}, #{parameters[1].Text}, #{parameters[2].Value}, #{parameters[3].Text}, #{parameters[4].Value}"
end
target_window = Proc.new do |window, commands|
  window = Builder.Add(:dialog_r) do
    Builder.Add(:choose, {:actual => :INDEX0, :text => "角色", :choice => { nil => Filechoice.new("actor") }})
    Builder.Add(:oldimage, {:actual => {:name => :INDEX1, :hue => :INDEX2}, :text => "角色脸谱"})
    Builder.Add(:oldimage, {:actual => {:name => :INDEX3, :hue => :INDEX4}, :text => "战斗图", :path => "battlers"})
  end
end
$commands_xp[322] = Command.new(322, -1, "SETACTORGRAPH", "更改角色图像", target_text, "isisi", target_window, nil, 0, 0)

#=================================================================
# Code 331
# 增减敌人 HP
#-----------------------------------------------------------------
# Parameter : [-1, 0, 1, 18, false]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  part0 = Event_Help.enemy(parameters[0].Value)
  part1 = Event_Help.add_or_sub(parameters[1].Value)
  part2 = Event_Help.variable_or_value(parameters[2].Value,parameters[3].Value)
  part0 + "," + part1 + part2
end
target_window = Proc.new do |window, commands|
  Builder.In(window)
    Builder.Pop(:enemy, 0)
    Builder.Pop(:operate, 1)
    Builder.Pop(:variable_or_value, 2)
    Builder.Add(:check, {:actual => :INDEX4, :text => "允许死亡"})  
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
  part0 = Event_Help.enemy(parameters[0].Value)
  part1 = parameters[1].Value > 0 ? " - " : " + "
  part2 = Event_Help.variable_or_value(parameters[2].Value,parameters[3].Value)
  part0 + ", " + part1 + part2
end
target_window = Proc.new do |window, commands|
  Builder.In(window)
    Builder.Pop(:enemy, 0)
    Builder.Pop(:operate, 1)
    Builder.Pop(:variable_or_value, 2)
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
  part0 = Event_Help.enemy(parameters[0].Value)
  part1 = parameters[1].Value > 0 ? " - " : " + "
  part2 = Event_Help.value(parameters[2].Value, Data["state"])
  part0 + "," + part1 + part2
end
target_window = Proc.new do |window, commands|
  Builder.In(window)
    Builder.Pop(:enemy, 0)
    Builder.Pop(:operate, 1)
    Builder.Add(:choose, {:actual => :INDEX2, :text => "状态", :choice => { nil => Filechoice.new("state") }})
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
  Event_Help.enemy(parameters[0].Value)
end
target_window = Proc.new do |window, commands|
  window = Builder.Add(:dialog_r) do
    Builder.Pop(:enemy, 0)
  end
end
$commands_xp[334] = Command.new(334, -1, "RECOVERENEMY", "敌人全体回复", target_text, "i", target_window, nil, 0, 0)

#=================================================================
# Code 335
# 出现敌人
#-----------------------------------------------------------------
# Parameter : [5]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  Event_Help.enemy(parameters[0].Value)
end
target_window = Proc.new do |window, commands|
  window = Builder.Add(:dialog_r) do
    Builder.Pop(:no_troop_enemy, 0)
  end
end
$commands_xp[335] = Command.new(335, -1, "SHOWENEMY", "出现敌人", target_text, "i", target_window, nil, 0, 0)

#=================================================================
# Code 336
# 敌人变身
#-----------------------------------------------------------------
# Parameter : [0, 20]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  Event_Help.enemy(parameters[0].Value) + ", " + Event_Help.value(parameters[1].Value,Data["enemy"])
end
target_window = Proc.new do |window, commands|
  window = Builder.Add(:dialog_r) do
    Builder.Pop(:no_troop_enemy, 0)
    Builder.Add(:choose, {:actual => :INDEX1, :text => "变身", :choice => { nil => Filechoice.new("enemy") }})
  end
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
  Builder.In(window)
    Builder.Add(:group, {:text => "目标"}) do
      Builder.Add(:radio, {:actual => :INDEX0, :text => "敌人", :key => 0, :group => "window_code_337"}) do
        Builder.Pop(:enemy, 1)
      end
      Builder.Add(:radio, {:actual => :INDEX0, :text => "角色", :key => 1, :group => "window_code_337"}) do
        Builder.Pop(:unknown_actor, 1)
      end
    end  
    Builder.Add(:choose, {:actual => :INDEX2, :text => "动画", :choice => { nil => Filechoice.new("animation") }})
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
  Builder.In(window)      
    Builder.Add(:group, {:text => "目标"}) do
      Builder.Add(:radio, {:actual => :INDEX0, :text => "敌人", :key => 0, :group => "window_code_337"}) do
        Builder.Pop(:enemy, 1)
      end
      Builder.Add(:radio, {:actual => :INDEX0, :text => "角色", :key => 1, :group => "window_code_337"}) do
        Builder.Pop(:unknown_actor, 1)
      end
    end
    Builder.Pop(:variable_or_value, 2)
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
  Builder.In(window)
    Builder.Add(:group, {:text => "目标"}) do
      Builder.Add(:radio, {:actual => :INDEX0, :text => "敌人", :key => 0, :group => "window_code_337"}) do
        Builder.Pop(:no_troop_enemy, 1)
      end
      Builder.Add(:radio, {:actual => :INDEX0, :text => "角色", :key => 1, :group => "window_code_337"}) do
        Builder.Pop(:unknown_no_troop_actor, 1)
      end
    end
    Builder.Add(:group, {:text => "行为"}) do
      Builder.Add(:radio, {:actual => :INDEX2, :text => "基本", :key => 0, :group => "window_code_337#2"}) do
        Builder.Add(:choose, {:actual => :INDEX3, :label => 0, :choice => { 0 => "攻击", 1 => "防御", 2 => "逃跑", 3 => "什么也不做" }})
      end
      Builder.Add(:radio, {:actual => :INDEX2, :text => "特技", :key => 1, :group => "window_code_337#2"}) do
        Builder.Add(:choose, {:actual => :INDEX3, :label => 0, :choice => { nil => Filechoice.new("skill") }})
      end
      Builder.Add(:choose, {:actual => :INDEX4, :text => "行为对象", :label => 2, :choice => {
        -2 => "最后的目标",
        -1 => "随机",
        0 => "Index 1",
        1 => "Index 2",
        2 => "Index 3",
        3 => "Index 4",
        4 => "Index 5",
        5 => "Index 6",
        6 => "Index 7",
        7 => "Index 8"
        }})
    end
    Builder.Add(:group, {:text => "顺序"}) do
      Builder.Add(:single_radio, {:actual => :INDEX5, :text => "按照平时的执行顺序执行", :key => 0, :group => "window_code_337#3"})
      Builder.Add(:single_radio, {:actual => :INDEX5, :text => "立即执行", :key => 1, :group => "window_code_337#3"})
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
target_text = Text.ret("")
$commands_xp[340] = Command.new(340, -1, "BREAKBATTLE", "战斗中断", target_text, "", nil, nil, 0, 0)

#=================================================================
# Code 351
# 呼叫菜单画面
#-----------------------------------------------------------------
# Parameter : []
#=================================================================
target_text = Text.ret("")
$commands_xp[351] = Command.new(351, -1, "CALLMENU", "呼叫菜单画面", target_text, "", nil, nil, 0, 0)

#=================================================================
# Code 352
# 呼叫存档画面
#-----------------------------------------------------------------
# Parameter : []
#=================================================================
target_text = Text.ret("")
$commands_xp[352] = Command.new(352, -1, "CALLSAVE", "呼叫存档画面", target_text, "", nil, nil, 0, 0)

#=================================================================
# Code 353
# 游戏结束
#-----------------------------------------------------------------
# Parameter : []
#=================================================================
target_text = Text.ret("")
$commands_xp[353] = Command.new(353, -1, "CALLGAMEOVER", "游戏结束", target_text, "", nil, nil, 0, 0)

#=================================================================
# Code 354
# 返回标题画面
#-----------------------------------------------------------------
# Parameter : []
#=================================================================
target_text = Text.ret("")
$commands_xp[354] = Command.new(354, -1, "CALLTITLE", "返回标题画面", target_text, "", nil, nil, 0, 0)

#=================================================================
# Code 355
# 执行脚本
#-----------------------------------------------------------------
# Parameter : ["这是脚本的第一行"]
#=================================================================
target_text = Text.new do |parameters, *followings| 
   parameters[0].Text
end
target_window = Proc.new do |window, commands|
  window = Builder.Add(:dialog_text, {:actual => :INDEX0})
end
target_with = Proc.new do |window, oldwith|

end
$commands_xp[355] = Command.new(355, -1, "SHELL", "脚本", target_text, "t", target_window, target_with, 0, 0)

#=================================================================
# Code 401
#-----------------------------------------------------------------
# Parameter : ["这是显示文章的第二行"]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  parameters[0].Text
end
$commands_xp[401] = Command.new(401, 101, "_MESSAGE", "继续显示对话", target_text, "f", nil, nil, 0, 0)
#=================================================================
# Code 402
#-----------------------------------------------------------------
# Parameter : [0, "这是选择项的第一项"]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  "[ " + parameters[1].Text + " ] 的场合".encode
end
$commands_xp[402] = Command.new(402, 102, "CHOICE", "选择项", target_text, "is", nil, nil, -1, 1)
#=================================================================
# Code 403
#-----------------------------------------------------------------
# Parameter : []
#=================================================================
target_text = Text.ret("取消的场合")
$commands_xp[403] = Command.new(403, 102, "CANCELCHOICE", "取消的场合", target_text, "", nil, nil, -1, 1)
#=================================================================
# Code 404
#-----------------------------------------------------------------
# Parameter : []
#=================================================================
target_text = Text.ret("分歧结束")
$commands_xp[404] = Command.new(404, 102, "ENDCHOOSE", "分歧结束", target_text, "", nil, nil, -1, 0)
#=================================================================
# Code 408
#-----------------------------------------------------------------
# Parameter : ["这是注释的第二行"]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  parameters[0].Text
end
$commands_xp[408] = Command.new(408, 108, "_REM", "继续注释", target_text, "s", nil, nil, 0, 0)
#=================================================================
# Code 411
#-----------------------------------------------------------------
# Parameter : []
#=================================================================
target_text = Text.ret("除此以外的场合")
$commands_xp[411] = Command.new(411, 111, "ELSE", "除此以外的场合", target_text, "", nil, nil, -1, 1)
#=================================================================
# Code 412
#-----------------------------------------------------------------
# Parameter : []
#=================================================================
target_text = Text.ret("分歧结束")
$commands_xp[412] = Command.new(412, 111, "ENDIF", "分歧结束", target_text, "", nil, nil, -1, 0)
#=================================================================
# Code 413
#-----------------------------------------------------------------
# Parameter : []
#=================================================================
target_text = Text.ret("以上反复")
$commands_xp[413] = Command.new(413, 112, "ENDLOOP", "以上反复", target_text, "", nil, nil, -1, 0)
#=================================================================
# Code 601
#-----------------------------------------------------------------
# Parameter : []
#=================================================================
target_text = Text.ret("胜利的场合")
$commands_xp[601] = Command.new(601, 301, "ONVICTORY", "胜利的场合", target_text, "")
#=================================================================
# Code 602
#-----------------------------------------------------------------
# Parameter : []
#=================================================================
target_text = Text.ret("失败的场合")
$commands_xp[602] = Command.new(602, 301, "ONLOSE", "失败的场合", target_text, "")
#=================================================================
# Code 603
#-----------------------------------------------------------------
# Parameter : []
#=================================================================
target_text = Text.ret("逃跑的场合")
$commands_xp[603] = Command.new(603, 301, "ONESCAPE", "逃跑的场合", target_text, "")
#=================================================================
# Code 604
#-----------------------------------------------------------------
# Parameter : []
#=================================================================
target_text = Text.ret("分歧结束")
$commands_xp[604] = Command.new(604, 301, "ENDBATTLE", "分歧结束", target_text, "", nil, nil, -1, 0)
#=================================================================
# Code 605
#-----------------------------------------------------------------
# Parameter : [1, 1]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  Event_Help.shop(parameters[0].Value,parameters[1].Value)
end
$commands_xp[605] = Command.new(605, 302, "", "商店物品", target_text, "ii")
#=================================================================
# Code 655
#-----------------------------------------------------------------
# Parameter : ["这是脚本的第二行"]
#=================================================================
target_text = Text.new do |parameters, *followings| 
  parameters[0].Text
end
$commands_xp[655] = Command.new(655, 355, "_SHELL", "继续脚本", target_text, "s", nil, nil, 0, 0)


$groups_xp = []
group1 = Group.new("显示信息".encode, $commands_xp, (101..107).to_a + (401..407).to_a)
group2 = Group.new("流程控制".encode, $commands_xp, (111..120).to_a + (411..420).to_a)
group3 = Group.new("变量控制".encode, $commands_xp, (121..130).to_a + (421..430).to_a)
group4 = Group.new("外观控制".encode, $commands_xp, (131..140).to_a + (431..440).to_a)
group5 = Group.new("地图事件".encode, $commands_xp, (201..210).to_a + (501..510).to_a)
group6 = Group.new("画面变更".encode, $commands_xp, (221..230).to_a + (521..530).to_a)
group7 = Group.new("图片控制".encode, $commands_xp, (231..240).to_a + (531..540).to_a)
group8 = Group.new("播放音乐".encode, $commands_xp, (241..260).to_a + (541..560).to_a)
group9 = Group.new("界面变换".encode, $commands_xp, (301..310).to_a + (601..610).to_a)
group10 = Group.new("我方战斗".encode, $commands_xp, (311..330).to_a + (611..630).to_a)
group11 = Group.new("敌方战斗".encode, $commands_xp, (331..340).to_a + (631..640).to_a)
group12 = Group.new("流程控制".encode, $commands_xp, (351..360).to_a + (651..660).to_a)
group13 = Group.new("注释".encode, $commands_xp, [108, 408])
group2.SetColor(0x0000FF)
group3.SetColor(0xFF0000)
group4.SetColor(0xFF00FF)
group5.SetColor(0x800000)
group6.SetColor(0x808000)
group7.SetColor(0x800080)
group8.SetColor(0x008080)
group9.SetColor(0xFF8C00)
group10.SetColor(0x1E90FF)
group11.SetColor(0x9400D3)
group12.SetColor(0x808080)
group13.SetColor(0x008000)
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
$groups_xp.push group12
$groups_xp.push group13
=begin
=end