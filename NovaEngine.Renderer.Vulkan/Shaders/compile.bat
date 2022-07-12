%VK_SDK_PATH%\Bin\glslangValidator.exe -g -V generate_frustums.comp -o bin/generate_frustums_comp.spv
%VK_SDK_PATH%\Bin\glslangValidator.exe -g -V depth.vert -o bin/depth_vert.spv
%VK_SDK_PATH%\Bin\glslangValidator.exe -g -V cull_lights.comp -o bin/cull_lights_comp.spv
%VK_SDK_PATH%\Bin\glslangValidator.exe -g -V mtsdf_text.vert -o bin/mtsdf_text_vert.spv
%VK_SDK_PATH%\Bin\glslangValidator.exe -g -V mtsdf_text.frag -o bin/mtsdf_text_frag.spv
%VK_SDK_PATH%\Bin\glslangValidator.exe -g -V pbr.vert -o bin/pbr_vert.spv
%VK_SDK_PATH%\Bin\glslangValidator.exe -g -V pbr.frag -o bin/pbr_frag.spv
%VK_SDK_PATH%\Bin\glslangValidator.exe -g -V solid_colour.vert -o bin/solid_colour_vert.spv
%VK_SDK_PATH%\Bin\glslangValidator.exe -g -V solid_colour.frag -o bin/solid_colour_frag.spv
pause