# Lead.rb
# Lead the user to run.

def LoadFile(file)
	# Start Builder
	Window["main"].Start
	# Check the Script
	# get file name
	rb_file_name = file_name + ".rb"
	if FileInfo.Exist(rb_file_name)
		require rb_file_name
	else 
		require "default_file.rb"
	end
end

def LoadProject(type)
	require type + " - project.rb"
end

load_file    = Proc.new {|file| LoadFile(file) }
load_project = Proc.new {|type| LoadProject(type) } 

Window["Lead"] = Window_Lead.new(load_file, load_project)
Window["Lead"].ShowDialog()