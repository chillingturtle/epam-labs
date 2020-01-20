Examples how to run unit tests through console.
Use special console:
cd "packages\NUnit.ConsoleRunner.3.10.0\tools"

Run nunit console, specify optional params and set path to DLL:
nunit3-console.exe --testparam:browser=Edge;environment=dev "UnitTestProject.dll"

NUnit doesn't have xml-suits but there are categories! Example:
nunit3-console.exe --testparam:browser=Edge;environment=dev "UnitTestProject.dll" --where "cat=<category name>"
If you want exclude category use !=.

Just for example I created bat-file Test.bat. Run it using dev console promt.
Of course, you can setup environment variables to make it better/smarter/more comfortable.