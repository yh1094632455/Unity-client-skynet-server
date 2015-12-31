import os

luamsg_dir = '../message'
def compile_proto(proto_dir):
	files = os.listdir(proto_dir)
	for f in files:
		print(f)
		if f.endswith('.proto'):
			print(f)
			target_file = f.replace(".proto",".pb")
			os.system("protoc -o ../proto/"+target_file+" ./"+f)

compile_proto(luamsg_dir)
		