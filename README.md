# HahnDemo2023

## Generate files for the compilation and creation of Docker images

#### 1. Must have docker installed on your PC.
#### 2. Open **CMD** at project root *(\HahnDemo2023)* .
#### 3. Run the "**docker compose up --build**" command and wait until ports are built and exposed.
#### 4. Open http://localhost:4200

##  Run the app in debug mode.

#### 1. Open the project solution in Visual Studio *(\HahnDemo2023)*.
#### 2. Choose AppWebApi as a startup project: **right click on the solution**-> **Properties->Single start project** (this should already be set).
#### 3. Choose **ISS Express** as the run mode.
#### 4. Open **CMD** in the /AppFront folder with administrator privileges.
#### 5. Run the command "**npm install**", wait for the installation to finish (takes a while).
#### 6. Run **ISS Express**.
#### 7. Run the command "**npm start**".
#### 8. Open http://localhost:4200
***
## Technical test with the DDD design pattern (Domain-Driven Design)

### Devised customer need:
Have control of the technological assets of each Department of the company (keyboards, mice, monitors, others) and know the remaining use time of each one.

### Solution proposed: 
To meet these requirements, I developed a software to record each asset.
When the user (person in charge of asset management) enters an asset, he must upload data such as the email address of the asset's Department, date of purchase, and total estimated useful life. As a result of this action, the new asset is saved in a database and the corresponding Department receives an email notification of the assigned asset.
When the user retrieves the data, he quickly obtains the list of the company's assets and the remaining useful life of each of them, according to the date of consultation.
