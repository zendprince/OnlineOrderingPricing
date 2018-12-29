Set-Location -Path "C:\Users\hnguyen4\Desktop\Selenium HN\NUnit.ConsoleRunner.3.6.1\tools"
@"
start-process "nunit3-console.exe " ".\DLL\Pipeline.Testing.dll --where method==Test" -Wait
"@
start-process "nunit3-console.exe " ".\DLL\Pipeline.Testing.dll" -Wait
Set-Location -Path "C:\Users\hnguyen4\Desktop\Selenium HN\NUnit.ConsoleRunner.3.6.1\tools\DLL"
$latest = Get-ChildItem  -Path *.html $dir | Sort-Object LastModifiedTime -Descending | Select-Object -First 1
start $latest
