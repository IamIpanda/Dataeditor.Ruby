# Actor - va.rb
# Descrobe the Actor page in RPG Maker VA

Path.RequestPath("project","请选择工程文件夹")
Builder.Add(:list , { :text => "角色" ,:textbook => Help.Get_Default_Text}) do 
	Builder.Order
	Builder.Add(:text , {:actual => :name , :text => "角色" })
	Builder.Add(:choose , {
		:actual => :classes , 
		:text => "职业" ,
		:choices => { nil => Filechoice.new("class") }
		})
end