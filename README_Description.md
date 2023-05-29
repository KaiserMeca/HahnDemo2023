## Technical test with the DDD design pattern (Domain-Driven Design)

### Devised customer need:
Have control of the technological assets of each Department of the company (keyboards, mice, monitors, others) and know the remaining use time of each one.

### Solution proposed: 
To meet these requirements, I developed a software to record each asset.
When the user (person in charge of asset management) enters an asset, he must upload data such as the email address of the asset's Department, date of purchase, and total estimated useful life. As a result of this action, the new asset is saved in a database and the corresponding Department receives an email notification of the assigned asset.
When the user retrieves the data, he quickly obtains the list of the company's assets and the remaining useful life of each of them, according to the date of consultation.

### Conclusion: 
The software has an intuitive interface, easy to use and uses terms used by the client. With this solution, the user can obtain a list of all company assets and those that are about to expire, to plan their replacement.