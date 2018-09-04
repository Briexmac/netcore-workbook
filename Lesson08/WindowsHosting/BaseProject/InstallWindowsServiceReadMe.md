# Build and Publish the project to the file system
This builds the project and copies all dependencies, config files, and any other required files to the same directory
```
dotnet publish -c Release
```

# Creating a Windows Service
```
sc create <SERVICE_NAME> binPath= "<PATH_TO_SERVICE_EXECUTABLE>"
```

# Example:
Build and Deploy the project
```
echo Stop the service before we build it, we will need to do this every time
sc stop Lesson08
echo Publishing the project with all it's dependencies
echo This will fail if any of the files are locked by the file system
dotnet publish -c Release
```
Install/Start the service 
```
echo Install the application as a windows service
echo This part only needs to happen once
sc create Lesson08 binPath= "C:\Users\danfe\Documents\MyProjects\netcore-workbook\Lesson08\WindowsHosting\BaseProject\bin\Release\netcoreapp2.1\win7-x64\publish\BaseProject.exe"
echo the space here -------^ is important, don't miss it
echo Start service now that it is installed
sc start Lesson08
```