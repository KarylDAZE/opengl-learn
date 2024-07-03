cmake_minimum_required(VERSION 3.0)

PROJECT(opengl)

SET(CMAKE_CXX_STANDARD 23)

add_subdirectory(external/glfw-3.4)
include_directories(include)
include_directories(external/glfw-3.4/include)
include_directories(external/glad/include)
add_subdirectory(external/assimp-5.4.1)
include_directories(external/assimp-5.4.1/include)

find_package(OpenGL REQUIRED)
include_directories(external)
# 添加源文件
add_library(glad STATIC external/glad/src/glad.c)

file(GLOB SOURCES src/main.cpp src/shader.cpp)

# 添加可执行文件
add_executable(${PROJECT_NAME} ${SOURCES})

# 链接
target_link_libraries(${PROJECT_NAME} glad glfw assimp ${OPENGL_gl_LIBRARY})