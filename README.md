# GameClub
Prepare Environment
1. Install NodeJs v18.19.1
2. Install .NET 8.0

Instructions
1. At root folder, open a command prompt and run following command lines:
- dotnet publish -o publish
- cd publish
- dotnet WebAPI.dll
2. At root folder, open another command prompt
- cd WebAPI/ClientApp
- npm i
- npm start -- --port 4100 -o

Note:
- Please make sure API is served at port:5000 and Angular APP is served at port:4200
- If the app doesn't open automatically, open browser and access the app at: http://localhost:4200