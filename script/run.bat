@echo off
pushd .

if not exist build/opengl.exe (
	echo "opengl.exe not exists!"
	popd
	exit(1)
)

cd build

opengl.exe

popd
