cd .\src\ProjectIT\Server

ECHO "|-----------------|" "|Dropping Database|" "|-----------------|"
ECHO y | dotnet ef database drop

ECHO "|-----------------------|" "|Removing last migration|" "|-----------------------|"
dotnet ef migrations remove

ECHO "|--------------------|" "|Adding new migration|" "|--------------------|"
$name = New-Guid
dotnet ef migrations add "ProjectIT $name"

ECHO "|-----------------|" "|Updating Database|" "|-----------------|"
dotnet ef database update

cd ..\..\..