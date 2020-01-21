cd ./UnitTestProject
msbuild.exe UnitTestProject.csproj -property:Configuration=Debug
cd ../packages\NUnit.ConsoleRunner.3.10.0\tools
nunit3-console.exe --testparam:browser=Edge --testparam:environment=dev "../../../UnitTestProject/bin/Debug/UnitTestProject.dll" --where "cat=All"
