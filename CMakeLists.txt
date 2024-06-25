cmake_minimum_required(VERSION 3.0)

PROJECT(opengl)

SET(CMAKE_CXX_STANDARD 23)

add_subdirectory(external/glfw-3.4)
include_directories(include)
include_directories(external/glfw-3.4/include)
include_directories(external/glad/include)
find_package(OpenGL REQUIRED)

# 添加源文件
add_library(glad STATIC external/glad/src/glad.c)

set(SOURCES src/shader.cpp src/main.cpp)

# 添加可执行文件
add_executable(${PROJECT_NAME} ${SOURCES})

# 链接GLFW和OpenGL库
target_link_libraries(${PROJECT_NAME} glad glfw ${OPENGL_gl_LIBRARY})