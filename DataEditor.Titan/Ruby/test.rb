#encoding:utf-8
# Arce Scrupt : Test.rb
# Run Single Tests.


Path["project"] = "Test/PjVATst"
Path.RequestPath("project","请选择工程文件夹")
Data["actor"] = "Data/Actors.rvdata2"
puts Data["actor"]
Builder.In(Window["Main"])
Builder.In(Builder.Add(:tabs, {}))
	Builder.In(Builder.Add(:tab, {:text => "www"}))
		Builder.Add(:text,{:text => "FUCK"})
		Builder.Add(:int,{:text => "WTF" , :maxvalue => 1000})
		Builder.Add(:float, {:text => "Floats", :digit => 3})
		Builder.In(Builder.Add(:group, {:text => "WOW"}))
			Builder.Add(:text,{:text => "My love"})
			choice = Filechoice.new("actor")
			Builder.Add(:choose, {:text => "choose_test", :choice => {-1 => "HAHAHA", 0 => "CMB", nil => choice}}) 
		Builder.Out
	Builder.Out
Builder.Out