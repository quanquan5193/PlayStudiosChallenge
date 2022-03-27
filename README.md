# PlayStudiosChallenge
PlayStudios challenge

## Application environment and how to run
Make sure the MongoDb is installed locally before running the application. It's used for initializing seed data and storing data during the application process.
The seeding data is initialized at the first time the application run in case there is no database named QuestingEngine was created before. Drop database and restart the application to re-trigger seeding data.
Visual Studio 2019 is also installed to open project.
Start application with pressing F5 button and the browser will show the Swagger.

## Application Configuration
Application allows administrator config the RateFromBet and LevelBonusRates via file appsettings.json.

## Sequence Diagram
Sequence diagram is stored in SequenceDiagram folder.