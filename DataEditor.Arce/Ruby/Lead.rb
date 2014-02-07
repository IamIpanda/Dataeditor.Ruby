#encoding:utf-8
# Lead.rb
# Lead the user to run.

class Lead
	class << self
		def search_ruby_file(path)
			dir = File.dirname(path)
			file = File.basename(path)
			# 首先查找目标文件夹下有无指定文件
			file_path = File.join(dir, file + ".rb")
			if (File.exist?(file_path))
				#------------------------
				# start main window
				#------------------------
				Builder.In(Window["Main"])
				Builder.Add(:tabs, {})
				#------------------------
				require file_path
				return true
			end
			ext = File.extname(path)
			type = { "rxdata" => "XP", "rvdata" => "VX", "rvdata2" => "VA" }[ext]
			type = "" if (type == nil) 
			file_path = File.join("Ruby", (type == "" ? "" : (type + "/")) + file + ".rb")
			if (File.exist?(file_path))
				#------------------------
				# start main window
				#------------------------
				Builder.In(Window["Main"])
				Builder.Add(:tabs, {})
				#------------------------
				require file_path
				return true
			end
			return false
		end
		def open_file(path)
			dir = File.dirname(path)
			file = File.basename(path)
			Path["project"] = dir
			return search_ruby_file(file)
		end
		def open_project(path)

			dir = File.dirname(path)
			file = File.basename(path)
			ext = File.extname(path)
			if(ext == ".rxproj")
				open_rmxp_project(dir)
			elsif (ext == ".rvproj")
				open_rmvx_project(dir)
			elsif (ext == ".rvproj2")
				open_rmva_project(dir)
			else
				return false
			end
			return true
		end

		def open_rmxp_project(path)
			Path["project"] = path
			require "Ruby/XP/File - xp.rb"
			# Start Main Window and Login
			Builder.In(Window["Main"])
			Builder.In(Builder.Add(:tabs))
			require "Ruby/XP/Actor - xp.rb"
			require "Ruby/XP/Class - xp.rb"
		end
		def open_rmvx_project(path)
			Path["project"] = path
			require "Ruby/VX/File - vx.rb"
		end
		def open_rmva_project(path)
			Path["project"] = path
			require "Ruby/VA/File - va.rb"
		end
		# Promise land
		def Open_project
			return Proc.new { |*args| open_project(*args) }
		end
		def Open_file
			return Proc.new { |*args| open_file(*args) }
		end
	end
end


